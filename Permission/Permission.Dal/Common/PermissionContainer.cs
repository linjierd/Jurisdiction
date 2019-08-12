using Permission.Model.DbModel;
using Permission.Model.DbModel.System;
using Permission.Model.DbModel.Test;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Dal.Common
{
    /// <summary>
    /// 权限库的连接上下文
    /// </summary>
    public class PermissionContainer : DbContext
    {
        public PermissionContainer() : base("PermissionDbContainer")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<ModuleDb>().ToTable("module_db");
        //    //modelBuilder.Entity<RoleDb>().ToTable("role_db");
        //    //modelBuilder.Entity<RoleModuleRelation>().ToTable("role_module_relation");
        //    //modelBuilder.Entity<AdminUser>().ToTable("admin_user");
        //    //modelBuilder.Entity<AdminUserRoleRelation>().ToTable("admin_user_role_relation");
        //}
        public virtual DbSet<ModuleDb> ModuleDb { get; set; }
        public virtual DbSet<RoleDb> RoleDb { get; set; }
        public virtual DbSet<RoleModuleRelationDb> RoleModuleRelation { get; set; }
        public virtual DbSet<AdminUserDb> AdminUser { get; set; }
        public virtual DbSet<AdminUserRoleRelationDb> AdminUserRoleRelation { get; set; }

        public virtual DbSet<DictionaryTableDb> DictionaryTable { get; set; }

        public virtual DbSet<DictionaryTypeTableDb> DictionaryTypeTableDb { get; set; }
        public virtual DbSet<TestDb> Test { get; set; }
        

    }
}
