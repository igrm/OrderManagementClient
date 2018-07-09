using OrderManagementClient.Models.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementClient.Models.Requests
{
    public class InitializeSameAddressRequest: InitializeRequestBase
    {
        public Address Address { get; set; }
    }
}
