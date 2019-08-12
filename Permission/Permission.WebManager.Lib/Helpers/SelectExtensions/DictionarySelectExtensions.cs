using Permission.Bll.SystemManager;
using Permission.Library.Web.Helpers.SelectExtensions;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Permission.WebManager.Lib.Helpers.SelectExtensions
{
    public static class DictionarySelectExtensions
    {
        /// <summary>
        /// 字典选择
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="dtTypeKey"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString DictionarySelectFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,string dtTypeKey,
            IDictionary<string, object> dic = null)
        {
            List<DictionaryTableDb> dtList = DictionaryTableBll.Instance.GetListInDtType(dtTypeKey);
            IDictionary<string, string> diList = dtList.ToDictionary(c => c.dt_key, c => c.dt_name);
            IDictionary<string, string> bDi = new Dictionary<string, string>();
            bDi.Add("", "请选择");
            return SelectCommonExtensions.CommonSelectorFor(htmlHelper, expression, dic, diList, bDi);
        }


        /// <summary>
        /// 字典选择
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name"></param>
        /// <param name="dtTypeKey"></param>
        /// <param name="value"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString DictionarySelect(this HtmlHelper htmlHelper, string name,string dtTypeKey, object value = null,
            IDictionary<string, object> dic = null)
        {
            List<DictionaryTableDb> dtList = DictionaryTableBll.Instance.GetListInDtType(dtTypeKey);
            IDictionary<string, string> diList = dtList.ToDictionary(c => c.dt_key, c => c.dt_name);
            IDictionary<string, string> bDi = new Dictionary<string, string>();
            bDi.Add("", "请选择");
            return SelectCommonExtensions.CommonSelector(htmlHelper, name, value, dic, diList, bDi);
        }
    }
}
