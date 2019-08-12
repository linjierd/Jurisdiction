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
using Permission.Model.Common.GlobalCode;

namespace Permission.Bll.SystemManager
{
    public class DictionaryTableBll : BasePermissionBll<DictionaryTableDb, DictionaryTableBll>
    {
        /// <summary>
        /// 获取制定key的字典
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DictionaryTableDb GetModel(string key)
        {
            return PermissionDal.GetModel(c => c.dt_key == key);
        }
        /// <summary>
        /// 获取指定类别key下面的有效字典
        /// </summary>
        /// <param name="dtTypeKey"></param>
        /// <returns></returns>
        public List<DictionaryTableDb> GetListInDtType(string dtTypeKey)
        {
            return PermissionDal.Where(c => c.dt_type_key == dtTypeKey && c.dt_status == (int)CommonStatus.Active).OrderBy(c=>c.dt_orderby).ToList();
        }
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="m">搜索模型</param>
        /// <returns></returns>
        public PagedList<DictionaryTableDb> GetPageList(SearchModel m)
        {
            return PermissionDal.Set<DictionaryTableDb>().Where(m).Pager(m);
        }
        public void Update(DictionaryTableDb model)
        {
            DictionaryTableDb tempModel = PermissionDal.GetModel(c => c.dt_key == model.dt_key);
            ModelCopier.CopyModel(model, tempModel, "creator_name", "creator_date");
            PermissionDal.SaveChanges();
        }

        public void Add(DictionaryTableDb model)
        {
            PermissionDal.Add(model);
        }
    }
}
