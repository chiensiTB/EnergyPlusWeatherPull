using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using MathNet.Numerics;

namespace EnergyPlusWeatherPull
{
    class Program
    {
        static void Main(string[] args)
        {
            //filepath to open
            string filepath = @"C:\Temp\USA_ID_Pocatello.Muni.AP.725780_TMY3.epw";
            EPWeatherData epw = new EPWeatherData();
            epw.GetRawData(filepath);

            //get default properties...wetbulb and dry bulb
            epw.GetWeatherStats();
            var m1 = epw.WeatherFileStats.Monthly.Find(x => x.Id == 1);
            var dbstats = m1.DB;

            //get all properties basic monthly statistics
            var cats = epw.HourlyWeatherDataRawList[0].GetType().GetProperties();
            List<string> varsOfInterest = new List<string>();
            for (int s = 6; s < cats.Count(); s++)
            {
                var i = cats[s].Name;
                varsOfInterest.Add(i);
            }
            epw.GetWeatherStats(varsOfInterest.ToArray());
            //write out some of it to CSV
            using (TextWriter tw = File.CreateText("C:\\Temp\\MonthlySummaryWeather.csv"))
            {
                var csv = new CsvWriter(tw);
                foreach(var item in epw.WeatherFileStats.Monthly)
                {
                    List<MathNet.Numerics.Statistics.DescriptiveStatistics> st = new List<MathNet.Numerics.Statistics.DescriptiveStatistics>();
                    st.Add(item.DB);
                    st.Add(item.WB);
                    csv.WriteRecords(st);
                }
            }

            //example of ways to use this data, once you have collected it, in methods separate from the EPWeather Data Class:
            //Get degree days
            DegreeDays dd = new DegreeDays(19, 12, epw.HourlyWeatherDataRawList);

            //show how to get mean coincident wet bulb.
            List<string> dbwb = new List<string> { "DB", "WB" };
            var dict = epw.GetHourlyListsTransformed(dbwb);
            WeatherFileStats wf = new WeatherFileStats();
            var mcwbs = wf.GetMCWBandDBBins(dict, 0.555*2);
            //write it to CSV
            using (TextWriter tw = File.CreateText("C:\\Temp\\outmcwb.csv"))
            {
                var csv = new CsvWriter(tw);
                csv.WriteField("Lower Range");
                csv.WriteField("Upper Range");
                csv.WriteField("DB Hours Count");
                csv.WriteField("MCWB");
                csv.NextRecord();
                for(int mw = 0; mw < mcwbs.mcwbs.Count; mw++)
                {
                    csv.WriteField(mcwbs.mcwbs[mw].bucketLower);
                    csv.WriteField(mcwbs.mcwbs[mw].buckeUpper);
                    csv.WriteField(mcwbs.dbBins.GetBucketOf(mcwbs.mcwbs[mw].buckeUpper).Count);
                    csv.WriteField(mcwbs.mcwbs[mw].coincidentWB);
                    csv.NextRecord();
                }
            }
            
        }
    }
}
