using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TimeZoner
{
    [ServiceContract]
    public interface ISOAP
    {

        [OperationContract]
        string GetCountryTime(string country);

        [OperationContract]
        string GetISOTime(string countryISO);
        
    }
}
