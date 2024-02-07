using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Exceptions
{
    public class RestaurantNotFoundException : Exception
    
    {
        public RestaurantNotFoundException()
            :base("رستوران مورد نظر یافت نشد")
        {
            
        }
    }
}
