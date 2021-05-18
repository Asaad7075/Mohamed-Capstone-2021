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
    /// Chantal Shirley
    /// Created: 2021/03/25
    /// 
    /// Validators for the Vehicle
    /// modification view model.
    /// </summary>
    public class VehicleBindingValidators { }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/25
    /// 
    /// Validators for the License
    /// plate number.
    /// </summary>
    public class LicensePlateValidator : ValidationRule
    {
        private bool _propertyErrors = true;
        public bool PropertyErrors
        {
            get => _propertyErrors;
            protected internal set
            {
                _propertyErrors = value;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/09
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object property, CultureInfo cultureInfo)
        {
            _propertyErrors = false;

            if (property == null)
            {
                return new ValidationResult(false, "License plate information must be filled out.");
            }
            else if (property.ToString().ContainsEmptyString())
            {
                return new ValidationResult(false, "License plate information must be filled out.");
            }
            else if (property.ToString().Length > 10)
            {
                return new ValidationResult(false, "License plate numbers max length 10.");
            }
            else if (!Regex.IsMatch(property.ToString(), @"^[a-zA-Z0-9]+$")) // From beginning to end, inclusive of the entire string ensure alpha-numeric characters
            {
                return new ValidationResult(false, "License plate numbers must be alpha-numeric.");
            }

            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/25
    /// 
    /// Validators for the mileage.
    /// </summary>
    public class MileageValidator : ValidationRule
    {
        private bool _propertyErrors = true;
        public bool PropertyErrors
        {
            get => _propertyErrors;
            protected internal set
            {
                _propertyErrors = value;
            }
        }


        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/03/25
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            _propertyErrors = false;

            if (value == null)
            {
                return new ValidationResult(false, "Mileage must be filled out.");
            }
            else if (value.ToString().ContainsEmptyString())
            {
                return new ValidationResult(false, "Mileage must be filled out.");
            }
            else if (!value.ToString().IsAnInteger())
            {
                return new ValidationResult(false, "Mileage ranges must be an integer.");
            }
            else if (Int64.Parse(value.ToString()) > (2147483647))
            {
                return new ValidationResult(false, "Mileage must be no bigger than 2147483647.");
            }
            else if (Int32.Parse(value.ToString()) < 0)
            {
                return new ValidationResult(false, "You cannot enter negative mileage.");
            }

            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }
}
