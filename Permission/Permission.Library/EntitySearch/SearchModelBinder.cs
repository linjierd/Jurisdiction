
namespace Permission.Library.EntitySearch
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web.Mvc;
    using Tools;

    /// <summary>
    /// 对SearchModel做为Action参数的绑定
    /// </summary>
    public class SearchModelBinder : IModelBinder
    {
        ///<summary>
        /// 生成SearchModel
        ///</summary>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            SearchModel model = GetModelPagerInfo(bindingContext);
            FillSearchItems(controllerContext, model);
            return model;
        }

        private void FillSearchItems(ControllerContext controllerContext, SearchModel model)
        {
            var nameValueCollection = controllerContext.HttpContext.Request.Params;
            var searchModelKeys = nameValueCollection.AllKeys.Where(c => c.StartsWith("["));
            if (searchModelKeys.Count() == 0) return;

            foreach (var key in searchModelKeys)
            {
                var inputValue = nameValueCollection[key];
                //处理无值的情况
                if (ValidateInputEmpty(inputValue)) continue;
                inputValue = PretreatmentString(inputValue);
                inputValue = PretreatmentDateWithTime(nameValueCollection, key, inputValue);
                inputValue = PretreatmentWhenUseTransformer(nameValueCollection, key, inputValue);
                AddSearchItem(model, key, inputValue);
            }
        }

        private string PretreatmentWhenUseTransformer(NameValueCollection nameValueCollection, string key, string inputValue)
        {
            var toTransformer = string.Format("Transform${0}", key);
            if (nameValueCollection.AllKeys.Contains(toTransformer))
            {
                var transformerName = nameValueCollection[toTransformer];
                var transformer = DefaultModelTransformers.Get(transformerName);
                if (transformer == null)
                    throw new Exception(string.Format("您所使用的SearchModelTransformer:{0}没有找到，请在Global.asax中注册", transformerName));
                inputValue = transformer(inputValue);
            }
            return inputValue;
        }

        private string PretreatmentDateWithTime(NameValueCollection nameValueCollection, string key, string inputValue)
        {
            var timefor = GetTimeFor(key);
            if (nameValueCollection.AllKeys.Contains(timefor))
            {
                inputValue = string.Format("{0} {1}", inputValue, nameValueCollection[timefor]);
            }
            return inputValue;
        }

        private string PretreatmentString(string inputValue)
        {
            inputValue = inputValue.Replace("\u200b", "");
            return inputValue;
        }

        private bool ValidateInputEmpty(string inputValue)
        {
            return string.IsNullOrEmpty(inputValue) || inputValue == "支持用*模糊查询";
        }

        private SearchModel GetModelPagerInfo(ModelBindingContext bindingContext)
        {
            var model = (SearchModel) (bindingContext.Model ?? new SearchModel());
            model.start = GetValue<int>(bindingContext, "start");
            model.length = GetValue<int>(bindingContext, "length");
            model.SortName = GetValue<string>(bindingContext, "sortname");
            model.SortOrder = GetValue<string>(bindingContext, "sortOrder");
            return model;
        }

        /// <summary>
        /// 获取Time的TimeFor的HtmlName
        /// </summary>
        static string GetTimeFor(string old)
        {
            return old + "_TimeFor";
        }

        /// <summary>
        /// 将一组key=value添加入SearchModel.Items
        /// </summary>
        /// <param name="model">SearchModel</param>
        /// <param name="key">当前项的HtmlName</param>
        /// <param name="val">当前项的值</param>
        public static void AddSearchItem(SearchModel model, string key, string val)
        {
            string field = "", prefix = "", orGroup = "", method = "";
            var keywords = key.Split(']', ')', '}');
            foreach (var keyword in keywords)
            {
                if (Char.IsLetterOrDigit(keyword[0])) field = keyword;
                var last = keyword.Substring(1);
                if (keyword[0] == '(') prefix = last;
                if (keyword[0] == '[') method = last;
                if (keyword[0] == '{') orGroup = last;
                    
            }
            if (string.IsNullOrEmpty(method) || string.IsNullOrEmpty(field)) return;
            var item = new SearchItem(field,
                                      EnumHelper<SearchMethod>.GetEnum(method),
                                      val.Trim())
                           {
                               Prefix = prefix,
                               OrGroup = orGroup,
                           };
            LikeSpecialHandle(item, val);
            model.Items.Add(item);
        }

        private static void LikeSpecialHandle(SearchItem item, string value)
        {
            if (item.Method != SearchMethod.Like) return;
            if (value.Contains("*") || value.Contains("%"))
            {
                item.Value = value.Trim().Replace("*", "%");
                return;
            }
        }


        private static T GetValue<T>(ModelBindingContext bindingContext, string key) {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(key);
            if (valueResult == null)
                return default(T);
            bindingContext.ModelState.SetModelValue(key, valueResult); 
            return (T)valueResult.ConvertTo(typeof(T));
        }



    }
}
