using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Zarabizi.Models.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DNIValidationAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "";

        public DNIValidationAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessageString;
        }

        public override bool IsValid(object value)
        {
            const string correspondencia = "TRWAGMYFPDXBNJZSQVHLCKE";
            string numberText = string.Empty;
            int number;
            char leter;
            string dni = value.ToString().ToUpper();
            numberText = dni.Substring(0, dni.Length - 1);
            if (numberText.Length >= 7 && int.TryParse(numberText, out number))
            {
                leter = correspondencia[number % 23];
                if (string.Concat(numberText, leter) == value.ToString().ToUpper())
                    return true;
            }
            return false;
        }
    }
}