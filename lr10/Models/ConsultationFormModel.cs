using System;
using System.ComponentModel.DataAnnotations;

namespace lr10.Models
{
    public class ConsultationFormModel
    {
        [Required(ErrorMessage = "Поле 'Ім'я прізвище' є обов'язковим")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Поле 'Email' є обов'язковим")]
        [EmailAddress(ErrorMessage = "Некоректний формат Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Бажана дата консультації' є обов'язковим")]
        [FutureDate(ErrorMessage = "Дата консультації має бути в майбутньому")]
        [NotOnWeekend(ErrorMessage = "Консультація не може проходити у вихідні")]
        [NotOnMondayIfBasics(ErrorMessage = "Консультація щодо продукту 'Основи' не може проходити по понеділках")]
        public DateTime ConsultationDate { get; set; }

        [Required(ErrorMessage = "Поле 'Продукт' є обов'язковим")]
        public string Product { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && (DateTime)value > DateTime.Now;
        }
    }

    public class NotOnWeekendAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is DateTime))
                return false;

            var date = (DateTime)value;
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
    }

    public class NotOnMondayIfBasicsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var model = (ConsultationFormModel)validationContext.ObjectInstance;
            var product = model.Product;

            if (product == "Основи" && ((DateTime)value).DayOfWeek == DayOfWeek.Monday)
            {
                return new ValidationResult("Консультація щодо продукту 'Основи' не може проходити по понеділках");
            }

            return ValidationResult.Success;
        }
    }

}
