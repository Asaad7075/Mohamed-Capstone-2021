using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfPresentation.Validators
{
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validators for address input using bindings.
    /// </summary>
    public class AddressBindingValidators { }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validator for street addresses.
    /// </summary>
    public class StreetAddressValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
            {
                return new ValidationResult(false, "");
            }
            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "Please enter a street address.");
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validator for city.
    /// </summary>
    public class CityValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "");
            }
            string valueString = value.ToString();
            if (String.IsNullOrWhiteSpace(valueString))
            {
                return new ValidationResult(false, "Value cannot be empty.");
            }
            else
            {
                
                if (valueString.Any(char.IsDigit))
                {
                    return new ValidationResult(false, "Cities cannot contain numbers.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validator for state.
    /// </summary>
    public class StateValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return new ValidationResult(false, "");
            }
            string valueString = value.ToString();
            if (String.IsNullOrWhiteSpace(valueString))
            {
                return new ValidationResult(false, "Please enter a state.");
            }
            else
            {

                if (valueString.Any(char.IsDigit))
                {
                    return new ValidationResult(false, "State cannot contain numbers.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validator for zipcode. Uses a regex found on stackoverflow.
    /// </summary>
    public class ZipCodeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
            {
                return new ValidationResult(false, "");
            }
            string valueString = value.ToString();
            string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            if (String.IsNullOrWhiteSpace(valueString))
            {
                return new ValidationResult(false, "Please enter a zipcode.");
            }
            else
            {
                if(!Regex.Match(valueString, _usZipRegEx).Success)
                {
                    return new ValidationResult(false, "Use a correctly formated US ZipCode.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
