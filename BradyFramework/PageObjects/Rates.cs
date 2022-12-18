using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BradyFramework.PageObjects
{
    public class Rates
    {
        public class Overview
        {
            public string? currency { get; set; }
            public string? source { get; set; }
            public string? price { get; set; }
            public string? chgPer { get; set; }
            public string? chg { get; set; }
            public string? Bid { get; set; }
            public string? Ask { get; set; }
            public string? high { get; set; }
            public string? low { get; set; }
        }
        public class Performance
        {
            public string? chgPer { get; set;}
            public string? weekChgPer { get; set; }
            public string? monthChgPer { get; set; }
            public string? threeMPerf { get; set; }
            public string? sixMPerf { get; set; }
            public string? ytdMPerf { get; set; }
            public string? yearPerf { get; set; }
            public string? Volatility { get; set; }
        }
        public class Oscillators
        {
            public string? Rating { get; set;}
            public string? adx { get; set; }
            public string? ao { get; set; }
            public string? atr { get; set; }
            public string? cci { get; set; }
            public string? macdLevel { get; set; }
        }

        public class TrendFollowing
        {
            public double? movingAveragesRating { get; set; }
            public double? price { get; set; }
            public double? sma20 { get; set; }
            public double? sma50 { get; set; }
            public double? sma200 { get; set; }
            public double? bbup { get; set; }
        }

    }
}
