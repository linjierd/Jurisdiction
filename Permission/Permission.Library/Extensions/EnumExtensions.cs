using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reflection;
    using Attributes;
    using System.Collections.Generic;

    /// <summary>
    /// 枚举的扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        #region 获取属性

        /// <summary>
        /// 获取Enum的GlobalCode标记中的描述信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetGlobalCode(this Enum e)
        {
            var attr = GetGlobalCodeAttribute(e);
            return attr != null ? attr.Description : e.ToString();
        }

        /// <summary>
        /// 获取Enum的GlobalCode标记中的颜色信息
        /// </summary>
        /// <param name="e"></param>
        /// <returns>返回表示颜色的字符串</returns>
        public static string GetDisplayColor(this Enum e)
        {
            var attr = GetGlobalCodeAttribute(e);

            if (attr != null)
                return attr.Color;

            return string.Empty;
        }

        /// <summary>
        /// 返回扩展名称，tj需要
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetExtName(this Enum e)
        {
            var attr = GetGlobalCodeAttribute(e);
            if (attr != null)
                return attr.ExtName;
            return string.Empty;
        }
        #endregion

        #region 获取Enum的GlobalCodeAttribute

        public static GlobalCodeAttribute GetGlobalCodeAttribute(Enum e)
        {
            Contract.Requires(e != null);
            Type type = e.GetType();
            MemberInfo[] memInfo = type.GetMember(e.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(GlobalCodeAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((GlobalCodeAttribute)attrs[0]);
                }
            }
            return null;
        }

        #endregion



        public static IList<string> GetNames(this Enum e)
        {
            IList<string> enumNames = new List<string>();
            foreach (FieldInfo fi in e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumNames.Add(fi.Name);
            }
            return enumNames;
        }

        public static Array GetValues(this Enum e)
        {
            List<int> enumValues = new List<int>();
            foreach (FieldInfo fi in e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((int)Enum.Parse(e.GetType(), fi.Name, false));
            }
            return enumValues.ToArray();
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
            {
                EnumEntryDescriptionAttribute[] temp2 = (EnumEntryDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumEntryDescriptionAttribute), false);
                if (temp2 != null && temp2.Length > 0)
                {
                    return temp2[0].DisplayName;
                }
                else
                {
                    return value.ToString();
                }
            }
        }

        public static List<string> GetAllEnumDescriptions<TEnum>() where TEnum : struct
        {
            List<string> result = new List<string>();
            Type temptype = typeof(TEnum);
            foreach (FieldInfo fi in temptype.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                EnumDescriptionAttribute[] temp = (EnumDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                string tempd = "";
                if (temp != null && temp.Length > 0)
                {
                    tempd = temp[0].Description;
                }
                else
                {
                    EnumEntryDescriptionAttribute[] temp2 = (EnumEntryDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumEntryDescriptionAttribute), false);
                    if (temp2 != null && temp2.Length > 0)
                    {
                        tempd = temp2[0].DisplayName;
                    }
                    else
                    {
                        tempd = fi.ToString();
                    }
                }
                result.Add(tempd);
            }
            return result;
        }

        public static EnumEntity GetEnumEntity<TEnum>() where TEnum : struct
        {
            EnumEntity result = new EnumEntity();
            try
            {
                Type temptype = typeof(TEnum);
                List<EnumItem> tempitems = new List<EnumItem>();
                foreach (FieldInfo fi in temptype.GetFields(BindingFlags.Static | BindingFlags.Public))
                {
                    EnumDescriptionAttribute[] temp = (EnumDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                    string tempd = "";
                    bool vis = true;
                    if (temp != null && temp.Length > 0)
                    {
                        tempd = temp[0].Description;
                        vis = temp[0].VisibleInList;
                    }
                    else
                    {
                        EnumEntryDescriptionAttribute[] temp2 = (EnumEntryDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumEntryDescriptionAttribute), false);
                        if (temp2 != null && temp2.Length > 0)
                        {
                            tempd = temp2[0].DisplayName;
                            vis = temp2[0].Visible;
                        }
                        else
                        {
                            tempd = fi.Name;
                            vis = true;
                        }
                    }
                    tempitems.Add(new EnumItem() { EnumField = fi.Name, Description = tempd, EnumValue = (int)Enum.Parse(temptype, fi.Name, false), Visible = vis });
                }
                result.Name = temptype.Name;
                result.Items = tempitems;
            }
            catch
            {
            }
            return result;
        }

        public static List<EnumItem> GetAllEnumItems(this Enum e)
        {
            List<EnumItem> result = new List<EnumItem>();
            foreach (FieldInfo fi in e.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                EnumDescriptionAttribute[] temp = (EnumDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                string tempd = "";
                bool vis = true;
                if (temp != null && temp.Length > 0)
                {
                    tempd = temp[0].Description;
                    vis = temp[0].VisibleInList;
                }
                else
                {
                    EnumEntryDescriptionAttribute[] temp2 = (EnumEntryDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumEntryDescriptionAttribute), false);
                    if (temp2 != null && temp2.Length > 0)
                    {
                        tempd = temp2[0].DisplayName;
                        vis = temp2[0].Visible;
                    }
                    else
                    {
                        tempd = e.ToString();
                        vis = true;
                    }
                }
                result.Add(new EnumItem() { EnumField = fi.Name, Description = tempd, EnumValue = (int)Enum.Parse(e.GetType(), fi.Name, false), Visible = vis });
            }
            return result;
        }

        public static List<EnumItem> GetAllEnumItems<TEnum>() where TEnum : struct
        {
            List<EnumItem> result = new List<EnumItem>();
            Type temptype = typeof(TEnum);
            foreach (FieldInfo fi in temptype.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                EnumDescriptionAttribute[] temp = (EnumDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                string tempd = "";
                bool vis = true;
                if (temp != null && temp.Length > 0)
                {
                    tempd = temp[0].Description;
                    vis = temp[0].VisibleInList;
                }
                else
                {
                    EnumEntryDescriptionAttribute[] temp2 = (EnumEntryDescriptionAttribute[])fi.GetCustomAttributes(typeof(EnumEntryDescriptionAttribute), false);
                    if (temp2 != null && temp2.Length > 0)
                    {
                        tempd = temp2[0].DisplayName;
                        vis = temp2[0].Visible;
                    }
                    else
                    {
                        tempd = fi.ToString();
                        vis = true;
                    }
                }
                result.Add(new EnumItem() { EnumField = fi.Name, Description = tempd, EnumValue = (int)Enum.Parse(temptype, fi.Name, false), Visible = vis });
            }
            return result;
        }
    }
}
