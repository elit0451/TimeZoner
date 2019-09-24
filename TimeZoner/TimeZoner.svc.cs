using System;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class TimeZoner : ISOAP, IREST
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
                            if (iso)
                            {
                                if (zone.CountryCode == country.ToUpper())
                                {
                                    UTCtime = zone.GMTOffset;
                                    found = true;
                                    break;
                                }
                            }
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
                            return -99;
                    }
                }
            }


            int totalSeconds;
            try
            {
                string hour = UTCtime.Substring(UTCtime.IndexOf(':') - 3, 3);
                string minutes = UTCtime.Substring(UTCtime.IndexOf(':') + 1, 2);

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
