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
        [FaultContract(typeof(ErrorData))]
        int GetCountryTime(string country);

        [OperationContract]
        [FaultContract(typeof(ErrorData))]
        int GetISOTime(string countryISO);
        
    }

    [DataContract]
    public class ErrorData
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }
}
