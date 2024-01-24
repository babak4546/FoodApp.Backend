using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        :base("ایمیل وارد شده صحیح نیست")
        {
            
        }
    }
}
