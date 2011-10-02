using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;
using MySql.Data.Types;
using log4net;

namespace SchoolParser
{
    public partial class MySQLReader
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetSchool(SchoolModel school)
        {
            bool result = false;

            StringBuilder statement = new StringBuilder();
            statement.Append("SELECT");
            statement.Append("    s.`id`,");
            statement.Append("    s.`state`,");
            statement.Append("    s.`city`,");
            statement.Append("    s.`name`,");
            statement.Append("    s.`url`,");
            statement.Append("    s.`created_at`,");
            statement.Append("    s.`updated_at`");
            statement.Append(" FROM");
            statement.Append("   `schools` s");
            statement.Append(" WHERE");

            if (school.ID != null)
            {
                statement.Append("    s.`id` = ").Append(Util.ConvertToDBString(school.ID, null));
            }
            else if (school.State != null && school.State != null && school.State != null)
            {
                statement.Append("    s.`state` = ").Append(Util.ConvertToDBString(school.State, null));
                statement.Append("    AND s.`city` = ").Append(Util.ConvertToDBString(school.City, null));
                statement.Append("    AND s.`name` = ").Append(Util.ConvertToDBString(school.Name, null));
            }

            statement.Append(";");

            MySqlConnection conn = new MySqlConnection();

            try
            {
                conn.ConnectionString = Util.MainConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = statement.ToString();
                cmd.CommandType = CommandType.Text;

                DateTime dtStart = DateTime.Now;
                MySqlDataReader rdr = cmd.ExecuteReader();
                TimeSpan ts = DateTime.Now - dtStart;

                while (rdr.Read())
                {
                    school.ID        = Util.SafeConvert.ToInt32(rdr["id"], null);
                    school.State     = Util.SafeConvert.ToString(rdr["state"], null);
                    school.City      = Util.SafeConvert.ToString(rdr["city"], null);
                    school.Name      = Util.SafeConvert.ToString(rdr["name"], null);
                    school.Url       = Util.SafeConvert.ToString(rdr["url"], null);
                    school.CreatedAt = GetMySQLDateTime(rdr, "created_at");
                    school.UpdatedAt = GetMySQLDateTime(rdr, "updated_at");

                    result = true;
                }

                conn.Close();

            }
            catch (MySqlException ex)
            {
                if (school.Name != null)
                {
                    log.Info(school.Name);
                }
                log.Error(ex.Message);
                result = false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                result = false;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static DateTime GetMySQLDateTime(MySqlDataReader mySQLDataReader, String column)
        {
            DateTime dateTime = DateTime.MinValue;

            try
            {
                int index = mySQLDataReader.GetOrdinal(column);
                if (!mySQLDataReader.IsDBNull(index))
                {
                    MySqlDateTime mySQLDateTime = mySQLDataReader.GetMySqlDateTime(column);
                    if (mySQLDateTime.Day > 0)
                    {
                        dateTime = mySQLDateTime.GetDateTime();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            return dateTime;
        }

    }
}
