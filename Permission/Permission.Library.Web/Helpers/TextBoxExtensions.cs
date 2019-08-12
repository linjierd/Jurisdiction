using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Web.Helpers
{
    using System.Linq.Expressions;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    public static class TextBoxExtensions
    {
        /// <summary>
        /// 具有属性width和maxlength的TextBox
        /// </summary>
        public static MvcHtmlString RestrictTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int width, int maxLength)
        {
            return htmlHelper.TextBoxFor(expression, new { style = string.Format("width:{0}px", width), @maxLength = maxLength });
        }
        /// <summary>
        /// 具有不可用效果的多行文本框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isReadonly"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString TextareaReadonly(this HtmlHelper htmlHelper, string name, object value = null, bool isReadonly = false, IDictionary<string, object> dic = null)
        {

            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            if (isReadonly)
            {
                if (dic.Keys.Contains("style"))
                {
                    string strStyle = dic["style"].ToString();
                    if (strStyle.Length > 0 && strStyle[dic["style"].ToString().Length - 1] != ';')
                        strStyle = strStyle + ";";
                    dic["style"] = strStyle + "background-color:#efefef;";
                }
                else
                {
                    dic.Add("style", "background-color:#efefef;");
                }

                dic.Add("readonly", isReadonly.ToString());
            }
            return htmlHelper.TextArea(name, value.ToString(), dic);

        }
        /// <summary>
        /// 具有不可用效果的文本框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isReadonly"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString TextareaReadonlyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadonly = false, IDictionary<string, object> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            if (isReadonly)
            {
                if (dic.Keys.Contains("style"))
                {
                    string strStyle = dic["style"].ToString();
                    if (strStyle.Length > 0 && strStyle[dic["style"].ToString().Length - 1] != ';')
                        strStyle = strStyle + ";";
                    dic["style"] = strStyle + "background-color:#efefef;";
                }
                else
                {

                    dic.Add("style", "background-color:#efefef;");
                }

                dic.Add("readonly", isReadonly.ToString());
            }

            return htmlHelper.TextAreaFor(expression, dic);
        }





        /// <summary>
        /// 具有不可用效果的文本框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isReadonly"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString TextReadonly(this HtmlHelper htmlHelper, string name, object value = null, bool isReadonly = false, IDictionary<string, object> dic = null)
        {

            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            if (isReadonly)
            {
                if (dic.Keys.Contains("style"))
                {
                    string strStyle = dic["style"].ToString();
                    if (strStyle.Length > 0 && strStyle[dic["style"].ToString().Length - 1] != ';')
                        strStyle = strStyle + ";";
                    dic["style"] = strStyle + "background-color:#efefef;";
                }
                else
                {
                    dic.Add("style", "background-color:#efefef;");
                }

                dic.Add("readonly", isReadonly.ToString());
            }
            return htmlHelper.TextBox(name, value, dic);

        }
        /// <summary>
        /// 具有不可用效果的文本框
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isReadonly"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString TextReadonlyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadonly = false, IDictionary<string, object> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<string, object>();
            }
            if (isReadonly)
            {
                if (dic.Keys.Contains("style"))
                {
                    string strStyle = dic["style"].ToString();
                    if (strStyle.Length > 0 && strStyle[dic["style"].ToString().Length - 1] != ';')
                        strStyle = strStyle + ";";
                    dic["style"] = strStyle + "background-color:#efefef;";
                }
                else
                {

                    dic.Add("style", "background-color:#efefef;");
                }

                dic.Add("readonly", isReadonly.ToString());
            }

            return htmlHelper.TextBoxFor(expression, dic);
        }

    }
}
