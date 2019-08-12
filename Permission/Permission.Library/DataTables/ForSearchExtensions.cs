namespace Permission.Library
{
    using Common;
    using System;
    using System.Diagnostics.Contracts;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// 
    /// </summary>
    public static class ForSearchExtensions
    {
        #region ForSearch

        /// <summary>
        /// 为当前表中所有单元素添加搜索条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="method">搜索方法</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        public static MvcHtmlWrapper ForSearchAll(this IHtmlString str, SearchMethod? method,
            string prefix = "")
        {
            var wrapper = MvcHtmlWrapper.Create(str);
            Contract.Assert(null != wrapper);
            if (!method.HasValue) return wrapper;
            var html = wrapper.HtmlString;

            #region 如果是CheckBox，则去掉hidden

            if (html.Contains("type=\"checkbox\""))
            {
                var checkMatch = Regex.Match(html, "<input name=\"[^\"]+\" type=\"hidden\" [^>]+ />");
                if (checkMatch.Success)
                {
                    wrapper.Add(checkMatch.Groups[0].Value, String.Empty);
                }
            }
            #endregion

            #region 替换掉Name
            var namePrefix = new StringBuilder();
            namePrefix.AppendFormat("[{0}]", method);//添加筛选谓词
            if (!String.IsNullOrWhiteSpace(prefix))
            {
                namePrefix.AppendFormat("({0})", prefix);//添加前缀
            }
            var matches = Regex.Matches(html, "name=\"(?<name>[^\"]+)\"");
            foreach (Match match in matches)
            {
                wrapper.Add(match.Groups[0].Value, String.Format("name=\"{1}{0}\"", match.Groups[1].Value, namePrefix));
            }
            #endregion
            return wrapper;
        }

        #endregion

        /// <summary>
        /// 为当前表单元素添加搜索条件
        /// </summary>
        /// <param name="str"></param>
        /// <param name="method">搜索方法</param>
        /// <param name="prefix">前缀</param>
        /// <param name="hasId">是否显示Id，默认false</param>
        /// <param name="orGroup">如果想要支援Or，请设置一个Or分组</param>
        /// <param name="transformer"></param>
        /// <returns></returns>
        public static MvcHtmlWrapper ForSearch(this IHtmlString str, SearchMethod? method,
                                               string prefix = "",
                                               bool hasId = false,
                                               string orGroup = "", string transformer = null)
        {
            var wrapper = MvcHtmlWrapper.Create(str);
            Contract.Assert(null != wrapper);
            if (!method.HasValue) return wrapper;
            var html = wrapper.HtmlString;

            #region 如果是CheckBox，则去掉hidden

            if (html.Contains("type=\"checkbox\""))
            {
                var checkMatch = Regex.Match(html, "<input name=\"[^\"]+\" type=\"hidden\" [^>]+ />");
                if (checkMatch.Success)
                {
                    wrapper.Add(checkMatch.Groups[0].Value, String.Empty);
                }
            }

            #endregion

            #region 替换掉Name

            var match = Regex.Match(html, "name=\"(?<name>[^\"]+)\"");
            var strInsert = "";
            if (!String.IsNullOrWhiteSpace(prefix))
            {
                strInsert += String.Format("({0})", prefix);
            }
            if (!String.IsNullOrWhiteSpace(orGroup))
            {
                strInsert += String.Format("{{{0}}}", orGroup);
            }
            if (match.Success)
            {
                wrapper.Add(match.Groups[0].Value,
                            String.Format("name=\"[{1}]{2}{0}\"", match.Groups[1].Value, method, strInsert));
                #region 使用转换器
                if(!String.IsNullOrWhiteSpace(transformer))
                {
                    wrapper.Add("/>",
                                String.Format("/><input type=\"hidden\" name=\"Transform$[{1}]{2}{0}\" value=\"{3}\" />",
                                              match.Groups[1].Value, method, strInsert, transformer));
                }

                #endregion
            }

            #endregion

            #region 如果不做设置，则默认不输出Id

            //默认搜索条件无Id
            //if (!hasId)
            //{
            //    var matchId = Regex.Match(html, " id=\"[^\"]+\"");
            //    if (matchId.Success)
            //    {
            //        wrapper.ReplaceDict.Add(Tuple.Create(matchId.Groups[0].Value, ""));
            //    }
            //}

            #endregion


            return wrapper;
        }
    }
}