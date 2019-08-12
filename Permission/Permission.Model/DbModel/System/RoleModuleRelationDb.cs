using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Permission.Model.DbModel.System
{
    /// <summary>
    /// 实体类 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("role_module_relation")]
    public class RoleModuleRelationDb
    {
        public RoleModuleRelationDb()
        { }
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int id
        {
            get;
            set;
        }
        /// <summary>
        /// 角色id
        /// </summary>
        public int role_id
        {
            get;
            set;
        }
        /// <summary>
        /// 功能id
        /// </summary>
        public string module_code
        {
            get;
            set;
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string creator_name
        {
            get;
            set;
        }
      
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime creator_date
        {
            get;
            set;
        }
        #endregion Model

        public virtual RoleDb RoleDb { get; set; }

        public virtual ModuleDb ModuleDb { get; set; }
    }
}
