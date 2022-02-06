namespace Web.Models
{
    public class ModelCurrency
    {
        public ModelCurrency() { }
        public int Id { get; set; } 
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public double Rate { get; set; }
    }
}
