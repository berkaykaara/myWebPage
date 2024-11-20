namespace myWebPageAPI.DTO
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; } // Yüklenen dosya
    }
}
