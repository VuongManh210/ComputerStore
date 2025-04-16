namespace ComputerStore.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; } // Để hiển thị
        public decimal Price { get; set; } // Để hiển thị
        public string ImageData { get; set; } // Để hiển thị
    }
}