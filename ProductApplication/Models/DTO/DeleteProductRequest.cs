namespace ProductApplication.Models.DTO
{
    public class DeleteProductRequest
    {
        public string Name { get; set; }
        public int quantity { get; set; }
        public decimal Price { get; set; }
        public string status { get; set; }
    }
}
