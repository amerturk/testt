using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class ContainsSpecialCharacterRole<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            return value is string str ? str.IndexOfAny("@#$%^&*_~-£()<>".ToCharArray()) == -1 : false;
        }
    }
}
