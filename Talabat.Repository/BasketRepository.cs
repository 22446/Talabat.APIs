using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repository;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase Database;

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            Database = Redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await Database.KeyDeleteAsync(BasketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var Basket = await Database.StringGetAsync(BasketId);
            if (Basket.IsNull) return null;
            else return JsonSerializer.Deserialize<CustomerBasket>(Basket);   
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var SerializedBasket = JsonSerializer.Serialize(Basket);
            var BasketUpdateCreate= await Database.StringSetAsync(Basket.Id, SerializedBasket, TimeSpan.FromDays(1));
            if (!BasketUpdateCreate) return null;
            return await GetBasketAsync(Basket.Id);
        }
    }
}
