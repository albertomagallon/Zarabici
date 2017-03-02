using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Zarabizi.Models.Validation
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class PostalCodeValidationAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "";

        public PostalCodeValidationAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessageString;
        }

        public override bool IsValid(object value)
        {
            string cadena = value.ToString();
            int number;
            if (cadena.Length == 5 && int.TryParse(cadena, out number))
            {
                return true;
            }
            return false;
        }
    }
}