using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace WhiteMvvm.Validations
{
    public class LimitLengthRule<T> : IValidationRule<T>
    {
        private readonly int _maxLenght;

        public LimitLengthRule(int maxLenght)
        {
            _maxLenght = maxLenght;
        }
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            if (value is string str)
            {
                return str.Length < _maxLenght;
            }

            return false;
        }
    }
}
