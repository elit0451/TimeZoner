using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoner
{
    [ServiceContract]
    public interface IREST
    {
        [OperationContract]
        [WebGet(UriTemplate = "/getCountryTime/{country}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetCountryTime(string country);

        [OperationContract]
        [WebGet(UriTemplate = "/getISOTime/{countryISO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetISOTime(string countryISO);
    }
}
