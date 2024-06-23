using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CommonLayer
{
    public class Validations
    {
        public static void ValidationEntityArray(string[] entityString)
        {
            if (entityString.Any(x => string.IsNullOrEmpty(x)))
                throw new ArgumentNullException();
            try
            {
                int.Parse(entityString[0]);
            }catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (entityString.Length == 3)
                throw new ArgumentOutOfRangeException("Invalid input length. You need to enter email address.");
            if (!new EmailAddressAttribute().IsValid(entityString[3]))
                throw new FormatException();
        }
    }
}
