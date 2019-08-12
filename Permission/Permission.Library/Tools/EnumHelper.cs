

namespace Permission.Library.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Attributes;
    using Extensions;

    /// <summary>
    /// Enum 辅助方法类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumHelper<T> where T : struct
    {
        #region Get All GlobalCode Attributes

        /// <summary>
        /// 获取T的所有GlobalCode
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, GlobalCodeAttribute>> GetGlobalCodeAttributes()
        {
            return GetGlobalCodeAttributes(typeof(T));
        }
        public static IEnumerable<KeyValuePair<int, GlobalCodeAttribute>> GetGlobalCodeAttributes(Type type)
        {
            var values = Enum.GetValues(type).Cast<Int32>();
            foreach (var value in values)
            {
                var en = (Enum)Enum.Parse(type, value.ToString());
                var attr = EnumExtensions.GetGlobalCodeAttribute(en);
                if (attr == null)
                    continue;
                yield return
                    new KeyValuePair<int, GlobalCodeAttribute>(
                        value,
                        attr
                        );
            }
        }

        #endregion

        #region Dictionary Key=intValue Value=Description

        /// <summary>
        /// 获取指定的枚举类型转换为的Dictionary，其中值做为Key，描述文本做为Value
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetDictionary(bool containsObsolete = false, Enum[] exceptEnums = null)
        {
            return GetDictionary(typeof(T), containsObsolete, exceptEnums);
        }

        public static Dictionary<int, string> GetDictionary(Type type, bool containsObsolete = false, Enum[] exceptEnums = null)
        {
            var attrs = GetGlobalCodeAttributes(type);
            if (exceptEnums != null && exceptEnums.Count() > 0)
            {
                var exceptInts = exceptEnums.Cast<int>();
                attrs = attrs.Where(c => !exceptInts.Contains(c.Key));
            }
            attrs = attrs.OrderBy(c => c.Value.DisplayOrder);
            return attrs.ToDictionary(c => c.Key, c => c.Value.Description);
        }

        #endregion

        #region SelectList value=Key(intValue) text=Value(Description)

        ///// <summary>
        ///// 获取指定枚举转换为的SelectList，value为枚举的值 option文本则为描述文本
        ///// </summary>
        ///// <param name="selectedValue">如果当前选择器有默认值则使用此参数</param>
        ///// <param name="containsObsolete">是否包含作废元素</param>
        ///// <param name="exceptEnums"></param>
        ///// <returns></returns>
        //public static SelectList GetSelectList(object selectedValue = null, bool containsObsolete = false, Enum[] exceptEnums = null)
        //{

        //    return new SelectList(GetDictionary(containsObsolete, exceptEnums), "Key", "Value", selectedValue);
        //}
        /// <summary>
        /// 获取指定枚举转换为的SelectList，value为枚举的值 option文本则为描述文本
        /// </summary>
        /// <param name="selectedValue">如果当前选择器有默认值则使用此参数</param>
        /// <param name="containsObsolete">是否包含作废元素</param>
        /// <param name="exceptEnums"></param>
        /// <returns></returns>
        public static SelectList GetSelectList(object selectedValue = null, bool containsObsolete = false, Enum[] exceptEnums = null, Dictionary<string, string> items = null)
        {

            var selectDisList = items;
            if (selectDisList == null) selectDisList = new Dictionary<string, string>();
            Dictionary<string, string> disList = GetDictionary(containsObsolete, exceptEnums).ToDictionary(c => c.Key.ToString(), c => c.Value);
            foreach (var d in disList)
            {
                selectDisList.Add(d.Key, d.Value);
            }
            SelectList list = new SelectList(selectDisList, "Key", "Value", selectedValue);
            return list;
        }

        #endregion

        #region Convert From int or string to EnumOfT

        /// <summary>
        /// 将值转换成指定枚举 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T GetEnum(int val)
        {
            return (T)Enum.Parse(typeof(T), val.ToString());
        }

        /// <summary>
        /// 将值转换成指定枚举 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T GetEnum(string p)
        {
            return (T)Enum.Parse(typeof(T), p);
        }

        #endregion
    }
}
