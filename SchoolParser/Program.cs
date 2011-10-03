using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;
using log4net;
using System.Text.RegularExpressions;

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

            if (nodes == null || nodes.Count == 0)
            {
                nodes = doc.DocumentNode.SelectNodes("//a");
            }

            if (nodes != null && nodes.Count > 0)
            {
                foreach (HtmlNode node in nodes)
                {
                    string parentHTML = node.ParentNode.InnerHtml;
                    if (parentHTML.Contains("a>"))
                    {
                        SchoolModel school = new SchoolModel();

                        school.State = Util.GetStateAbrv(state);
                        school.City = parentHTML.Substring(parentHTML.LastIndexOf("a>") + 2);
                        school.City = Regex.Replace(school.City, @"<[^>]*>", String.Empty);
                        school.City = school.City.Trim(trimChars);
                        school.Url = node.GetAttributeValue("href", "");
                        school.Name = node.InnerHtml.Trim(trimChars);

                        if (school.Name == null || school.Name.Length == 0)
                        {
                            continue;
                        }

                        if (school.City == null || school.City.Length == 0)
                        {
                            continue;
                        }

                        if (school.Url == null || school.Url.Length == 0 || school.Url.Contains("..") || school.Url.Contains("#"))
                        {
                            continue;
                        }

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
            else
            {
                log.Error("Something went terribly wrong and no schools were found for " + state);
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
                        try
                        {
                            parseSchools(url + href, state);
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message);
                        }
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
