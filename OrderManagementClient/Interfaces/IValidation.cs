using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagementClient.Interfaces
{
    public interface IValidation
    {
        (string messages, bool isValid) Validate(object value);
    }
}
