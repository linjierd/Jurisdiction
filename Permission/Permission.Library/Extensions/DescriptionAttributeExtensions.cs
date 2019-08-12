using System;
using System.ComponentModel;
using System.Reflection;

namespace Permission.Library
{
    public static class DescriptionAttributeExtensions
    {
        public static string GetDescription(this Enum e)
        {
            Type type = e.GetType();
            MemberInfo[] memInfo = type.GetMember(e.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return e.ToString();
        }
    }
}
