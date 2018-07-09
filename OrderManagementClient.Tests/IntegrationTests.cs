using OrderManagementClient.Models.Business;
using OrderManagementClient.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OrderManagementClient.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void CheckBasket()
        {
            var client = OrderManagementClientFactory.CreateOrderManagementClient();
            var order = client.Get();
            Assert.True(order.Count() > 0);
        }

        [Fact]
        public void CompleteLifecycle()
        {
            var client = OrderManagementClientFactory.CreateOrderManagementClient();
            var orderId = client.InitializeSameAddress(new InitializeSameAddressRequest()
            {
                Client = new Client()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ClientCode = "4829",
                    BirthDate = DateTime.Now.Date.AddDays(-9600),
                    Gender = Gender.Male,
                    Contacts = new List<Contact>()
                                                          {
                                                              new Contact()
                                                              {
                                                                  ContactType = ContactType.Email,
                                                                  Value = "test@test.test"
                                                              }
                                                          }
                },
                Address = new Address()
                {
                    Country = "DE",
                    City = "Berlin",
                    AddressLine = "Wittenauer Straﬂe",
                    State = "Berlin zzz",
                    Zip = "10115"
                },
                PaymentMethod=PaymentMethod.CreditCard,
                CurrencyCode = "EUR",
                DiscountRate = 0.1m
            });

            client.Add(orderId, new AddRequest() { ProductCode = "PRODUCT-1", Quantity = 2 });
            client.Add(orderId, new AddRequest() { ProductCode = "PRODUCT-2", Quantity = 3 });

            client.SetQuantity(orderId, "PRODUCT-2", 2);

            var order = client.Get(orderId);

            Assert.Equal(600, order.OrderAmount);

        }
    }
}
