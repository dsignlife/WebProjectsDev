namespace Webshop.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public decimal Price { get; set; }
        public string ImageThumbnailUrl { get; set; }
    }
}