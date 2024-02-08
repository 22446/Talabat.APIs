namespace Talabat.APIs.DTO
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? SecretKey { get; set; }
    }
}
