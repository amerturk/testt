using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Validations
{
    public class MinimumLengthRule<T> : IValidationRule<T>
    {
        private readonly int _minimumLengthRule;

        public MinimumLengthRule(int minimumLengthRule)
        {
            _minimumLengthRule = minimumLengthRule;
        }
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value is string str)
            {
                return str.Length >= _minimumLengthRule;
            }

            return false;
        }
    }
}
