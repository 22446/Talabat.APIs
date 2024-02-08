using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTO
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string productName { get; set; }
        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        [Range(0.1,double.MaxValue,ErrorMessage ="price must be more than 0")]
        public decimal Price { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Must Be Atleat One")]
        public int Quantity { get; set; }
    }
}