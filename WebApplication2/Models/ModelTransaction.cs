namespace Web.Models
{
    public class ModelTransaction
    {
        public int Id { get; set; }
        public string SKU { get; set; } = "";
        public double Amount { get; set; }
        public string Currency { get; set; } = "";
    }
}
