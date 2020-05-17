using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class EmailExistRule<T> : IValidationRule<T>
    {
    
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return true;
            }
            return !(value as string is "User Already Exists");
        }
    }
}
