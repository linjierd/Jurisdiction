namespace Permission.Library.ComponentModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class TelphoneValidationAttribute : RegularExpressionAttribute
    {
        public TelphoneValidationAttribute()
            : base(@"[\d]{3,4}-*[\d]{4,9}")
        {
            ErrorMessage = "请添写正确的电话号码";
        }
    }
}
