using System.ComponentModel.DataAnnotations;

namespace Vacation_Request_Tracker.Helper
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime >= DateTime.Today;
            }
            return false;
        }
    }

    public class GreaterThanAttribute : ValidationAttribute
    {
        private string _comparisonProperty;

        public GreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (DateTime)value;
            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null)
            {
                throw new ArgumentException("Property with this name not found");
            }
            var comparisonValue = (DateTime)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
