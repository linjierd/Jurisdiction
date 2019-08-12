namespace Permission.Library.ComponentModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using System.Globalization;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class CharacterValidationAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' 要包含 {1}。";
        private readonly object _typeId = new object();

        public CharacterValidationAttribute() : base(DefaultErrorMessage) { }

        public bool HasLetter { get; set; }
        public bool HasNumeric { get; set; }
        public bool HasChineseCharacter { get; set; }
        public bool HasSpecialCharacter { get; set; }

        public override object TypeId { get { return _typeId; } }

        public override string FormatErrorMessage(string name)
        {
            var list = new List<string>();
            if (HasLetter) list.Add("英文字母");
            if (HasNumeric) list.Add("数字");//TODO zoujian 字符验证 float,int,uint
            if (HasChineseCharacter) list.Add("汉字");
            if (HasSpecialCharacter) list.Add("特殊符号");
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, string.Join("、", list.ToArray()));
        }

        public override bool IsValid(object value)
        {
            string str = value.ToString();
            if (HasLetter && !Regex.IsMatch(str, "[a-zA-Z]+"))
            {
                return false;
            }
            if (HasNumeric && !Regex.IsMatch(str, "\\d+"))
            {
                return false;
            }
            if (HasChineseCharacter && !Regex.IsMatch(str, "[\u4e00-\u9fa5]+"))
            {
                return false;
            }
            if (HasSpecialCharacter && !Regex.IsMatch(str, "[^\\w]+"))
            {
                return false;
            }
            return true;
        }
    }
}
