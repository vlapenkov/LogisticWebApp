//------------------------------------------------------------------------------
// <автоматически создаваемое>
//     Этот код создан программой.
//     //
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторного создания кода.
// </автоматически создаваемое>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="www.yst.ru/ServiceCarrier", ConfigurationName="ServiceReference1.ServiceCarrierPortType")]
    public interface ServiceCarrierPortType
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="www.yst.ru/ServiceCarrier#ServiceCarrier:CarrierAnswer", ReplyAction="*")]
        System.Threading.Tasks.Task<ServiceReference1.CarrierAnswerResponse> CarrierAnswerAsync(ServiceReference1.CarrierAnswerRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CarrierAnswerRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CarrierAnswer", Namespace="www.yst.ru/ServiceCarrier", Order=0)]
        public ServiceReference1.CarrierAnswerRequestBody Body;
        
        public CarrierAnswerRequest()
        {
        }
        
        public CarrierAnswerRequest(ServiceReference1.CarrierAnswerRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="www.yst.ru/ServiceCarrier")]
    public partial class CarrierAnswerRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string CarrierId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string GuidIn1S;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.DateTime ArrivalDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public System.DateTime UnloadDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int Cost;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string Car;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string Driver;
        
        public CarrierAnswerRequestBody()
        {
        }
        
        public CarrierAnswerRequestBody(string CarrierId, string GuidIn1S, System.DateTime ArrivalDate, System.DateTime UnloadDate, int Cost, string Car, string Driver)
        {
            this.CarrierId = CarrierId;
            this.GuidIn1S = GuidIn1S;
            this.ArrivalDate = ArrivalDate;
            this.UnloadDate = UnloadDate;
            this.Cost = Cost;
            this.Car = Car;
            this.Driver = Driver;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CarrierAnswerResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CarrierAnswerResponse", Namespace="www.yst.ru/ServiceCarrier", Order=0)]
        public ServiceReference1.CarrierAnswerResponseBody Body;
        
        public CarrierAnswerResponse()
        {
        }
        
        public CarrierAnswerResponse(ServiceReference1.CarrierAnswerResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="www.yst.ru/ServiceCarrier")]
    public partial class CarrierAnswerResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public CarrierAnswerResponseBody()
        {
        }
        
        public CarrierAnswerResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public interface ServiceCarrierPortTypeChannel : ServiceReference1.ServiceCarrierPortType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.0")]
    public partial class ServiceCarrierPortTypeClient : System.ServiceModel.ClientBase<ServiceReference1.ServiceCarrierPortType>, ServiceReference1.ServiceCarrierPortType
    {
        
    /// <summary>
    /// Реализуйте этот разделяемый метод для настройки конечной точки службы.
    /// </summary>
    /// <param name="serviceEndpoint">Настраиваемая конечная точка</param>
    /// <param name="clientCredentials">Учетные данные клиента.</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ServiceCarrierPortTypeClient(EndpointConfiguration endpointConfiguration) : 
                base(ServiceCarrierPortTypeClient.GetBindingForEndpoint(endpointConfiguration), ServiceCarrierPortTypeClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            this.ChannelFactory.Credentials.UserName.UserName = "ClientRazumov";
            this.ChannelFactory.Credentials.UserName.Password = "js4nHnY8";
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceCarrierPortTypeClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ServiceCarrierPortTypeClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceCarrierPortTypeClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ServiceCarrierPortTypeClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ServiceCarrierPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceReference1.CarrierAnswerResponse> ServiceReference1.ServiceCarrierPortType.CarrierAnswerAsync(ServiceReference1.CarrierAnswerRequest request)
        {
            return base.Channel.CarrierAnswerAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.CarrierAnswerResponse> CarrierAnswerAsync(string CarrierId, string GuidIn1S, System.DateTime ArrivalDate, System.DateTime UnloadDate, int Cost, string Car, string Driver)
        {
            ServiceReference1.CarrierAnswerRequest inValue = new ServiceReference1.CarrierAnswerRequest();
            inValue.Body = new ServiceReference1.CarrierAnswerRequestBody();
            inValue.Body.CarrierId = CarrierId;
            inValue.Body.GuidIn1S = GuidIn1S;
            inValue.Body.ArrivalDate = ArrivalDate;
            inValue.Body.UnloadDate = UnloadDate;
            inValue.Body.Cost = Cost;
            inValue.Body.Car = Car;
            inValue.Body.Driver = Driver;
            return ((ServiceReference1.ServiceCarrierPortType)(this)).CarrierAnswerAsync(inValue);
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
            if ((endpointConfiguration == EndpointConfiguration.ServiceCarrierSoap))
            {
                //   System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding(System.ServiceModel.BasicHttpSecurityMode.TransportCredentialOnly);
                result.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Basic;
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceCarrierSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ServiceCarrierSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://37.1.84.50:8080/YST/ws/servicecarrier");
            }
            if ((endpointConfiguration == EndpointConfiguration.ServiceCarrierSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://37.1.84.50:8080/YST/ws/servicecarrier");
            }
            throw new System.InvalidOperationException(string.Format("Не удалось найти конечную точку с именем \"{0}\".", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ServiceCarrierSoap,
            
            ServiceCarrierSoap12,
        }
    }
}
