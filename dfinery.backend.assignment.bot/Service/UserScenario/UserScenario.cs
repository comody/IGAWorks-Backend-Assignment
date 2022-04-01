using System;
using System.Threading.Tasks;
using dfinery.backend.assignment.bot.API;
using dfinery.backend.assignment.bot.Models;
using dfinery.backend.assignment.bot.Service;
namespace dfinery.backend.assignment.bot
{
    public class UserScenario
    {
        private readonly UserActionModule userActionModule;
        private readonly Random r;

        private int scenarioCount = 10;

        private readonly string [] userNames = { "Mahesh Chand", "Jeff Prosise", "Dave McCarter", "Allen O'neill",
            "Monica Rathbun", "Henry He", "Raj Kumar", "Mark Prime",
            "Rose Tracey", "Mike Crown"
        };

        private readonly string[] productNames = { "맛동산", "새우깡", "꼬깔콘", "오! 감자", "몽쉘", "꼬북칩", "빠새" };
        
        public UserScenario(IEventAPIMessenger eventAPI)
        {
            userActionModule = new UserActionModule(eventAPI);
            r = new Random();
        }

        public async Task PurchaseScenario(User user, Product product)
        {
            await userActionModule.Login(user);

            while (true)
            {
                var amount = r.Next(1, 5);

                if (!await userActionModule.Purchase(user, product, amount))
                {
                    await userActionModule.Logout(user);

                    break;
                }
            }
        }

        public User GetRandomUser()
        {
            var userName = userNames[r.Next(userNames.Length)];
            var point = r.Next(1000, 10000);

            return new User(userName, point);
        }

        public Product GetRandomProduct()
        {
            var product_name = productNames[r.Next(productNames.Length)];
            var stock = r.Next(5, 40);
            var price = r.Next(100, 2500);

            return new Product(product_name, stock, price);
        }

        public async Task Run()
        {
            for (int i = 0; i < scenarioCount; i++)
            {
                var user = GetRandomUser();
                var product = GetRandomProduct();

                await PurchaseScenario(user, product);
            }
        }
    }
}
