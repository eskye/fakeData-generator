using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bogus;

namespace FakeDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var billingDetailsFaker = new Faker<BillingDetails>()
                .RuleFor(x => x.CustomerName, x=>x.Person.FullName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.AddressLine, x => x.Address.StreetAddress())
                .RuleFor(x => x.City, x => x.Address.City())
                .RuleFor(x => x.Country, x => x.Address.Country())
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.PostCode, x => x.Address.ZipCode());

            var text = JsonSerializer.Serialize(billingDetailsFaker.Generate());

            Console.WriteLine(text);
        }
    }
}
