using System;
using System.IO;
using System.Net;
using System.Xml;
using TimeZoner;

namespace TimeZonerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Program timeZoner = new Program();
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

            if (countryCode)
                countryTime = await proxy.GetISOTimeAsync(country);
            else
                countryTime = await proxy.GetCountryTimeAsync(country);

            if (int.Parse(countryTime) == -99)
                Console.WriteLine("Country not found!");
            else
                Console.WriteLine("Country time SOAP: " + UnixTimeStampToDateTime(currentTime + Double.Parse(countryTime)));
        }

        public void RunRest(double currentTime, bool countryCode, string country)
        {
            HttpWebRequest request;

            if (countryCode)
                request = (HttpWebRequest)WebRequest.Create("http://localhost:49851/TimeZoner.svc/rest/getISOTime/" + country);
            else
                request = (HttpWebRequest)WebRequest.Create("http://localhost:49851/TimeZoner.svc/rest/getCountryTime/" + country);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string json = streamReader.ReadToEnd();

            streamReader.Close();

            if (int.Parse(json) == -99)
                Console.WriteLine("Country not found!");
            else
                Console.WriteLine("Country time REST: " + UnixTimeStampToDateTime(currentTime + Double.Parse(json)));
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

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }
        
    }
}
