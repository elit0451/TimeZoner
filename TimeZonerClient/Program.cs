using System;
using System.IO;
using System.Net;
using TimeZoner;

namespace TimeZonerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().RunSoapAsync();
            new Program().RunRest();
            Console.ReadKey();
        }

        public async void RunSoapAsync()
        {
            SOAPClient proxy = new SOAPClient();
            string countryTime = await proxy.GetCountryTimeAsync("Denmark");
            Console.WriteLine(countryTime);
        }

        public void RunRest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:49851/TimeZoner.svc/rest/getCountryTime/Denmark");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
            string json = streamReader.ReadToEnd();

            streamReader.Close();

            Console.WriteLine(json);
        }
    }
}
