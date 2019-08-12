namespace Permission.Library.ComponentModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]

    public class CellPhoneValidationAttribute : RegularExpressionAttribute
    {
        public CellPhoneValidationAttribute()
            : base(@"1[\d]{10}")
        {
            ErrorMessage = "请添写正确的手机号码";
        }
    }
}