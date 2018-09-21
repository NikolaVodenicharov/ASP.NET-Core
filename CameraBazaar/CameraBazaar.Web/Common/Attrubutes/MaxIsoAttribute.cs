namespace CameraBazaar.Web.Common.Attrubutes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MaxIsoAttribute : ValidationAttribute
    {
        private int minimum;
        private int maximum;

        public MaxIsoAttribute(int minimum, int maximum) 
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int maxIso = (int)value;

            if (maxIso < this.minimum ||
                maxIso > this.maximum ||
                (maxIso % 100 != 0))
            {
                return new ValidationResult($"The value must be greater than {this.minimum}, lesser than {this.maximum} and must be devidable by 100");
            }

            return ValidationResult.Success;
        }
    }
}
