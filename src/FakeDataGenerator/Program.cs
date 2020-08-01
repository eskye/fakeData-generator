using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bogus;

namespace FakeDataGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Randomizer.Seed = new Random(54815148);
            var billingDetailsFaker = new Faker<BillingDetails>()
                .RuleFor(x => x.CustomerName, x=>x.Person.FullName)
                .RuleFor(x => x.Email, x => x.Person.Email)
                .RuleFor(x => x.AddressLine, x => x.Address.StreetAddress())
                .RuleFor(x => x.City, x => x.Address.City())
                .RuleFor(x => x.Country, x => x.Address.Country())
                .RuleFor(x => x.Phone, x => x.Person.Phone)
                .RuleFor(x => x.PostCode, x => x.Address.ZipCode());

            var orderFaker = new Faker<Order>()
                    .RuleFor(r => r.Id, Guid.NewGuid)
                    .RuleFor(r => r.Currency, r => r.Finance.Currency().Code)
                    .RuleFor(r => r.Price, r => r.Finance.Amount(30, 1000))
                    .RuleFor(r => r.BillingDetails, r => billingDetailsFaker);

            foreach (var order in orderFaker.GenerateForever())
            {
                var text = JsonSerializer.Serialize(order);
                Console.WriteLine(text);
                await Task.Delay(1000);
            }

           
        }
    }
}
