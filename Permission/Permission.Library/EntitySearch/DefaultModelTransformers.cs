using Permission.Library.Tools.DataTimeTools;
using System;
using System.Collections.Generic;

namespace Permission.Library.EntitySearch
{
    ///<summary>
    /// 对绑定数据进行设置的转换器
    ///</summary>
    [Obsolete]
    public class SearchModelTransformers : DefaultModelTransformers { }
    ///<summary>
    /// 对绑定数据进行设置的转换器
    ///</summary>
    public class DefaultModelTransformers
    {
        //用于进行数据转换
        static Dictionary<string, Func<string, string>> Dict { get; set; }

        static DefaultModelTransformers()
        {
            Dict = Dict ?? new Dictionary<string, Func<string, string>>
                               {
                                   {
                                       "ToUnixTime", c =>
                                                         {
                                                             DateTime dt;
                                                             if (DateTime.TryParse(c, out dt))
                                                             {
                                                                 return UnixTime.FromDateTime(dt).ToString();
                                                             }
                                                             return "0";
                                                         }
                                       }

                               };
        }

        /// <summary>
        /// 获取指定的转换器
        /// </summary>
        /// <param name="key">转换器的Key</param>
        /// <returns></returns>
        public static Func<string, string> Get(string key)
        {
            return Dict[key];
        }

        /// <summary>
        /// 注册转换器
        /// </summary>
        /// <param name="key">转换器的Key</param>
        /// <param name="transformer">转换器的方法，默认可以使用委托、方法或Lambda表达式</param>
        public static void Register(string key, Func<string, string> transformer)
        {
            if (!Dict.ContainsKey(key))
                Dict.Add(key, transformer);
        }
    }
}
