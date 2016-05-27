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
            epw.GetWeatherStats();
            var m1 = epw.WeatherFileStats.Find(x => x.Id == 1);
            var dbstats = m1.Monthly.DB;
        }
    }
}
