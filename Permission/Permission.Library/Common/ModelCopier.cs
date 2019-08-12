namespace Permission.Library.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public static class ModelCopier {

        /// <summary>
        /// 从集合from复制元素到集合to，集合to的内容将被清空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static void CopyCollection<T>(IEnumerable<T> from, ICollection<T> to) {
            if (from == null || to == null || to.IsReadOnly) {
                return;
            }
            to.Clear();
            foreach (var element in from) {
                to.Add(element);
            }
        }

        /// <summary>
        /// 从form对象复制属性值到to对象
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="exceptProperties">排除属性的列表</param>
        public static void CopyModel(object from, object to, params string[] exceptProperties)
        {
            if (from == null || to == null) return;
                
            if (exceptProperties == null) exceptProperties = new string[] { };
            var fromProperties = TypeDescriptor.GetProperties(from);
            var toProperties = TypeDescriptor.GetProperties(to);

            foreach (PropertyDescriptor fromProperty in fromProperties)
            {
                if (exceptProperties.Contains(fromProperty.Name)) continue;

                PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true /* ignoreCase */);
                if (toProperty != null && !toProperty.IsReadOnly)
                {

                    bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);

                    bool liftedValueType = (isDirectlyAssignable) ? false : (Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType);

                    if (isDirectlyAssignable || liftedValueType)
                    {
                        object fromValue = fromProperty.GetValue(from);
                        object toValue = toProperty.GetValue(to);
                        if ((isDirectlyAssignable || (fromValue != null && liftedValueType))
                            && (fromValue != null && !fromValue.Equals(toValue)))
                        {
                            if (toProperty.PropertyType == typeof(DateTime) && fromValue.Equals(DateTime.MinValue))
                                continue;
                            
                            toProperty.SetValue(to, fromValue);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 实例化类型，并从某对象Copy属性值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="from"></param>
        /// <returns></returns>
        public static TEntity CreateInstanceFrom<TEntity>(object from)
        {
            var newObj = (TEntity)Activator.CreateInstance(typeof(TEntity));
            CopyModel(from, newObj);
            return newObj;
        }
    }
}
