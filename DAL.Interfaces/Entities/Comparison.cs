namespace DAL.Interfaces.Entities
{
    public class Comparison
    {
        public int ComparisonId { get; set; }

        public string UserName { get; set; }
        
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}