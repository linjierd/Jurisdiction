using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Permission.Library.Web.Helpers.SelectExtensions;
using Permission.Model.DbModel.System;
using Permission.Bll.SystemManager;

namespace Permission.WebManager.Lib.Helpers.SelectExtensions
{
    public static class DictionaryTypeSelectExtensions
    {
        /// <summary>
        /// 字典类别选择
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString DictionaryTypeSelectFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, 
            IDictionary<string, object> dic = null)
        {
            List<DictionaryTypeTableDb> typeList = DictionaryTypeTableBll.Instance.GetAllList().OrderBy(c=>c.dt_type_orderby).ToList();
            IDictionary<string, string> diTypeList = typeList.ToDictionary(c => c.dt_type_key, c => c.dt_type_name);
            IDictionary<string, string> bDi = new Dictionary<string, string>();
            bDi.Add("", "请选择");
            return SelectCommonExtensions.CommonSelectorFor(htmlHelper, expression, dic,  diTypeList, bDi);
        }


        /// <summary>
        /// 字典类别选择
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static MvcHtmlString DictionaryTypeSelect(this HtmlHelper htmlHelper, string name, object value = null,
            IDictionary<string, object> dic = null)
        {
            List<DictionaryTypeTableDb> typeList = DictionaryTypeTableBll.Instance.GetAllList().OrderBy(c => c.dt_type_orderby).ToList();
            IDictionary<string, string> diTypeList = typeList.ToDictionary(c => c.dt_type_key, c => c.dt_type_name);
            IDictionary<string, string> bDi = new Dictionary<string, string>();
            bDi.Add("", "请选择");
            return SelectCommonExtensions.CommonSelector(htmlHelper,name,value, dic, diTypeList, bDi );
        }
    }
}
