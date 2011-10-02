using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SchoolParser
{
    public class Util
    {

        public static String ConvertToDBString(Int32? i, string val)
        {
            if (i != null)
            {
                val = i.ToString();
            }

            return ((val == null) ? "null" : val.Replace("'", "''"));
        }

        public static String ConvertToDBString(Int64? i, string val)
        {
            if (i != null)
            {
                val = i.ToString();
            }

            return ((val == null) ? "null" : val.Replace("'", "''"));
        }

        public static String ConvertToDBString(Double? d, string val)
        {
            if (d != null)
            {
                val = d.ToString();
            }

            return ((val == null) ? "null" : val.Replace("'", "''"));
        }

        public static String ConvertToDBString(Single? f, string val)
        {
            if (f != null)
            {
                val = f.ToString();
            }

            return ((val == null) ? "null" : val.Replace("'", "''"));
        }

        public static String ConvertToDBString(string s, string val)
        {
            if (s != null && s.Length > 0)
            {
                val = "'" + s.Replace("'", "''") + "'";
            }
            else if (val == null)
            {
                val = "null";
            }
            else
            {
                val = "''";
            }

            return val;
        }

        public static String ConvertToDBString(TimeSpan? t, string val)
        {
            if (t != null)
            {
                val = "'" + t.Value + "'";
            }
            else if (val == null)
            {
                val = "null";
            }
            else
            {
                val = "'00:00:00'";
            }

            return val;
        }

        public static String ConvertToDBString(DateTime d, string val)
        {
            if (d != null && d != DateTime.MinValue)
            {
                val = "'" + d.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            else if (val == null)
            {
                val = "null";
            }
            else
            {
                val = "'0000-00-00 00:00:00'";
            }

            return val;
        }

        /// <summary>
        /// Safely convert
        /// </summary>
        public class SafeConvert
        {
            /// <summary>
            /// Safely convert to Int32?
            /// </summary>
            public static Int32? ToInt32(object o, Int32? defaultValue)
            {
                Int32? result = defaultValue;
                if (o != DBNull.Value)
                {
                    try
                    {
                        result = Convert.ToInt32(o);
                    }
                    catch { }
                }
                return result;
            }

            /// <summary>
            /// Safely convert to ToInt64?
            /// </summary>
            public static Int64? ToInt64(object o, Int64? defaultValue)
            {
                Int64? result = defaultValue;
                try
                {
                    result = Convert.ToInt64(o);
                }
                catch { }
                return result;
            }

            /// <summary>
            /// Safely convert to Double?
            /// </summary>
            public static Double? ToDouble(object o, Double? defaultValue)
            {
                Double? result = defaultValue;
                if (o != DBNull.Value)
                {
                    try
                    {
                        result = Convert.ToDouble(o);
                    }
                    catch { }
                }
                return result;
            }

            /// <summary>
            /// Safely convert to Single?
            /// </summary>
            public static Single? ToSingle(object o, Single? defaultValue)
            {
                Single? result = defaultValue;
                if (o != DBNull.Value)
                {
                    try
                    {
                        result = Convert.ToSingle(o);
                    }
                    catch { }
                }
                return result;
            }

            /// <summary>
            /// Safely convert to String
            /// </summary>
            public static string ToString(object o, string defaultValue)
            {
                string result = defaultValue;
                try
                {
                    result = Convert.ToString(o);
                }
                catch { }
                return result;
            }

            /// Safely convert to TimeSpan
            /// </summary>
            public static TimeSpan? ToTimeSpan(object o, TimeSpan? defaultValue)
            {
                TimeSpan? result = defaultValue;
                try
                {
                    result = TimeSpan.Parse(Convert.ToString(o));
                }
                catch { }
                return result;
            }

            /// <summary>
            /// Safely convert to DateTime
            /// </summary>
            public static DateTime ToDateTime(object o, DateTime defaultValue)
            {
                DateTime result = defaultValue;
                try
                {
                    result = DateTime.Parse(Convert.ToString(o));
                }
                catch { }
                return result;
            }

        }

        /// <summary>
        /// Convert state name to 2 letter abbreviation
        /// </summary>
        protected static Dictionary<string, string> STATES = new Dictionary<string, string>();

        public static string GetStateAbrv(string name)
        {
            string result = "";

            if (STATES.Count == 0)
            {
                STATES.Add("Alabama", "AL");
                STATES.Add("Alaska", "AK");
                STATES.Add("Arizona", "AZ");
                STATES.Add("Arkansas", "AR");
                STATES.Add("California", "CA");
                STATES.Add("Colorado", "CO");
                STATES.Add("Connecticut", "CT");
                STATES.Add("Delaware", "DE");
                STATES.Add("District of Columbia", "DC");
                STATES.Add("Florida", "FL");
                STATES.Add("Georgia", "GA");
                STATES.Add("Hawaii", "HI");
                STATES.Add("Idaho", "ID");
                STATES.Add("Illinois", "IL");
                STATES.Add("Indiana", "IN");
                STATES.Add("Iowa", "IA");
                STATES.Add("Kansas", "KS");
                STATES.Add("Kentucky", "KY");
                STATES.Add("Louisiana", "LA");
                STATES.Add("Maine", "ME");
                STATES.Add("Maryland", "MD");
                STATES.Add("Massachusetts", "MA");
                STATES.Add("Michigan", "MI");
                STATES.Add("Minnesota", "MN");
                STATES.Add("Mississippi", "MS");
                STATES.Add("Missouri", "MO");
                STATES.Add("Montana", "MT");
                STATES.Add("Nebraska", "NE");
                STATES.Add("Nevada", "NV");
                STATES.Add("New Hampshire", "NH");
                STATES.Add("New Jersey", "NJ");
                STATES.Add("New Mexico", "NM");
                STATES.Add("New York", "NY");
                STATES.Add("North Carolina", "NC");
                STATES.Add("North Dakota", "ND");
                STATES.Add("Ohio", "OH");
                STATES.Add("Oklahoma", "OK");
                STATES.Add("Oregon", "OR");
                STATES.Add("Pennsylvania", "PA");
                STATES.Add("Rhode Island", "RI");
                STATES.Add("South Carolina", "SC");
                STATES.Add("South Dakota", "SD");
                STATES.Add("Tennessee", "TN");
                STATES.Add("Texas", "TX");
                STATES.Add("Utah", "UT");
                STATES.Add("Vermont", "VT");
                STATES.Add("Virginia", "VA");
                STATES.Add("Washington", "WA");
                STATES.Add("West Virginia", "WV");
                STATES.Add("Wisconsin", "WI");
                STATES.Add("Wyoming", "WY");
                STATES.Add("American Samoa", "AS");
                STATES.Add("Guam", "GU");
                STATES.Add("Northern Mariana Islands", "MP");
                STATES.Add("Puerto Rico", "PR");
                STATES.Add("Virgin Islands", "VI");
                STATES.Add("U.S. Minor Outlying Islands", "");
                STATES.Add("Federated States of Micronesia", "FM");
                STATES.Add("Marshall Islands", "MH");
                STATES.Add("Palau", "PW");
                STATES.Add("D.C.", "DC");
            }

            STATES.TryGetValue(name, out result);

            return result;
        }

        /// <summary>
        /// Read configuration parameters
        /// </summary>
        protected static String m_MainConnectionString = ConfigurationManager.AppSettings.Get("MainConnectionString");
        protected static String m_SiteOneUrl           = ConfigurationManager.AppSettings.Get("SiteOneUrl");
        protected static String m_SiteOneHome          = ConfigurationManager.AppSettings.Get("SiteOneHome");

        public static String MainConnectionString
        {
            get
            {
                return m_MainConnectionString;
            }
        }

        public static String SiteOneUrl
        {
            get
            {
                return m_SiteOneUrl;
            }
        }

        public static String SiteOneHome
        {
            get
            {
                return m_SiteOneHome;
            }
        }
    }
}
