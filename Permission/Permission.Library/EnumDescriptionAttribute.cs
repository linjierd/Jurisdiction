

namespace Permission.Library
{
    using System;
    using System.Reflection;
    using System.Collections;
    /// <summary>
    /// 把枚举值按照指定的文本显示
    /// <example>
    /// 一般通过枚举值的ToString()可以得到变量的文本，但是有时候需要的到与之对应的更充分的文本，这个类帮助达到此目的
    /// EnumDescriptionAttribute.GetEnumText(typeof(MyEnum));
    /// EnumDescriptionAttribute.GetFieldText(MyEnum.Two);
    /// EnumDescriptionAttribute.GetFieldTexts(typeof(MyEnum)); 
    /// 绑定到下拉框：
    /// comboBox1.DataSource = EnumDescriptionAttribute.GetFieldTexts(typeof(OrderStateEnum));
    /// comboBox1.ValueMember = "EnumValue";
    /// comboBox1.DisplayMember = "EnumDisplayText";
    /// comboBox1.SelectedValue = (int)OrderStateEnum.Finished;  //选中值
    /// source code from: codeproject.com
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public class EnumDescriptionAttribute : Attribute
    {
        private string description;
        private int enumRank;
        private bool visibleInList = true;
        internal FieldInfo fieldInfo;

        /// <summary>
        /// 描述枚举值
        /// </summary>
        /// <param name="description">描述内容</param>
        /// <param name="enumRank">排列顺序</param>
        public EnumDescriptionAttribute(string description, int enumRank)
        {
            this.description = description;
            this.enumRank = enumRank;
        }

        /// <summary>
        /// 描述枚举值，默认排序为5
        /// </summary>
        /// <param name="description">描述内容</param>
        public EnumDescriptionAttribute(string description)
            : this(description, 5) { }

        public string Description
        {
            get { return this.description; }
        }

        public int EnumRank
        {
            get { return enumRank; }
        }

        public int EnumValue
        {
            get { return fieldInfo == null ? -1 : (int)fieldInfo.GetValue(null); }
        }

        public string FieldName
        {
            get { return fieldInfo == null ? string.Empty : fieldInfo.Name; }
        }
        /// <summary>
        /// 列表中是否显示
        /// </summary>
        public bool VisibleInList
        {
            get { return visibleInList; }
            set { visibleInList = value; }
        }

        #region  对枚举描述属性的解释相关函数

        /// <summary>
        /// 排序类型
        /// </summary>
        public enum SortBy
        {
            /// <summary>
            ///按枚举顺序默认排序
            /// </summary>
            Default,
            /// <summary>
            /// 按描述值排序
            /// </summary>
            Description,
            /// <summary>
            /// 按排序熵
            /// </summary>
            Rank
        }

        private static System.Collections.Hashtable cachedEnum = new Hashtable();


        /// <summary>
        /// 得到对枚举的描述文本
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static string GetEnumText(Type enumType)
        {
            EnumDescriptionAttribute[] eds = (EnumDescriptionAttribute[])enumType.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (eds.Length != 1) return string.Empty;
            return eds[0].Description;
        }

        /// <summary>
        /// 获得指定枚举类型中，指定值的描述文本。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static string GetFieldText(object enumValue)
        {
            EnumDescriptionAttribute[] descriptions = GetFieldTexts(enumValue.GetType(), SortBy.Default);
            foreach (EnumDescriptionAttribute ed in descriptions)
            {
                if (ed.fieldInfo.Name == enumValue.ToString()) return ed.Description;
            }
            return string.Empty;
        }


        /// <summary>
        /// 获得指定枚举类型中，指定值的是否在类表中显示。
        /// </summary>
        /// <param name="enumValue">枚举值，不要作任何类型转换</param>
        /// <returns>描述字符串</returns>
        public static bool GetFieldVisibleInList(object enumValue)
        {
            EnumDescriptionAttribute[] descriptions = GetFieldTexts(enumValue.GetType(), SortBy.Default);
            foreach (EnumDescriptionAttribute ed in descriptions)
            {
                if (ed.fieldInfo.Name == enumValue.ToString()) return ed.VisibleInList;
            }
            return true;
        }

        /// <summary>
        /// 得到枚举类型定义的所有文本，按定义的顺序返回
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <returns>所有定义的文本</returns>
        public static EnumDescriptionAttribute[] GetFieldTexts(Type enumType)
        {
            return GetFieldTexts(enumType, SortBy.Rank);
        }

        /// <summary>
        /// 得到枚举类型定义的所有文本
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <param name="enumType">枚举类型</param>
        /// <param name="sortType">指定排序类型</param>
        /// <returns>所有定义的文本</returns>
        public static EnumDescriptionAttribute[] GetFieldTexts(Type enumType, SortBy sortType)
        {
            EnumDescriptionAttribute[] descriptions = null;
            //缓存中没有找到，通过反射获得字段的描述信息
            if (cachedEnum.Contains(enumType.FullName) == false)
            {
                FieldInfo[] fields = enumType.GetFields();
                ArrayList edAL = new ArrayList();
                foreach (FieldInfo fi in fields)
                {
                    object[] eds = fi.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
                    if (eds.Length != 1) continue;
                    ((EnumDescriptionAttribute)eds[0]).fieldInfo = fi;
                    edAL.Add(eds[0]);
                }

                cachedEnum.Add(enumType.FullName, (EnumDescriptionAttribute[])edAL.ToArray(typeof(EnumDescriptionAttribute)));
            }
            descriptions = (EnumDescriptionAttribute[])cachedEnum[enumType.FullName];
            //if ( descriptions.Length <= 0 ) throw new NotSupportedException("枚举类型[" + enumType.Name + "]未定义属性EnumValueDescription");
            if (descriptions.Length <= 0)
            {
                return descriptions;
            }
            //按指定的属性冒泡排序
            for (int m = 0; m < descriptions.Length; m++)
            {
                //默认就不排序了
                if (sortType == SortBy.Default) break;

                for (int n = m; n < descriptions.Length; n++)
                {
                    EnumDescriptionAttribute temp;
                    bool swap = false;

                    switch (sortType)
                    {
                        case SortBy.Default:
                            break;
                        case SortBy.Description:
                            if (string.Compare(descriptions[m].Description, descriptions[n].Description) > 0) swap = true;
                            break;
                        case SortBy.Rank:
                            if (descriptions[m].EnumRank > descriptions[n].EnumRank) swap = true;
                            break;
                    }

                    if (swap)
                    {
                        temp = descriptions[m];
                        descriptions[m] = descriptions[n];
                        descriptions[n] = temp;
                    }
                }
            }

            return descriptions;
        }

        #endregion

        public override string ToString()
        {
            return this.description;
        }
    }
}
