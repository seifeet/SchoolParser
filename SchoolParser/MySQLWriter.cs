using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;
using log4net;

namespace SchoolParser
{

    public partial class MySQLWriter
    {

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool InsertSchool(SchoolModel school)
        {
            bool result = true;

            StringBuilder statement = new StringBuilder();
            statement.Append("INSERT INTO `schools`");
            statement.Append("     (");
            statement.Append("    `state`,");
            statement.Append("    `city`,");
            statement.Append("    `name`,");
            statement.Append("    `url`,");
            statement.Append("    `created_at`,");
            statement.Append("    `updated_at`");
            statement.Append("     )");
            statement.Append(" VALUES ");
            statement.Append("     (");
            statement.Append(Util.ConvertToDBString(school.State, null)).Append(",");
            statement.Append(Util.ConvertToDBString(school.City, null)).Append(",");
            statement.Append(Util.ConvertToDBString(school.Name, null)).Append(",");
            statement.Append(Util.ConvertToDBString(school.Url, null)).Append(",");

            String currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            statement.Append(Util.ConvertToDBString(currentTime, null)).Append(",");
            statement.Append(Util.ConvertToDBString(currentTime, null));
            statement.Append("     );");

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
                cmd.ExecuteNonQuery();
                TimeSpan ts = DateTime.Now - dtStart;

                school.ID = Convert.ToInt32(cmd.LastInsertedId);
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

        public bool UpdateSchool(SchoolModel school)
        {
            bool result = true;

            StringBuilder statement = new StringBuilder();
            statement.Append("UPDATE `schools` SET");
            if (school.State != null)
            {
                statement.Append("    `state` = ").Append(Util.ConvertToDBString(school.State, null)).Append(",");
            }
            if (school.City != null)
            {
                statement.Append("    `city` = ").Append(Util.ConvertToDBString(school.City, null)).Append(",");
            }
            if (school.Name != null)
            {
                statement.Append("    `name` = ").Append(Util.ConvertToDBString(school.Name, null)).Append(",");
            }
            if (school.Url != null)
            {
                statement.Append("    `url` = ").Append(Util.ConvertToDBString(school.Url, null)).Append(",");
            }

            String currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            statement.Append("    `created_at` = '").Append(currentTime).Append("'");
            statement.Append(" WHERE");

            if (school.ID != null)
            {
                statement.Append("    `id` = ").Append(Util.ConvertToDBString(school.ID, null));
            }
            statement.Append(";");

            MySqlConnection conn = new MySqlConnection(Util.MainConnectionString);

            try
            {
                conn.Open();

                MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = statement.ToString();
                cmd.CommandType = CommandType.Text;

                DateTime dtStart = DateTime.Now;
                cmd.ExecuteNonQuery();
                TimeSpan ts = DateTime.Now - dtStart;
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

    }
}
