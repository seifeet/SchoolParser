using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using log4net;

namespace SchoolParser
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static MySQLReader reader = new MySQLReader();
        static MySQLWriter wrtier = new MySQLWriter();

        static void parseSchools(string url, string state)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//p/a");

            char[] trimChars = { ',', ' ', '-' };

            if (nodes != null)
            {
                foreach (HtmlNode node in nodes)
                {
                    string parentHTML = node.ParentNode.InnerHtml;
                    if (parentHTML.Contains(">"))
                    {
                        SchoolModel school = new SchoolModel();

                        school.State = Util.GetStateAbrv(state);
                        school.City  = parentHTML.Substring(parentHTML.LastIndexOf(">") + 1);
                        school.City  = school.City.Trim(trimChars);
                        school.Url   = node.GetAttributeValue("href", "");
                        school.Name  = node.InnerHtml.Trim(trimChars);

                        reader.GetSchool(school);

                        if (school.ID != null)
                        {
                            wrtier.UpdateSchool(school);
                        }
                        else
                        {
                            wrtier.InsertSchool(school);
                        }
                    }
                }
            }
        }

        static void parseStates(string url, string page)
        {
            HtmlWeb hw = new HtmlWeb();
            HtmlDocument doc = hw.Load(url + page);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//font/a");

            if (nodes != null)
            {
                bool import = false;

                foreach (HtmlNode node in nodes)
                {
                    string state = node.InnerHtml;
                    if (state == "Alabama")
                    {
                        import = true;
                    }

                    if (import)
                    {
                        string href = node.GetAttributeValue("href", "");

                        log.Info(state);
                        parseSchools(url + href, state);
                    }

                    if (state == "Wyoming")
                    {
                        break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            parseStates(Util.SiteOneUrl, Util.SiteOneHome);

        }
    }
}
