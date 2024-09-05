using System.ComponentModel.DataAnnotations;

namespace CustomInputAndValidation.CustomValidation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateTimeMustBeAfterAttribute : ValidationAttribute
    {
        public DateTimeMustBeAfterAttribute(string targetPropertyName)
            => TargetPropertyName = targetPropertyName;

        public string TargetPropertyName { get; }

        public string GetErrorMessage(string propertyName) => $"'{propertyName}' must be after '{TargetPropertyName}'.";
        //public string GetErrorMessage(string propertyName) => "Phải lựa thời gian ở tương lai.";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            var targetValue = validationContext.ObjectInstance
                .GetType()
                .GetProperty(TargetPropertyName)
                ?.GetValue(validationContext.ObjectInstance, null);

            if ((DateTime?)value < (DateTime?)targetValue)
            {
                var propertyName = validationContext.MemberName ?? string.Empty;
                return new ValidationResult(GetErrorMessage(propertyName), new[] { propertyName });
            }

            return ValidationResult.Success;
        }
    }
}
