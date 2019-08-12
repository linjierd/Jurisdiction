namespace Permission.Library.ComponentModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public sealed class EqualsValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' 和 '{1}' 不匹配。";
        private readonly object _typeId = new object();

        public EqualsValidationAttribute(string originalProperty, string confirmProperty)
            : base(DefaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
