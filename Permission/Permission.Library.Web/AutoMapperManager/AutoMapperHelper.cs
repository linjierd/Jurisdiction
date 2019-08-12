using AutoMapper;
using AutoMapper.Configuration;
using Permission.Library.Web.AutoMapperManager.ModelMapConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Library.Web.AutoMapperManage
{
    public  class AutoMapperHelper
    {
        private static readonly MapperConfigurationExpression MapperConfiguration = new MapperConfigurationExpression();

        static AutoMapperHelper()
        {
        }

        private AutoMapperHelper()
        {
            MapperConfiguration.AddProfile<AdminUserProfile>();
            Mapper.Initialize(MapperConfiguration);
        }

        public static AutoMapperHelper Instance { get; } = new AutoMapperHelper();

        /// <summary>
        /// 添加映射关系
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        public void AddMap<TSource, TDestination>() where TSource : class, new() where TDestination : class, new()
        {
            MapperConfiguration.CreateMap<TSource, TDestination>();
        }

        /// <summary>
        /// 获取映射值
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TDestination>(object source) where TDestination : class, new()
        {
            if (source == null)
            {
                return default(TDestination);
            }
         
            return Mapper.Map<TDestination>(source);
        }
        /// <summary>
        /// 获取映射值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source) where TSource : class, new() where TDestination : class, new()
        {
            if (source == null)
            {
                return default(TDestination);
            }

            return Mapper.Map<TSource, TDestination>(source);
        }
        /// <summary>
        /// 拷贝值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public TDestination MapCopyTo<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : class, new()
             where TDestination : class, new()
        {
            if (source == null)
            {
                return destination;
            }
            return Mapper.Map(source,destination);
        }

        /// <summary>
        /// 获取集合映射值
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<TDestination> Map<TDestination>(IEnumerable source) where TDestination : class, new()
        {
            if (source == null)
            {
                return default(IEnumerable<TDestination>);
            }

            return Mapper.Map<IEnumerable<TDestination>>(source);
        }

       

        /// <summary>
        /// 获取集合映射值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<TDestination> Map<TSource, TDestination>(IEnumerable<TSource> source) where TSource : class, new() where TDestination : class, new()
        {
            if (source == null)
            {
                return default(IEnumerable<TDestination>);
            }

            return Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
        }

       
    }
}
