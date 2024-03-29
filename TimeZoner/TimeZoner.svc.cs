﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Reflection;
using CsvHelper;
using System.Net;

namespace TimeZoner
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TimeZoner : ISOAP
    {
        public int GetCountryTime(string country)
        {
            return GetCountryTimeUTC(country, false);
        }

        public int GetISOTime(string countryISO)
        {
            return GetCountryTimeUTC(countryISO, true);
        }

        private int GetCountryTimeUTC(string country, bool iso)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // Get path to the csv file
            var resourceName = assembly.GetManifestResourceNames().Single(name => name.EndsWith("time-zones.csv"));
            string UTCtime = "";
            bool found = false;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.Delimiter = ";";
                        var records = csv.GetRecords<UTCZone>();

                        foreach (UTCZone zone in records)
                        {
                            // Check if the passed country name exists in our records
                            if (iso)
                            {
                                if (zone.CountryCode == country.ToUpper())
                                {
                                    UTCtime = zone.GMTOffset;
                                    found = true;
                                    break;
                                }
                            }
                            // Check if the passed country name exists in our records
                            else
                            {
                                if (zone.CountryName.ToLower() == country.ToLower())
                                {
                                    UTCtime = zone.GMTOffset;
                                    found = true;
                                    break;
                                }
                            }
                        }

                        if (found == false)
                            throw new FaultException<ErrorData>(new ErrorData() { ErrorMessage = "Country Not Found!" });
                    }
                }
            }


            int totalSeconds;
            try
            {
                // Get a string for the hours from a country UTC (ex: -02:30)
                string hour = UTCtime.Substring(UTCtime.IndexOf(':') - 3, 3);
                // Get a string for the minutes from a country UTC (ex: -02:30)
                string minutes = UTCtime.Substring(UTCtime.IndexOf(':') + 1, 2);

                // convert hours and minutes to seconds for easier addition
                totalSeconds = Int32.Parse(hour) * 3600 + Int32.Parse(minutes) * 60;
            }
            catch (Exception e)
            {
                totalSeconds = 0;
            }
            return totalSeconds;
        }
    }
}
