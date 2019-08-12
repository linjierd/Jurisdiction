using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Dal.Common
{
    /// <summary>
    /// Dal基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> where T : class, new()
    {
        public virtual DbContext DbContext { get; set; }
        public BaseDal()
        {
          
        }
        public  DbSet<TEntity> Set<TEntity>() where TEntity : class, new()
        {
            return DbContext.Set<TEntity>();
        }
        /// <summary>
        /// 执行保存
        /// </summary>
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        #region 添加数据
        //添加数据
        public int Add(T model)
        {

            DbContext.Set<T>().Add(model);
            return DbContext.SaveChanges();
        }

        public int AddList(List<T> list)
        {
            foreach (var item in list)
            {
                DbContext.Set<T>().Add(item);
            }
            return DbContext.SaveChanges();
        }
        public int AddList<U>(List<U> list) where U:class
        {
            foreach (var item in list)
            {
                DbContext.Set<U>().Add(item);
            }
            return DbContext.SaveChanges();
        }
        public T AddModel(T model)
        {
            DbContext.Set<T>().Add(model);
            DbContext.SaveChanges();
            return model;
        }
        #endregion

        #region 修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditData(T model)
        {

            DbContext.Entry(model).State = EntityState.Modified;
            return DbContext.SaveChanges();
        }
        ///// <summary>
        ///// 修改数据
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="fileds">需要修改的列</param>
        ///// <returns></returns>
        //public int UpdateEntityFields(T entity, List<string> fileds)
        //{
        //    // DbContext.Set<T>().AsNoTracking().FirstOrDefault();
        //  // var model= DbContext.Set<T>().Find();

        //    DbEntityEntry model = DbContext.Entry<T>(entity);
        //    model.State = EntityState.Unchanged;
        //    foreach (var t in fileds)
        //    {
        //        model.Property(t).IsModified = true;
        //    }
        //    //DbContext.Set<T>().Attach(entity);
        //    //var SetEntry = ((IObjectContextAdapter)DbContext).ObjectContext.
        //    //    ObjectStateManager.GetObjectStateEntry(entity);
        //    //foreach (var t in fileds)
        //    //{
        //    //    SetEntry.SetModifiedProperty(t);
        //    //}
        //    return DbContext.SaveChanges();
        //}

        #endregion

        #region 删除数据
        //根据实体删除数据
        public int DeleteData(T model)
        {
            //db.Entry(model).Property("IsDelete").CurrentValue=true;
            //db.Entry(model).Property("IsDelete").IsModified = true;
            DbContext.Set<T>().Attach(model);
            DbContext.Set<T>().Remove(model);
            return DbContext.SaveChanges();
        }

        //根据id删除数据
        public int DeleteData(int id)
        {
            T model = DbContext.Set<T>().Find(id);
            this.DeleteData(model);
            return DbContext.SaveChanges(); 
        }

        //根据多个id删除数据
        public int DeleteData(int[] ids)
        {
            for (int i = 0; i < ids.Count(); i++)
            {
                DeleteData(ids[i]);
            }
            return DbContext.SaveChanges(); 
        }
        #endregion

        #region 获取实体
        //根据id获取实体
        public T GetById(int id)
        {
            return DbContext.Set<T>().Find(id);
        }
        public T GetModel(Expression<Func<T, bool>> whereLamda)
        {
            var q= DbContext.Set<T>().FirstOrDefault(whereLamda);
            return q;
        }
        #endregion

        #region 查询数据
        //分页查询
        public IQueryable<T> GetListByPage<TKey>(Expression<Func<T, bool>> whereLamda, Expression<Func<T, TKey>> orderLambda, int pageSize, int pageIndex, out int total)
        {
            if (orderLambda == null) throw new Exception("请写入排序的拉姆达表达式!");
            if (whereLamda != null)
            {
                total = DbContext.Set<T>().Where(whereLamda).Count();
              return  DbContext.Set<T>().Where(whereLamda)
                 .OrderByDescending(orderLambda)
                 .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize);
            }
            else
            {
                total = DbContext.Set<T>().Where(c => true).Count();
                return DbContext.Set<T>().Where(c=>true)
                 .OrderByDescending(orderLambda)
                 .Skip((pageIndex - 1) * pageSize)
                  .Take(pageSize);
            }
        } 

            

        //查询所有数据
        public IQueryable<T> Where(Expression<Func<T, bool>> whereLambda)
        {
            return DbContext.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> GetAllList()
        {
            return DbContext.Set<T>();
        }
        #endregion
    }
}
