using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KinopoiskAPI
{
    public class KinipoiskApi
    {
        static public string GetKinopoiskData(string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();
            return data;
        }


        static public string GetKinopoiskUrl(string title)
        {
            string kinopoiskId = GetKinopoiskId(System.Uri.EscapeUriString(title));
            return $"https://api.kinopoisk.cloud/movies/{kinopoiskId}/token/e479bfa37eb4a59d8463c13222f75367";
        }

        static private string GetKinopoiskId(string MovieName, string searchEngine = "google")
        {
            string GoogleSearch = "http://www.google.com/search?q=kinopoisk+";
            string BingSearch = "http://www.bing.com/search?q=kinopoisk+";
            string AskSearch = "http://www.ask.com/web?q=kinopoisk+";
            string url = GoogleSearch + MovieName; //default to Google search
            if (searchEngine.ToLower().Equals("bing")) url = BingSearch + MovieName;
            if (searchEngine.ToLower().Equals("ask")) url = AskSearch + MovieName;
            string html = GetUrlData(url);
            string regex = @"(film/)\d+";
            var reg = new Regex(regex);
            var imdbUrls = reg.Matches(html);

            if (imdbUrls.Count > 0)
            {
                var matchId = imdbUrls.ElementAt(0);
                var id = matchId.Value.Split("/")[matchId.Value.Split("/").Length - 1];
                return id;
            } //return first IMDb result
            else if (searchEngine.ToLower().Equals("google")) //if Google search fails
                return GetKinopoiskId(MovieName, "bing"); //search using Bing
            else if (searchEngine.ToLower().Equals("bing")) //if Bing search fails
                return GetKinopoiskId(MovieName, "ask"); //search using Ask
            else //search fails
                return string.Empty;
        }

        static private string GetUrlData(string url)
        {
            WebClient client = new WebClient();
            Random r = new Random();
            //Random IP Address
            client.Headers["X-Forwarded-For"] = r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255);
            //Random User-Agent
            client.Headers["User-Agent"] = "Mozilla/" + r.Next(3, 5) + ".0 (Windows NT " + r.Next(3, 5) + "." + r.Next(0, 2) + "; rv:2.0.1) Gecko/20100101 Firefox/" + r.Next(3, 5) + "." + r.Next(0, 5) + "." + r.Next(0, 5);
            Stream datastream = client.OpenRead(url);
            StreamReader reader = new StreamReader(datastream);
            StringBuilder sb = new StringBuilder();
            while (!reader.EndOfStream)
                sb.Append(reader.ReadLine());
            return sb.ToString();
        }

        //Match single instance
        private string Match(string regex, string html, int i = 1)
        {
            return new Regex(regex, RegexOptions.Multiline).Match(html).Groups[i].Value.Trim();
        }

        //Match all instances and return as ArrayList
        static private ArrayList MatchAll(string regex, string html, int i = 1)
        {
            ArrayList list = new ArrayList();
            foreach (Match m in new Regex(regex, RegexOptions.Multiline).Matches(html))
                list.Add(m.Groups[i].Value.Trim());
            return list;
        }
    }
}
