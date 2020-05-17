using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WhiteMvvm.Validations
{
    public class PhoneNumberFormat<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value==null||string.IsNullOrEmpty(value.ToString()))
                return false;
            //Regex phoneRegex = new Regex(@"^(\+|\d{2})\d{10,12}$");
            //Regex phoneRegex = new Regex(@"^(\d{2})\d{10,12}$");
            Regex phoneRegex = new Regex(@"^(?:00)[0-9\\s.\\/-]{6,20}$");
            var x = phoneRegex.IsMatch(value.ToString());
            return x;
        }
    }
}
