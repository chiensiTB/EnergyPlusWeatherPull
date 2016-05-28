# EnergyPlusWeatherPull
## An OpenSource C# Library to Interpret epw Files

This is a new library created for personal and commercial projects, to be able to pull information from [EnergyPlus (E+) weather files](energyplus.net/weather) (denoted by the .epw extension).  EnergyPlus is a software program created by the US Government used to simulate the energy performance of buildings.  This project is not affiliated with any governmental agency.

The goals of this project are two-pronged.  First, to make sure that the whole methodology for users to gain access to these classes is simple, easy, and clean.  Second, the calls to access more powerful statistical analysis of the weather should also be trivial, with plenty of syntactic sugar.  

##Dependencies
[CsvHelper](https://joshclose.github.io/CsvHelper/)

[Math.Net Numerics](http://numerics.mathdotnet.com/)

##Use Cases

Primary use case for this tool is to provide a simple way to acess and analyze historical 20 year average weather data against real-time weather.  So, if a user wants to get the entire year's worth of data, and use in their own analysis or .Net software project, it is now trivial to pull this from an .epw file.

A second use case is to get statistics on historical weather data (means, variance, median, histograms, etc.)  The need being filled here by EnergyPlusWeatherPull is dead-simple calls to many statistical summaries of the hourly data provided by .epw files.  This may assist engineers and other interested folks to easily compare meteorological long-term averages to more real-time data.

##Getting Started

The main class library is EPWeatherData.  To access a files data, simply create an empty constructor and call GetRawData(filepath) as in the following command:

'''
string filepath = "some filepath";
EPWeatherData epw = new EPWeatherData();
epw.GetRawData(filepath);
'''

I have started a statistical class to hold relevant information about the weather data history.  Currently, EnergyPlusWeatherPull can easily return monthly statistics of DB and WB only, but this is quickly being expanded.
