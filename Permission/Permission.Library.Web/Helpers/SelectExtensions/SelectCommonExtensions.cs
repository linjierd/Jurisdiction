using Permission.Library.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Permission.Library.Web.Helpers.SelectExtensions
{
    public static class SelectCommonExtensions
    {
        public static MvcHtmlString CommonSelector(this HtmlHelper htmlHelper, string name, object value = null,
                                                  IDictionary<string, object> dic = null,
                                                  IDictionary<string, string> binding = null,
                                                  IDictionary<string, string> defaultBinding = null)
        {
            if (defaultBinding == null)
            {
                defaultBinding = new Dictionary<string, string>();
            }
            if (binding != null && binding.Count > 0)
            {
                defaultBinding.AddRange(binding);
            }
            return htmlHelper.DropDownList(name, new SelectList(defaultBinding, "key", "value", value), dic);
        }
        public static MvcHtmlString CommonSelectorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                         Expression<Func<TModel, TProperty>> expression,
                                                                         IDictionary<string, object> dic = null,
                                                                         IDictionary<string, string> binding = null,
                                                                         IDictionary<string, string> defaultBinding =null)
        {
            if (defaultBinding == null)
            {
                defaultBinding = new Dictionary<string, string>();
            }
            if (binding != null && binding.Count > 0)
            {
                defaultBinding.AddRange(binding);
            }
            return htmlHelper.DropDownListFor(expression, new SelectList(defaultBinding, "key", "value"), dic);
        }
    }
}
