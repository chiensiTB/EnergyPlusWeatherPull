using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace EnergyPlusWeatherPull
{
    class Program
    {
        static void Main(string[] args)
        {
            //filepath to open
            string filepath = @"C:\EnergyPlusV8-4-0\WeatherData\USA_VA_Sterling-Washington.Dulles.Intl.AP.724030_TMY3.epw";
            EPWeatherData epw = new EPWeatherData();
            epw.GetRawData(filepath);

            //get default properties...wetbulb and dry bulb
            epw.GetWeatherStats();
            var m1 = epw.WeatherFileStats.Monthly.Find(x => x.Id == 1);
            var dbstats = m1.DB;

            //get many properties
            var cats = epw.HourlyWeatherData[0].GetType().GetProperties();
            List<string> varsOfInterest = new List<string>();
            for (int s = 6; s < cats.Count(); s++)
            {
                var i = cats[s].Name;
                varsOfInterest.Add(i);
            }
            epw.GetWeatherStats(varsOfInterest.ToArray());
        }
    }
}
