using System.Collections.Generic;
using System.Threading.Tasks;
using dfinery.backend.assignment.bot.API;
using dfinery.backend.assignment.bot.Models;
using dfinery.backend.assignment.Models;

namespace dfinery.backend.assignment.bot.Service
{
    public class UserActionModule
    {
        private readonly IEventAPIMessenger eventAPIMessenger;

        public UserActionModule(IEventAPIMessenger eventAPIMessenger)
        {
            this.eventAPIMessenger = eventAPIMessenger;
        }

        public async Task<User> Login(User user)
        {
            await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.LOG_IN, new Dictionary<string, object> { { "username", user.username }, { "point", user.point } }));

            return user;
        }

        public async Task<bool> Purchase(User user, Product product, int amount)
        {
            var is_success = true;

            try
            {
                await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.READY_PURCHASE, new Dictionary<string, object> {
                    { "amount", amount },
                    { "product_id", product.product_id },
                    { "price", product.price },
                    { "left_stock", product.stock },
                    { "total_price", amount*product.price },
                    { "left_point", user.point },
                }));

                product.Purchase(amount);

                user.DecreasePoint(amount * product.price);

                await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.COMPLETE_PURCHASE, new Dictionary<string, object> {
                    { "amount", amount },
                    { "product_id", product.product_id },
                    { "price", product.price },
                    { "left_stock", product.stock },
                    { "total_price", amount*product.price },
                    { "left_point", user.point },
                }));

            }
            catch (GoBankruptException)
            {
                await GoBankrupt(user);
                is_success = false;
            }
            catch (OutOfStockException)
            {
                await OutOfStock(user, product, amount);
                is_success = false;
            }

            return is_success;
        }

        public async Task OutOfStock(User user, Product product, int amount)
        {
            await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.OUT_OF_STOCK, new Dictionary<string, object> { { "left_stock", product.stock }, { "requestAmount", amount } }));
        }

        public async Task GoBankrupt(User user)
        {
            await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.GO_BANKRUPT, new Dictionary<string, object> {}));
        }

        public async Task Logout(User user)
        {
            await eventAPIMessenger.SendEventAsync(new EventModel(user, EVENT_TYPE.LOG_OUT, new Dictionary<string, object> { { "username", user.username } }));
        }
    }
}
