using System;
using System.IO;
using System.Net;
using System.Text;
using NScrape;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using NetworkSearches;
using System.Collections.Generic;
using System.Linq;

namespace CerebroConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = Console.ReadLine();

            var searchresults = NetworkSearchesManager.Instance.GetNetworkResults(key);

            Console.ReadKey();
        }
    }
}
