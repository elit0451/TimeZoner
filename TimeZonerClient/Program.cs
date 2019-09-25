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

            timeZoner.ReloadGUI();

            bool isRunning = true;
            string input = "";

            do
            {
                if (isRunning)
                    input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    isRunning = false;
                    break;
                }

                // Check if get ISO code
                bool countryCode = input.Length == 2;

                timeZoner.RunSoapAsync(currentTime, countryCode, input);
                timeZoner.RunRest(currentTime, countryCode, input);
                
                Console.ReadKey();
                Console.Clear();
                timeZoner.ReloadGUI();

            } while (isRunning);

        }

        public async void RunSoapAsync(double currentTime, bool countryCode, string country)
        {
            SOAPClient proxy = new SOAPClient();
            // Trim and make into PascalCase
            string countryName = country.Trim().Substring(0, 1).ToUpper() + country.Trim().Substring(1).ToLower();
            string countryTime;

            try
            {
                if (countryCode)
                    countryTime = await proxy.GetISOTimeAsync(countryName);
                else
                    countryTime = await proxy.GetCountryTimeAsync(countryName);

                Console.WriteLine("Time in " + countryName + " (SOAP): " + UnixTimeStampToDateTime(currentTime + Double.Parse(countryTime)));
            }
            catch (FaultException e)
            {
                Console.WriteLine("SOAP: Country not found!");
            }

        }

        public void RunRest(double currentTime, bool countryCode, string country)
        {
            HttpWebRequest request;
            // Trim and make into PascalCase
            string countryName = country.Trim().Substring(0,1).ToUpper() + country.Trim().Substring(1).ToLower();

            if (countryCode)
                request = (HttpWebRequest)WebRequest.Create("https://localhost:44313/api/TimeZone/GetCountryISO/" + countryName);
            else
                request = (HttpWebRequest)WebRequest.Create("https://localhost:44313/api/TimeZone/GetCountryTime/" + countryName);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                string jsonResponse = streamReader.ReadToEnd();

                streamReader.Close();

                Console.WriteLine("Time in " + countryName + " (REST): " + UnixTimeStampToDateTime(currentTime + Double.Parse(jsonResponse)));
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

        private void ReloadGUI()
        {
            string title = @" ____  ____  __  __  ____    ____  _____  _  _  ____  ____ 
(_  _)(_  _)(  \/  )( ___)  (_   )(  _  )( \( )( ___)(  _ \
  )(   _)(_  )    (  )__)    / /_  )(_)(  )  (  )__)  )   /
 (__) (____)(_/\/\_)(____)  (____)(_____)(_)\_)(____)(_)\_)
                ";
            Console.WriteLine(title);
            Console.WriteLine("Welcome to the amazing Time Zoner!");
            Console.WriteLine("\tType in the name (or ISO) of a country to see what's the time there...");
            Console.WriteLine("\t\t For closing the application just type in -> exit\n");
            Console.Write("Country: ");
        }

    }
}
