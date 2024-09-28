namespace MVCWithJQueryAJAX.Models
{
    public class OrderModel
    {
        public string OrderId { get; set; }
        public int  UnitPrice { get; set; }
        public int  Quantity { get; set; }
        public DateTime OrderDate = DateTime.Now;
        public decimal CalculatedResult { get; set; }
    }
}
