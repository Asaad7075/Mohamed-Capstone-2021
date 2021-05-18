using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Created: 2021/03/09
    /// 
    /// Validators for the drivers license view model.
    /// </summary>
    public class DriversLicenseBindingValidator { }


    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/09
    /// 
    /// Validator for drivers license number.
    /// </summary>
    public class LicenseNumberValidator : ValidationRule
    {
        private bool _propertyErrors;
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
        /// Verified regex blog post suggestions at https://www.syncfusion.com/blogs/post/form-validation-of-input-controls-in-wpf.aspx
        /// against Microsoft's API https://docs.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex?view=net-5.0
        /// </summary>
        /// <param name="property"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object property, CultureInfo cultureInfo)
        {
            _propertyErrors = false;

            if (property == null)
            {
                return new ValidationResult(false, "Forms must be fully filled out to add Driver's License information.");
            }
            else if (property.ToString().ContainsEmptyString())
            {
                return new ValidationResult(false, "Forms must be fully filled out to add Driver's License information.");
            }
            else if (property.ToString().Length < 6)
            {
                return new ValidationResult(false, "Drivers License numbers must be at least 6 characters in length.");
            }
            else if (!Regex.IsMatch(property.ToString(), @"^[a-zA-Z0-9]+$")) // From beginning to end, inclusive of the entire string ensure alpha-numeric characters
            {
                return new ValidationResult(false, "Drivers License numbers must be alpha-numeric.");
            }
            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/09
    /// 
    /// Validator for employee ID.
    /// </summary>
    public class EmployeeIDValidator : ValidationRule
    {
        private bool _propertyErrors;
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
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            _propertyErrors = false;

            if (value == null)
            {
                return new ValidationResult(false, "Forms must be fully filled out to add Driver's License information.");
            }
            else if (value.ToString().ContainsEmptyString())
            {
                return new ValidationResult(false, "Forms must be fully filled out to add Driver's License information.");
            }
            else if (!value.ToString().IsAnInteger())
            {
                return new ValidationResult(false, "Employee ID's are within the range of 1000000-2147483647.");
            }
            else if (value.ToString().Length > 10)
            {
                return new ValidationResult(false, "Employee ID's must be 7-10 digits in length starting at 1000000.");
            }
            else if (Int64.Parse(value.ToString()) > (2147483647))
            {
                return new ValidationResult(false, "Employee ID's must be no bigger than 2147483647.");
            }
            else if (Int32.Parse(value.ToString()) < 1000000)
            {
                return new ValidationResult(false, "Employee ID's must be larger than 1000000.");
            }

            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/11
    /// 
    /// Validator for license Issue date.
    /// </summary>
    public class LicenseIssuedValidator : ValidationRule
    {
        private bool _propertyErrors;
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
        /// Created: 2021/03/10
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date;

            _propertyErrors = false;
            if (value == null)
            {
                return new ValidationResult(false, "Please enter a valid date.");
            }
            else if (DateTime.TryParse(value.ToString(), out date) != true)
            {
                return new ValidationResult(false, "Please enter a valid date.");
            }
            else if (date.DateInFuture())
            {
                return new ValidationResult(false, "License issued date cannot be blank or in the future.");
            }

            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }

    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/03/10
    /// 
    /// Validator for license expiry date.
    /// </summary>
    public class LicenseExpiryValidator : ValidationRule
    {
        private bool _propertyErrors;
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
        /// Created: 2021/03/10 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date;
            _propertyErrors = false;

            if (value == null)
            {
                return new ValidationResult(false, "Please enter a valid date.");
            }
            if (DateTime.TryParse(value.ToString(), out date) != true)
            {
                return new ValidationResult(false, "Please enter a valid date.");
            }
            else if (date.DateTodayOrInPast())
            {
                return new ValidationResult(false, "License expiry date cannot be today or in the past.");
            }

            _propertyErrors = true; // Serves as a flag of whether there are errors
            return ValidationResult.ValidResult;
        }
    }
}
