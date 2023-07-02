using System.ComponentModel.DataAnnotations;

namespace ContactsManager
{
    public class CheckIsraelNumber : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? phone, ValidationContext validationContext)
        {
            var phone_number = phone as string;

            if (string.IsNullOrWhiteSpace(phone_number))
            {
                return new ValidationResult("Phone number cannot be empty");
            }

            if (phone_number.Length < 10 || phone_number.Length > 14)
            {
                return new ValidationResult("Phone number shuold contain from 10 to 14 symbols");
            }

            if (!phone_number.StartsWith("+972") && !phone_number.StartsWith("0"))
            {
                return new ValidationResult("Phone number should begin with +972 or 0");
            }

            if (((phone_number[0] == '+') && (!phone_number[1..].All(char.IsDigit))) ||
                ((phone_number[0] != '+') && (!phone_number.All(char.IsDigit))))
            {
                return new ValidationResult("Phone number should contain only digits or +");
            }   

            return ValidationResult.Success;
        }
    }
}

