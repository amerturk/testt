using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class PasswordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
          
            if (!(value is string str))
                return false;
            return str.Any(char.IsUpper) &&
                   str.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1 &&
                   str.Any(char.IsUpper) &&
                   str.Any(char.IsLower) &&
                   str.Any(char.IsDigit);
        }
    }
}
