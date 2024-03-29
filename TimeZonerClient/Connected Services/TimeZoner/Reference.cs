﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     //
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeZoner
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TimeZoner.ISOAP")]
    public interface ISOAP
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISOAP/GetCountryTime", ReplyAction="http://tempuri.org/ISOAP/GetCountryTimeResponse")]
        System.Threading.Tasks.Task<string> GetCountryTimeAsync(string country);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISOAP/GetISOTime", ReplyAction="http://tempuri.org/ISOAP/GetISOTimeResponse")]
        System.Threading.Tasks.Task<string> GetISOTimeAsync(string countryISO);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface ISOAPChannel : TimeZoner.ISOAP, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class SOAPClient : System.ServiceModel.ClientBase<TimeZoner.ISOAP>, TimeZoner.ISOAP
    {
        
    /// <summary>
    /// Implement this partial method to configure the service endpoint.
    /// </summary>
    /// <param name="serviceEndpoint">The endpoint to configure</param>
    /// <param name="clientCredentials">The client credentials</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SOAPClient() : 
                base(SOAPClient.GetDefaultBinding(), SOAPClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.Soap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SOAPClient(EndpointConfiguration endpointConfiguration) : 
                base(SOAPClient.GetBindingForEndpoint(endpointConfiguration), SOAPClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SOAPClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SOAPClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SOAPClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SOAPClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SOAPClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> GetCountryTimeAsync(string country)
        {
            return base.Channel.GetCountryTimeAsync(country);
        }
        
        public System.Threading.Tasks.Task<string> GetISOTimeAsync(string countryISO)
        {
            return base.Channel.GetISOTimeAsync(countryISO);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Soap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.Soap))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:49851/TimeZoner.svc/soap");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SOAPClient.GetBindingForEndpoint(EndpointConfiguration.Soap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SOAPClient.GetEndpointAddress(EndpointConfiguration.Soap);
        }
        
        public enum EndpointConfiguration
        {
            
            Soap,
        }
    }
}
