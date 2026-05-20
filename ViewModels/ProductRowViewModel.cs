namespace ProductStorageMvc.ViewModels
{
    public class ProductRowViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalValue { get; set; }
    }
}
