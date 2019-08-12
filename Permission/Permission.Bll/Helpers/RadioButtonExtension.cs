using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using Permission.Library.Tools;
using Permission.Model.Common.GlobalCode;
namespace Permission.Bll.Helpers
{
    public static class RadioButtonExtension
    {

        public static MvcHtmlString RadioButtonTrueOrFlaseReadonlyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadonly, IDictionary<string, object> dic)
        {
            return RadioButtonReadonlyFor(htmlHelper, expression, isReadonly, dic, EnumHelper<IntTrueOrFalse>.GetDictionary().OrderByDescending(c => c.Key).ToDictionary(c => c.Key, c => c.Value));

        }
        /// <summary>
        /// 具有不可用效果的单选框
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="isReadonly"></param>
        /// <param name="dic"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonReadonlyFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, bool isReadonly, IDictionary<string, object> dic, IDictionary<int, string> binding)
        {
            if (isReadonly)
            {
                if (dic != null && dic.Keys.Contains("style"))
                {
                    string strStyle = dic["style"].ToString();
                    if (strStyle.Length > 0 && strStyle[dic["style"].ToString().Length - 1] != ';')
                        strStyle = strStyle + ";";
                    dic["style"] = strStyle + "background-color:#efefef;";
                }
                else
                {
                    if (dic == null) dic = new Dictionary<string, object>();
                    dic.Add("style", "background-color:#efefef;");
                }

                dic.Add("readonly", isReadonly.ToString());
            }
            if (binding != null)
            {
                StringBuilder redioSB = new StringBuilder();
                foreach (var d in binding)
                {
                    redioSB.Append(string.Concat(d.Value, ": ", htmlHelper.RadioButtonFor(expression, d.Key, dic).ToHtmlString(), " "));
                }
                return new MvcHtmlString(redioSB.ToString());
            }
            return new MvcHtmlString("");
        }


    }
}
