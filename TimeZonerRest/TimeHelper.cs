using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TimeZonerRest
{
    public static class TimeHelper
    {
        public static int GetCountryTimeUTC(string country, bool iso, string basePath)
        {
            string UTCtime = "";
            bool found = false;
            
            FileStream file = new FileStream(basePath + @"\data\time-zones.csv", FileMode.Open);
            using (Stream stream = file)
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        csv.Configuration.Delimiter = ";";
                        var records = csv.GetRecords<UTCZone>();

                        foreach (UTCZone zone in records)
                        {
                            // Check if the passed ISO exists in our records
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
                            throw new Exception();
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
