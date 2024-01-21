using FoodApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class ApplicationUser 
    {
        public string? Username { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        [JsonIgnore]
        public ApplicationUserType Type { get; set; }
        public string? Email { get; set; }
        public bool Verified { get; set; }=false;
        [JsonIgnore]
        public string VerificationCode { get; set; }


        public string UserType
        {
         get{
                switch (this.Type)
                {
                    case ApplicationUserType.SystemAdmin:
                        return "مدیریت سیستم";
                        break;
                    case ApplicationUserType.RestaurantOwner:
                        return "رستوران دار";
                        break;
                    case ApplicationUserType.Customer:
                        return "مشتری" ;
                        break;
                    default:
                        break;
                }
                return "ناشناخته";
            }
        }
    }
}
