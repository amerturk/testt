using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class SelectPickerRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            return value != null;
        }
    }
}
