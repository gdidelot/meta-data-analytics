using System;
using System.Threading;
using System.Net;
using System.Text;
using NScrape;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using CerebroModels;
using System.Threading.Tasks;

namespace NetworkSearches
{
    public class NetworkSearchesManager
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static NetworkSearchesManager instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static NetworkSearchesManager Instance
        {
            get 
            {
                if(instance == null)
                {
                    instance = new NetworkSearchesManager();
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets the network results.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public List<WebPage> GetNetworkResults(string key)
        {
            var networkSearchTask = Task<List<WebPage>>.Factory.StartNew(() =>
            {
                var result = new List<WebPage>();
                var bufferForHtml = new StringBuilder();
                var encodedBytes = new byte[8192];
                var urlForSearch = string.Format("http://google.com/search?q={0}", key.Trim());

                var request = (HttpWebRequest)System.Net.WebRequest.Create(urlForSearch);
                var response = (HttpWebResponse)request.GetResponse();

                using (var responseFromGoogle = response.GetResponseStream())
                {
                    var enc = response.GetEncoding();

                    var count = 0;
                    do
                    {
                        count = responseFromGoogle.Read(encodedBytes, 0, encodedBytes.Length);
                        if (count != 0)
                        {
                            var tempString = enc.GetString(encodedBytes, 0, count);
                            bufferForHtml.Append(tempString);
                        }
                    }

                    while (count > 0);
                }

                var sbb = bufferForHtml.ToString();

                var processedHtml = new HtmlAgilityPack.HtmlDocument
                {
                    OptionOutputAsXml = true
                };

                processedHtml.LoadHtml(sbb);
                var doc = processedHtml.DocumentNode;

                foreach (var link in doc.SelectNodes("//a[@href]"))
                {
                    var hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (!hrefValue.ToUpper().Contains("GOOGLE")
                        && hrefValue.Contains("/url?q=")
                        && hrefValue.ToUpper().Contains("HTTP"))

                    {
                        var index = hrefValue.IndexOf("&");
                        if (index > 0)
                        {
                            hrefValue = hrefValue.Substring(0, index);
                            hrefValue = hrefValue.Replace("/url?q=", string.Empty);
                            var output = Regex.Replace(link.InnerText, "&quot;\\.?", string.Empty);
                            result.Add(new WebPage() { Url = hrefValue, Title = output });
                        }
                    }
                }

                return result;
            });

            return networkSearchTask.Result;
        }
    }
}
