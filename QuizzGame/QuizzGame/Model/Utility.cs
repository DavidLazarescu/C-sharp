using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace QuizzGame.Model
{
    public class Utility
    {

        /*Unescaping (Some characters could be seen as code, so they get transformed into smth else, this changes it back, so no
          weird simbols like "#37!;" are getting set instead of " */
        public static string UnescapeXml(string escaped)
        {
            return escaped.Replace("&lt;", "<")
                          .Replace("&gt;", ">")
                          .Replace("&quot;", "\"")
                          .Replace("&apos;", "'")
                          .Replace("&amp;", "&")
                          .Replace("&#39;", "");
        }

        public static string UnescapeHtml(string escaped)
        {
            return HttpUtility.HtmlDecode(escaped);
        }



        //Method to shuffle a list
        private static Random rng = new Random();
        public static void Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
