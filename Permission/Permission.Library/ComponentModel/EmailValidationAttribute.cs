namespace Permission.Library.ComponentModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class EmailValidationAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return string.Format("{0}不符合Email的格式", name);
        }
        public override bool IsValid(object value)
        {
            string str = value.ToString();
            if (!Regex.IsMatch(str, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"))
            {
                return false;
            }
            return true;
        }
    }
}