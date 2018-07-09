using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementClient.Models.Requests
{
    public class AddRequest
    {
        public string ProductCode { get; set; }
        public uint Quantity { get; set; }
    }
}
