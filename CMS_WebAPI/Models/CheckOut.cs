namespace CMS_WebAPI.Models
{
    public class CheckOut
    {
        public int CheckOutId { get; set; }
        public int StudentId { get; set; }
        public int ClassroomId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount {  get; set; }
        public string Note { get; set; }
        public decimal Total { get; set; }
    }
}
