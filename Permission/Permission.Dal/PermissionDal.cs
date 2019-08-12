using Permission.Dal.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Dal
{
    public class PermissionDal<T>:BaseDal<T> where T : class, new()
    {
        private static DbContext _DbContext;
        private static readonly object locker = new object();
        public override DbContext DbContext
        {
            get
            {
                return DbContextFactory.PermissionContainer;
            }
            set {
                _DbContext = value;
                DbContextFactory.PermissionContainer = value;
            }
        }
        /// <summary>
        /// 释放上下文
        /// </summary>
        public void DbContextDispose()
        {
            DbContext = null;
        }
    }
}
