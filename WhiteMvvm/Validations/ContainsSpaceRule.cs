using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class ContainsSpaceRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value is string str)
            {
                return !str.Contains(" ");
            }
            return false;
        }
    }
}
