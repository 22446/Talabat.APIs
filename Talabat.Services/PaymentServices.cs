using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Aggregate_Order;
using Talabat.Core.Repository;
using Talabat.Core.Services;

namespace Talabat.Services
{
    public class PaymentServices : IPaymentSrevices
    {
        private readonly IConfiguration configuration;
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentServices(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            this.configuration = configuration;
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            var Basket = await basketRepository.GetBasketAsync(BasketId);
            if (Basket is null) return null;
            var Shipingprice = 0M;

            if (Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await unitOfWork.Repository<Core.Entities.Product>().GetById(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                    if (Basket.DeliveryMethodId.HasValue)
                    {
                        var DelivaryMehod = await unitOfWork.Repository<Delivery_Method>().GetById(Basket.DeliveryMethodId.Value);
                        Shipingprice = DelivaryMehod.Cost;
                    }
                }
            }
            var SubTotal = Basket.Items.Sum(item => item.Price * item.Quantity);
            var Services = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(Basket.PaymentIntentId))//create
            {
                var Opttions = new PaymentIntentCreateOptions()
                {
                    Amount = (long?)(SubTotal * 100 + Shipingprice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await Services.CreateAsync(Opttions);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.SecretKey = paymentIntent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long?)(SubTotal * 100 + Shipingprice * 100)
                };
                paymentIntent = await Services.UpdateAsync(Basket.PaymentIntentId, option);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.PaymentIntentId = paymentIntent.ClientSecret;
            }
            await basketRepository.UpdateBasketAsync(Basket);
            return Basket;

        }
    }
}
