# EnergyPlusWeatherPull
## An OpenSource C# Library to Interpret epw Files

This is a library offered under the MIT license, created for either personal and commercial projects, in order to have powerful capabilities to pull information from [EnergyPlus (E+) weather files](energyplus.net/weather) (denoted by the .epw extension) in a .NET environment.  EnergyPlus is a software program created by the US Government used to simulate the energy performance of buildings.  This project is not affiliated with any governmental agency.

The goals of this project are two.  First, to make sure that the whole methodology for users to gain access to these classes is simple, easy, and clean.  Second, the calls to access more powerful statistical analysis of the weather should also be trivial, with plenty of syntactic sugar.  

##Dependencies
[CsvHelper](https://joshclose.github.io/CsvHelper/)

[Math.Net Numerics](http://numerics.mathdotnet.com/)

##Use Cases

Primary use case for this tool is to provide a simple way to acess and analyze historical 20 year average weather data against real-time weather.  So, if a user wants to get the entire year's worth of data, and use in their own analysis or .NET software project, it is now trivial to pull this from an .epw file.

A second use case is to get statistics on historical weather data (means, variance, median, histograms, degree days, etc.)  The need being filled here by EnergyPlusWeatherPull is dead-simple calls to many statistical summaries of the hourly data provided by .epw files.  This may assist engineers and other interested folks to easily compare meteorological long-term averages to more real-time data.

##Getting Started

The main class library is EPWeatherData.  You can reference this dll in a console app, or you project of choice..  

Once the DLL has been referenced...To access a .epw file's data, simply create an empty constructor and call GetRawData(filepath) as in the following command:

'''
//load data into memory
            string filepath = @"C:\EnergyPlusV8-4-0\WeatherData\USA_CA_San.Francisco.Intl.AP.724940_TMY3.epw";
            EPWeatherData epw = new EPWeatherData();
            epw.GetRawData(filepath);

            //get typical statistics for any EPW weather variable broken down on a monthly basis
            string[] paramsOfInterest = new string[2];
            paramsOfInterest[0] = "DB";
            paramsOfInterest[1] = "WB";
            epw.GetWeatherStats(paramsOfInterest);

            //if a user wants to work with lists....
            var r = epw.GetHourlyListsTransformed(paramsOfInterest.ToList());


            DegreeDays dd = new DegreeDays();
            dd.CoolingBalancePoint = (decimal)((50 - 32) / 1.8); //currently only accepts metric
            dd.HeatingBalancePoint = (decimal)((65 - 32) / 1.8); //currently only accepts metric
            dd.CalculateDailyDegreeDays(epw.HourlyWeatherDataRawList);
            dd.CalculateMonthlyDegreeDays();

            var annHDD = dd.MonthlyHDD.Sum(x => x.Value); //still in metric
            var annCDD = dd.MonthlyCDD.Sum(x => x.Value); //still in metric
'''


