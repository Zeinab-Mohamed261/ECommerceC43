namespace Domain.Models.BasketModule
{
    public class BasketItems  //مش عايزاه يتخزن في DB
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}