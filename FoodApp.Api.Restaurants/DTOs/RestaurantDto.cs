namespace FoodApp.Api.Restaurants.DTOs
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public bool IsActive { get; set; }
    }
}
