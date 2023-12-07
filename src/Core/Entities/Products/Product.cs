using System.ComponentModel.DataAnnotations;

namespace Core.Entities.Products
{
    public class Product
    {
        private Rating _rating = new Rating();
        public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;
        public double Price { get; set; } = 0;
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Category { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Image { get; set; }  =  string.Empty;
        public Rating Rating 
        { 
            get { return _rating; } 
            set { _rating = value ?? new Rating(); } 
        }
    }
}