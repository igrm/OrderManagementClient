using OrderManagementClient.Models.Business;
using OrderManagementClient.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementClient.Interfaces
{
    public interface IClient
    {
        IEnumerable<Order> Get();
        Order Get(int orderId);
        int InitializeSameAddress(InitializeSameAddressRequest initValues);
        int InitializeSeparateAddresses(InitializeSeparateAddressesRequest initValues);
        void Add(int orderId, AddRequest request);
        void Remove(int orderId, string productCode);
        void SetQuantity(int orderId, string productCode, uint quantity);
        void Complete(int orderId);
        void ClearOut(int orderId);
    }
}
