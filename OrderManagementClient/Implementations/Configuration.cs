using System;
using System.Collections.Generic;
using System.Text;
using OrderManagementClient.Interfaces;

namespace OrderManagementClient.Implementations
{
    public class Configuration : IConfiguration
    {
        private const string VERSION = "1";
        private const string BASEURL = "https://ordermanagement-prototype.azurewebsites.net";

        private IDictionary<string, string> MAPPING = new Dictionary<string, string>()
        {
            { "Get","/api/v{0}/Basket"},
            { "GetById","/api/v{0}/Basket/{1}"},
            { "InitializeSameAddress","/api/v{0}/Basket/InitializeSameAddress"},
            { "InitializeSeparateAddresses","/api/v{0}/Basket/InitializeSeparateAddresses"},
            { "Add","/api/v{0}/Basket/{1}/Add"},
            { "Remove","/api/v{0}/Basket/{1}/Remove/{2}"},
            { "SetQuantity","/api/v{0}/Basket/{1}/SetQuantity/{2}"},
            { "Complete","/api/v{0}/Basket/{1}/Complete"},
            { "ClearOut","/api/v{0}/Basket/{1}/ClearOut"},
        };

        public string GetAPIVersion()
        {
            return VERSION;
        }

        public string GetBaseUrl()
        {
            return BASEURL;
        }

        public string GetQueryString(string type, params string[] values)
        {
            return String.Format(MAPPING[type], values);
        }
    }
}
