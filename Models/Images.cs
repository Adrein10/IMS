namespace IMS.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
