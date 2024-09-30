namespace BookStore.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Bill { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

    }
}
