namespace FoodApp.Api.Restaurants.DTOs
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public string? OwnerUsername { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }

    }
}
