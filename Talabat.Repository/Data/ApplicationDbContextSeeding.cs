using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.Repository.Data
{
    public static class ApplicationDbContextSeeding
    {
        public async static Task DataseedAsync(ApplicationDbContext dbContext)
        {
            if (!dbContext.productBrands.Any())
            {
                var DataSeedBrand = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(DataSeedBrand);
                if (Brands?.Count > 0)
                    foreach (var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.productTypes.Any())
            {
                var DataSeedType = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(DataSeedType);
                if (Types?.Count > 0)
                    foreach (var Type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    }
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.products.Any())
            {
                var DataSeedProduct = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(DataSeedProduct);
                if (Products?.Count > 0)
                    foreach (var Product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(Product);
                    }
                await dbContext.SaveChangesAsync();
            }
            if (!dbContext.delivery_Methods.Any())
            {
                var DataSeedDeleviry= File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var Deliverys = JsonSerializer.Deserialize<List<Delivery_Method>>(DataSeedDeleviry);
                if (Deliverys?.Count > 0)
                    foreach (var delivery in Deliverys)
                    {
                        await dbContext.Set<Delivery_Method>().AddAsync(delivery);
                    }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
