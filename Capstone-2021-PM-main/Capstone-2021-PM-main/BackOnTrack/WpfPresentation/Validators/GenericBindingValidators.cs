using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfPresentation.Validators
{
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validators for inputs using bindings.
    /// </summary>
    public class GenericBindingValidators
    {
    }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validator for numbered IDs.
    /// </summary>
    public class NumberedIDValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null)
            {
                return new ValidationResult(false, "ID field cannot be empty");
            }
            else
            {
                int valueNum;
                if(!int.TryParse(value.ToString(), out valueNum))
                {
                    return new ValidationResult(false, "ID field should contain a whole number");
                }
                else
                {
                    if(valueNum < 10000)
                    {
                        return new ValidationResult(false, "ID field numbers start at 10000 and go up");
                    }
                }
            }
            return ValidationResult.ValidResult;
        }
    }
    /// <summary>
    /// Jakub Kawski
    /// 2020/03/01
    /// 
    /// Validators for ticket Notes.
    /// </summary>
    public class TicketNotesValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return ValidationResult.ValidResult;
            }
            else
            {               
                if(value.ToString().Length > 500) 
                {
                    return new ValidationResult(false, "Notes cannot be longer then 500 characters.");
                }
            }
            return ValidationResult.ValidResult;
        }
    }

}
