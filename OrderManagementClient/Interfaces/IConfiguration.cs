using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementClient.Interfaces
{
    public interface IConfiguration
    {
        string GetAPIVersion();
        string GetBaseUrl();
        string GetQueryString(string type, params string[] values);
    }
}
