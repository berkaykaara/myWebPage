namespace myWebPageClient.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public byte[] ImageData { get; set; } // Fotoğrafın binary verisi
        public string ImageType { get; set; } // MIME türü (örn. "image/jpeg")

    }
}
