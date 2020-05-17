using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhiteMvvm.Utilities;

namespace WhiteMvvm.Validations
{
    public class ValidatableObject<T> : NotifiedObject, IValidity
    {
        private bool _isValid;
        private List<string> _errors;
        private T _value;

        public List<IValidationRule<T>> Validations { get; }

        public List<string> Errors
        {
            get => _errors;
            set { _errors = value; OnPropertyChanged();}
        }

        public T Value
        {
            get => _value;
            set { _value = value;OnPropertyChanged(); }
        }

        public bool IsValid { get; set; }
        public ValidatableObject()
        {
            _isValid = true;
            Errors = new List<string>();
            Validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();
            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);
            Errors = errors.ToList();
            IsValid = !Errors.Any();
            return IsValid;
        }
    }
}
