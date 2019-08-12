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
            ErrorMessage = "����д��ȷ���ֻ�����";
        }
    }
}