using System.ComponentModel.DataAnnotations;

namespace CustomInputAndValidation.CustomValidation
{
    public class DateTimeFromNowAttribute : ValidationAttribute
    {
        public DateTimeFromNowAttribute() { }

        public string GetErrorMessage() => "Ngày phải là tương lai";

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var datetime = (DateTime)value;

            if (DateTime.Compare(datetime, DateTime.Now) < 0) { return new ValidationResult(GetErrorMessage()); }
            else { return ValidationResult.Success; }

        }
    }
}
