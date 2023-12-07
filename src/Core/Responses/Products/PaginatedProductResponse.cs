namespace Core.Responses.Products
{
    public class ProductListResponse
    {
        public List<ProductResponse>? Products { get; set; }
        public int TotalCount { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}
