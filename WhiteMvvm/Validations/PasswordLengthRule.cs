using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class PasswordLengthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value is string str)
            {
                return str.Length >= 5 && str.Length <= 8;
            }
            return false;
        }
    }
}
