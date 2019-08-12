using Permission.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Bll.Common
{
    /// <summary>
    /// Bll的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePermissionBll<T,U> where T : class, new()
                                        where U : class, new()
    {

        public static U Instance { get;} = new U();
        private PermissionDal<T> _PermissionDal;
        /// <summary>
        /// 构造
        /// </summary>
        public BasePermissionBll()
        {
            //创建权限dal
            _PermissionDal = new PermissionDal<T>();
        }
        /// <summary>
        /// 权限dal只读属性
        /// </summary>
        public PermissionDal<T> PermissionDal
        {
            get
            {
                return new PermissionDal<T>();
            }
        }

        
    }
}
