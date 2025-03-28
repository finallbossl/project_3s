using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Helpers
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime endDate) return ValidationResult.Success;

            var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (comparisonProperty == null) return ValidationResult.Success;

            var startDate = (DateTime?)comparisonProperty.GetValue(validationContext.ObjectInstance);

            if (startDate != null && endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}