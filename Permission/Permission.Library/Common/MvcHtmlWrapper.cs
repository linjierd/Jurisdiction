

namespace Permission.Library.Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// MvcHtmlString��װ��
    /// �������ö�Html���滻���������ʱͳһ������ʾHtml
    /// </summary>
    public class MvcHtmlWrapper : IHtmlString
    {
        /// <summary>
        /// ����һ��MvcHtmlWrapper�������MvcHtmlWrapper��ֱ�ӷ��أ�
        /// �������������������MvcHtmlWrapper�󷵻�
        /// </summary>
        /// <param name="str">IHtmlString���ͣ�ʵ����ToHtmlString�ӿڵ�����</param>
        /// <returns></returns>
        public static MvcHtmlWrapper Create(IHtmlString str)
        {
            Contract.Requires(str != null);
            if (str is MvcHtmlWrapper)
                return str as MvcHtmlWrapper;
            if (str is MvcHtmlString)
                return new MvcHtmlWrapper(str);
            return new MvcHtmlWrapper(str);
        }

        IHtmlString HtmlStringInterface { get; set; }

        private string _htmlString;

        /// <summary>
        /// ��ȡMvcHtmlString�����ɵ�Html�ַ���
        /// </summary>
        public string HtmlString
        {
            get { return _htmlString ?? (_htmlString = HtmlStringInterface.ToHtmlString()); }
        }

        /// <summary>
        /// �����滻��Dict
        /// </summary>
        public List<Tuple<string, string>> ReplaceDict { get; set; }

        /// <summary>
        /// ����MvcHtmlString��װ��
        /// </summary>
        /// <param name="str">MvcHtmlString��ʵ����������Ϊ��</param>
        MvcHtmlWrapper(IHtmlString str)
        {
            Contract.Requires(str != null);
            HtmlStringInterface = str;
            ReplaceDict = new List<Tuple<string, string>>();
        }
 
        /// <summary>
        /// ��ToHtmlString��������д�� ���HtmlString�����ݣ������滻����������滻
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString()
        {
            return ToString();
        }

        /// <summary>
        /// ��ToString��������д�� ���HtmlString�����ݣ������滻����������滻
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder(HtmlString);
            foreach (var item in ReplaceDict)
            {
                if (!string.IsNullOrEmpty(item.Item1))
                {
                    sb.Replace(item.Item1, item.Item2);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// ����滻ԭ��
        /// </summary>
        /// <param name="item1">���滻���ַ���</param>
        /// <param name="item2">�滻Ϊ���ַ���</param>
        internal void Add(string item1, string item2)
        {
            ReplaceDict.Add(Tuple.Create(item1, item2));
        }
    }
}