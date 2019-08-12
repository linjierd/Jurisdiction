

namespace Permission.Library.Tools.TypeTools
{
    using System;

    /// <summary>
    /// Type��Ĵ�������
    /// </summary>
    public class TypeUtil
    {
        /// <summary>
        /// ��ȡ�ɿ����͵�ʵ������
        /// </summary>
        /// <param name="conversionType"></param>
        /// <returns></returns>
        public static Type GetUnNullableType(Type conversionType)
        {
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                //����Ƿ��ͷ������ҷ�������ΪNullable<>����Ϊ�ɿ�����
                //��ʹ��NullableConverterת��������ת��
                var nullableConverter = new System.ComponentModel.NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return conversionType;
        }
    }
}