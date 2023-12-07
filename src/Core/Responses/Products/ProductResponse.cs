using Core.Entities.Products;

namespace Core.Responses.Products
{
    public class ProductResponse
    {
        private Rating _rating = new Rating();
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        public string? Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; }  =  string.Empty;
        public Rating? Rating 
        { 
            get { return _rating; } 
            set { _rating = value ?? new Rating(); } 
        }
    }
}
