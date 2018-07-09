using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagementClient.Models.Business
{
    public enum OrderStatus
    {
        Initialized = 1,
        Submited = 2,
        Processing = 3,
        Fulfilled = 4
    }
}
