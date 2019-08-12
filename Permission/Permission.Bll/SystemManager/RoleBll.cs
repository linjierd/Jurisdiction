using Permission.Bll.Common;
using Permission.Library;
using Permission.Library.Common;
using Permission.Library.DataTables.Models;
using Permission.Library.EntitySearch;
using Permission.Library.Extensions;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Bll.SystemManager
{
    public class RoleBll: BasePermissionBll<RoleDb, RoleBll>
    {
        public RoleDb GetModel(int id)
        {
           return PermissionDal.GetById(id);
        }
        public List<RoleDb> GetRoleListAll()
        {
            return PermissionDal.Where(c => true).ToList();
        }

        public List<RoleDb> GetRoleListAllInCache()
        {
            return SystemCacheManager.GetCache("RoleBll_GetRoleListAll", 60, GetRoleListAll);
        }
        /// <summary>
        /// 获取用户对应的角色列表
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public IQueryable<RoleDb> GetRoleDbList(string userName)
        {
            IQueryable<int> roleIds =
                    PermissionDal.Set<AdminUserRoleRelationDb>().Where(c => c.user_name == userName).Select(c => c.role_id);
            return PermissionDal.Where(c => roleIds.Contains(c.role_id));
        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="m">搜索模型</param>
        /// <returns></returns>
        public PagedList<RoleDb> GetRoleDbPageList(SearchModel m)
        {
            return PermissionDal.Set<RoleDb>().Where(m).Pager(m);
        }


        public void Update(RoleDb model, List<RoleModuleRelationDb> roleModuleRelations)
        {
            using (var db = PermissionDal.DbContext)
            {
                using (var dbContextTransaction = PermissionDal.DbContext.Database.BeginTransaction()) //起一个事务
                {
                    try
                    {
                        RoleDb tempModel = PermissionDal.GetModel(c => c.role_id == model.role_id);

                        ModelCopier.CopyModel(model, tempModel, "creator_name",
                                             "creator_full_name", "creator_time");
                        PermissionDal.EditData(tempModel);
                        PermissionDal.DbContext.Database.ExecuteSqlCommand(" delete from role_module_relation where role_id=@p0 ", model.role_id);
                        PermissionDal.AddList(roleModuleRelations);
                        PermissionDal.DbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                    finally
                    {
                        PermissionDal.DbContextDispose();
                    }
                }
            }
           
          

        }

        public int Add(RoleDb model,List<RoleModuleRelationDb> roleModuleRelations)
        {

            using (var dbContextTransaction = PermissionDal.DbContext.Database.BeginTransaction()) //起一个事务
            {
                try
                {
                    PermissionDal.Add(model);
                    PermissionDal.DbContext.SaveChanges();
                    if (roleModuleRelations != null)
                    {
                        foreach (var r in roleModuleRelations)
                        {
                            r.creator_name = model.creator_name;
                            r.creator_date = model.creator_date;
                            r.role_id = model.role_id;
                            PermissionDal.Set<RoleModuleRelationDb>().Add(r);
                        }
                    }
                    PermissionDal.DbContext.SaveChanges();
                    dbContextTransaction.Commit();
                    return model.role_id;
                  
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
