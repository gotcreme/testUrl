using ShortUrls.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ShortUrls.Helpers
{
    public static class UrlHelper
    {
        public static string GenerateRandomShortUrl(int length = 10)
        {
            Random randomGenerator = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());
        }

        public static bool HasHttpProtocol(string url)
        {
            url = url.ToLower();
            if (url.Length > 5)
                return url.StartsWith(Constants.Protocol.Http) || url.StartsWith(Constants.Protocol.Https);

            return false;
        }

        public static bool IsUrlExists(string url)
        {
            if (!HasHttpProtocol(url))
                url = Constants.Protocol.Http + url;

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        public static string BuildUrlFromSegments(string scheme, string authority, string path)
        {
            return scheme + "://" + authority + "/" + path;
        }
    }
}