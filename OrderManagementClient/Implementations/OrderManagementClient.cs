using OrderManagementClient.Models.Business;
using OrderManagementClient.Models.Requests;
using System;
using System.Collections.Generic;
using OrderManagementClient.Interfaces;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace OrderManagementClient
{
    public class OrderManagementClient : IClient
    {
        private readonly HttpClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IValidation _validation;

        public OrderManagementClient(IConfiguration configuration, IValidation validation)
        {
            _configuration = configuration;
            _validation = validation;
            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri(configuration.GetBaseUrl());
        }

        public IEnumerable<Order> Get()
        {
            var response = _restClient.GetAsync(_configuration.GetQueryString("Get", _configuration.GetAPIVersion())).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException(response.ReasonPhrase);
            }
            string result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IEnumerable<Order>>(result);
        }

        public Order Get(int orderId)
        {
            var response = _restClient.GetAsync(_configuration.GetQueryString("GetById", _configuration.GetAPIVersion(), orderId.ToString())).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ArgumentException(response.ReasonPhrase);
            }
            string result = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Order>(result);
        }

        public int InitializeSameAddress(InitializeSameAddressRequest initValues)
        {
            var validationResult = _validation.Validate(initValues);
            int orderId = -1;

            if (validationResult.isValid)
            {
                var response = _restClient.PostAsJsonAsync<InitializeSameAddressRequest>(_configuration.GetQueryString("InitializeSameAddress", _configuration.GetAPIVersion()),initValues).Result;

                if(response.StatusCode == HttpStatusCode.Created)
                {
                    orderId = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).orderId;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException(validationResult.messages);
            }
            return orderId;
        }

        public int InitializeSeparateAddresses(InitializeSeparateAddressesRequest initValues)
        {
            var validationResult = _validation.Validate(initValues);
            int orderId = -1;

            if (validationResult.isValid)
            {
                var response = _restClient.PostAsJsonAsync<InitializeSeparateAddressesRequest>(_configuration.GetQueryString("InitializeSameAddress", _configuration.GetAPIVersion()), initValues).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    orderId = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).orderId;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException(validationResult.messages);
            }
            return orderId;
        }

        public void Add(int orderId, AddRequest addValues)
        {
            var validationResult = _validation.Validate(addValues);

            if (validationResult.isValid)
            {
                var response = _restClient.PostAsJsonAsync<AddRequest>(_configuration.GetQueryString("Add", _configuration.GetAPIVersion(), orderId.ToString()), addValues).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException(validationResult.messages);
            }
        }

        public void Remove(int orderId, string productCode)
        {
            if(!String.IsNullOrEmpty(productCode))
            {
                var response = _restClient.PostAsync(_configuration.GetQueryString("Remove", _configuration.GetAPIVersion(), orderId.ToString(), productCode), null).Result;

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException("ProductCode is null or empty! Please provide a valid value.");
            }
        }

        public void SetQuantity(int orderId, string productCode, uint quantity)
        {
            if (!String.IsNullOrEmpty(productCode) && quantity > 0)
            {
                var response = _restClient.PutAsJsonAsync<uint>(_configuration.GetQueryString("SetQuantity", _configuration.GetAPIVersion(), orderId.ToString(), productCode), quantity).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException("Invalid arguments! Please check ProductCode and Quantity.");
            }
        }

        public void Complete(int orderId)
        {
            if (orderId > 0)
            {
                var response = _restClient.PostAsJsonAsync(_configuration.GetQueryString("Complete", _configuration.GetAPIVersion(), orderId.ToString()), orderId).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException("OrderId must be a positive number.");
            }
        }

        public void ClearOut(int orderId)
        {
            if (orderId > 0)
            {
                var response = _restClient.PostAsJsonAsync(_configuration.GetQueryString("ClearOut", _configuration.GetAPIVersion(), orderId.ToString()), orderId).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            else
            {
                throw new ArgumentException("OrderId must be a positive number.");
            }
        }
    }
}
