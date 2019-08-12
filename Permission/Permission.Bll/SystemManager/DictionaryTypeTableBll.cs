using Permission.Bll.Common;
using Permission.Library;
using Permission.Model.DbModel.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Permission.Library.Extensions;
using Permission.Library.Common;

namespace Permission.Bll.SystemManager
{
    public class DictionaryTypeTableBll : BasePermissionBll<DictionaryTypeTableDb, DictionaryTypeTableBll>
    {
        /// <summary>
        /// 获取指定用户名的用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DictionaryTypeTableDb GetModel(string key)
        {

            return PermissionDal.GetModel(c => c.dt_type_key == key);

        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="m">搜索模型</param>
        /// <returns></returns>
        public PagedList<DictionaryTypeTableDb> GetPageList(SearchModel m)
        {
            return PermissionDal.Set<DictionaryTypeTableDb>().Where(m).Pager(m);
        }

        public List<DictionaryTypeTableDb> GetAllList()
        {
            return PermissionDal.GetAllList().ToList(); ;
        }
        public void Update(DictionaryTypeTableDb model)
        {
            DictionaryTypeTableDb tempModel = PermissionDal.GetModel(c => c.dt_type_key == model.dt_type_key);
            ModelCopier.CopyModel(model, tempModel, "creator_name", "creator_date");
            PermissionDal.SaveChanges();
        }

        public void Add(DictionaryTypeTableDb model)
        {
            PermissionDal.Add(model);
        }
    }
}
