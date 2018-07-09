using OrderManagementClient.Implementations;
using OrderManagementClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementClient
{
    public static class OrderManagementClientFactory
    {
        public static IClient CreateOrderManagementClient()
        {
            IClient client = new OrderManagementClient(new Configuration(), new Validation());
            return client;
        }
    }
}
