namespace Store.Models
{
    public class Comparison
    {
        public int ComparisonId { get; set; }

        public string UserName { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}