using FoodApp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Infrastructure.ExceptionHandler
{
    public static class DBExceptionHandler
    {
        public static void HandleIt(Exception ex)
        {   
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ResetColor();
            if (ex.InnerException?.Message.Contains("violation of primary key constraint 'PK_ApplicationUsers'") == true)
            {

                throw new DuplicateUsernameException();

            }
            else
            {
                throw new UnknownException();
            }
        }
    }
}
