using System;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.Xml;
using TimeZoner;

namespace TimeZonerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Program timeZoner = new Program();

            // Helper method to get GMT Universal time from an external web service
            double currentTime = timeZoner.GetGMTTime();

            string input = Console.ReadLine();
            bool countryCode = input.Length == 2;
            
            timeZoner.RunSoapAsync(currentTime, countryCode, input);
            timeZoner.RunRest(currentTime, countryCode, input);

            Console.WriteLine();
            Console.ReadKey();
        }

        public async void RunSoapAsync(double currentTime, bool countryCode, string country)
        {
            SOAPClient proxy = new SOAPClient();
            string countryTime;

            try
            {
                if (countryCode)
                    countryTime = await proxy.GetISOTimeAsync(country);
                else
                    countryTime = await proxy.GetCountryTimeAsync(country);

                Console.WriteLine("Country time SOAP: " + UnixTimeStampToDateTime(currentTime + Double.Parse(countryTime)));
            }
            catch (FaultException e)
            {
                Console.WriteLine("SOAP: Country not found!");
            }
            
        }

        public void RunRest(double currentTime, bool countryCode, string country)
        {
            HttpWebRequest request;

            if (countryCode)
                request = (HttpWebRequest)WebRequest.Create("https://localhost:44313/api/TimeZone/GetCountryISO/" + country);
            else
                request = (HttpWebRequest)WebRequest.Create("https://localhost:44313/api/TimeZone/GetCountryTime/" + country);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                string jsonResponse = streamReader.ReadToEnd();

                streamReader.Close();

                Console.WriteLine("Country time REST: " + UnixTimeStampToDateTime(currentTime + Double.Parse(jsonResponse)));
            }
            catch (WebException e)
            {
                Console.WriteLine("REST: Country not found!");
            }
        }

        private double GetGMTTime()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://api.timezonedb.com/v2.1/get-time-zone?key=LW4TEDDA3EA5&format=xml&by=zone&zone=GMT");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string xmlString = streamReader.ReadToEnd();

            streamReader.Close();
            
            XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
            xmlDoc.LoadXml(xmlString); // Load the XML document from the specified file

            // Get elements
            XmlNodeList timestamp = xmlDoc.GetElementsByTagName("timestamp");

            return Double.Parse(timestamp[0].InnerText);
        }

        private static string UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime.ToLongTimeString();
        }
        
    }
}
