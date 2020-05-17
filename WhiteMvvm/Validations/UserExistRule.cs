using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WhiteMvvm.Validations;

public class UserExistRule<T> : IValidationRule<T>
{
    public string ValidationMessage { get; set; }

    public bool Check(T value)
    {
        if (value == null)
        {
            return true;
        }

        return !(value as string is "The username is already in use.");
    }
}