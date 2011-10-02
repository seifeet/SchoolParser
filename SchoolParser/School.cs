using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolParser
{
    public class SchoolModel
    {
        protected Int32? m_ID          = null;
        protected String m_State       = null;
        protected String m_City        = null;
        protected String m_Name        = null;
        protected String m_Url         = null;
        protected DateTime m_CreatedAt = DateTime.MinValue;
        protected DateTime m_UpdatedAt = DateTime.MinValue;

        public Int32? ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

        public String State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public String City
        {
            get { return m_City; }
            set { m_City = value; }
        }

        public String Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public String Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public DateTime CreatedAt
        {
            get { return m_CreatedAt; }
            set { m_CreatedAt = value; }
        }

        public DateTime UpdatedAt
        {
            get { return m_UpdatedAt; }
            set { m_UpdatedAt = value; }
        }

    }
}
