using OrderManagementClient.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrderManagementClient.Implementations
{
    public class Validation : IValidation
    {
        public (string messages, bool isValid) Validate(object value)
        {
            var context = new ValidationContext(value, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            string messages = string.Empty;

            var isValid = Validator.TryValidateObject(value, context, results);

            if (!isValid)
            {
                messages = string.Join("; ", results);
            }

            return (messages, isValid);
        }
    }
}
