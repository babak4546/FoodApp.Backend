namespace FoodApp.Api.Food.DTOs
{
    public class UpdateFoodDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Price { get; set; }
        public string? Description { get; set; }

        public bool IsAvailable { get; set; }
        public int Discount { get; set; }
        public int RestaurantId { get; set; }
    }
}
