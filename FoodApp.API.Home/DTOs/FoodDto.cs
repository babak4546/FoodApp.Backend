namespace FoodApp.API.Home.DTOs
{
    public class FoodDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string? RestaurantTitle { get; set; }

    }
}
