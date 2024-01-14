namespace FoodApp.API.Security.DTOs
{
    public class LoginResultDto
    {
        public string Message { get; set; }
        public Boolean IsOk { get; set; }
        public string Token { get; set; }
    }
}
