using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Exceptions
{
    public class DuplicateUsernameException:Exception
    {
        public DuplicateUsernameException()
        : base("نام و یا شماره همراه تکراری می باشد")
        {
            
        }

    }
}
