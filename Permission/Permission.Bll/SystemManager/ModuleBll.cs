using Permission.Bll.Common;
using Permission.Library.Common;
using Permission.Model.Common.ZTree;
using Permission.Model.DbModel.System;
using Permission.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permission.Bll.SystemManager
{
    public class ModuleBll : BasePermissionBll<ModuleDb, ModuleBll>
    {
        /// <summary>
        /// 获取所有的模块
        /// </summary>
        /// <returns></returns>
        public IQueryable<ModuleDb> GetMouduleListAll()
        {
            return PermissionDal.Set<ModuleDb>().OrderBy(c => c.order_by);
        }
        public void Update(ModuleDb model)
        {
            ModuleDb tempModel = PermissionDal.GetModel(c => c.module_code == model.module_code);
            ModelCopier.CopyModel(model, tempModel, "parent_code", "module_level", "creator_name", "creator_date");
            PermissionDal.SaveChanges();
        }

        public void Add(ModuleDb model)
        {
            PermissionDal.Add(model);
        }
        /// <summary>
        /// 获取制定code的模块
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ModuleDb GetModule(string code)
        {
            return PermissionDal.GetModel(c => c.module_code == code);
        }
        /// <summary>
        /// 获取对应url的模块
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ModuleDb GetModuleOnUrl(string url)
        {
            return PermissionDal.GetModel(c => c.action_url.ToUpper() == url.ToUpper());
        }

        /// <summary>
        /// 获取用户对应的有权限的活动的模块
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public IQueryable<ModuleDb> GetModuleList(string userName)
        {
            IQueryable<int> roleIds = RoleBll.Instance.GetRoleDbList(userName).Select(c=>c.role_id);
            IQueryable<string> moduleCodes = PermissionDal.Set<RoleModuleRelationDb>()
                .Where(c=> roleIds.Contains( c.role_id)).Select(c=>c.module_code);
            return PermissionDal.Where(c => moduleCodes.Contains(c.module_code) && c.module_status == (int)Model.Common.GlobalCode.CommonStatus.Active)
                .OrderBy(c=>c.order_by);

        }
        #region 具有层级结构的Module操作方法
        /// <summary>
        /// 将一维的模块列表初始化为层级模块列表
        /// </summary>
        /// <param name="moduleList"></param>
        public List<LevelModuleViewModel> IniLevelModuleList(List<ModuleDb> moduleList)
        {
            List<LevelModuleViewModel> moduleViewList = new List<LevelModuleViewModel>();
            if (moduleList != null && moduleList.Count > 0)
            {
                moduleViewList = IniModuleViewList(moduleList);
                return GetLevelModuleList(moduleViewList);
            }
            return moduleViewList;
        }

        /// <summary>
        /// 将一维的模块列表初始化为层级模块列表
        /// </summary>
        /// <param name="moduleViewListAll">所有的模块列表</param>
        public List<LevelModuleViewModel> GetLevelModuleList(List<LevelModuleViewModel> moduleViewListAll)
        {
            if (moduleViewListAll != null && moduleViewListAll.Count > 0)
            {
                int maxLevel = moduleViewListAll.Min(c => c.module_level);
                List<LevelModuleViewModel> paterList = moduleViewListAll.Where(c => c.module_level == maxLevel).ToList();
                foreach (var moduleViewModel in paterList)
                {
                    IniLevelModuleList(moduleViewModel, moduleViewListAll);
                }
                return paterList;

            }
            return moduleViewListAll;
        }


        /// <summary>
        /// 递归初始化模块子集
        /// </summary>
        /// <param name="model">父模块</param>
        /// <param name="moduleViewList">模块列表</param>
        public void IniLevelModuleList(LevelModuleViewModel model, List<LevelModuleViewModel> moduleViewList)
        {
            if (model != null && moduleViewList.Count > 0)
            {
                List<LevelModuleViewModel> sonList = moduleViewList.Where(c => c.parent_code == model.module_code).ToList();
                if (sonList.Count > 0)
                {
                    model.SonList = sonList;
                    foreach (var moduleViewModel in sonList)
                    {
                        IniLevelModuleList(moduleViewModel, moduleViewList);
                    }
                }
            }
        }
        /// <summary>
        /// 从指定的模块列表中获取指定模块的子列表（递归）
        /// </summary>
        /// <param name="parentCode">父模块code</param>
        /// <param name="sourceList">原模块列表</param>
        /// <returns>子列表</returns>
        public List<LevelModuleViewModel> GetSonLevelModuleList(string parentCode, List<LevelModuleViewModel> sourceList)
        {
            if (sourceList != null && sourceList.Count > 0)
            {
                foreach (LevelModuleViewModel model in sourceList)
                {
                    if (model.module_code == parentCode) return model.SonList;
                    else
                    {
                        List<LevelModuleViewModel> resultList = GetSonLevelModuleList(parentCode, model.SonList);
                        if (resultList != null && resultList.Count > 0) return resultList;
                    }
                }

            }
            return new List<LevelModuleViewModel>();
        }

        #endregion



        #region ModuleDb内容复制到LevelModuleViewModel的方法
        /// <summary>
        /// 将ModuleDb列表的内容复制到LevelModuleViewModel列表中
        /// </summary>
        /// <param name="list">ModuleDb列表</param>
        /// <returns></returns>
        public List<LevelModuleViewModel> IniModuleViewList(List<ModuleDb> list)
        {
            var viewModels = new List<LevelModuleViewModel>();
            foreach (var module in list)
            {

                LevelModuleViewModel viewModule = IniModuleView(module);
                if (list.FirstOrDefault(c => c.parent_code == module.module_code) != null)
                {
                    viewModule.isParent = true;
                }
                else
                {
                    viewModule.isParent = false;
                }
                viewModels.Add(viewModule);
            }
            return viewModels;
        }

        /// <summary>
        /// 将ModuleDb的内容复制到LevelModuleViewModel中
        /// </summary>
        /// <param name="module">ModuleDb</param>
        /// <returns></returns>
        public LevelModuleViewModel IniModuleView(ModuleDb module)
        {
            var view = new LevelModuleViewModel();
            ModelCopier.CopyModel(module, view, "SonList");
            return view;
        }
        #endregion

        #region 一维的,没有层级的Module操作方法
        /// <summary>
        /// 获取全部一维的模块View列表
        /// </summary>
        /// <returns></returns>
        public List<LevelModuleViewModel> GetLevelMudleListIsArrayAll()
        {
            return IniModuleViewList(GetMouduleListAll().ToList());
        }
        /// <summary>
        /// 获取全部一维的模块View列表,在缓存中
        /// </summary>
        /// <returns></returns>
        public List<LevelModuleViewModel> GetLevelModuleListIsArrayAllInCache()
        {
            return SystemCacheManager.GetCache("ModuleBll_GetLevelModuleListIsArrayAllInCache", 60, GetLevelMudleListIsArrayAll);
        }
        /// <summary>
        /// 获取没有层级的指定条件的Moudule列表
        /// </summary>
        /// <param name = "parentCode" >父code</ param >
        /// < param name="moduleLevelCount">获取几级</param>
        /// <param name = "isCache" >是否缓存</ param >
        /// < returns ></ returns >
        public List<LevelModuleViewModel> GetSonModuleViewListIsArrayInCache(string parentCode, int moduleLevelCount=1, bool isCache = true)
        {
            List<LevelModuleViewModel> resultList = new List<LevelModuleViewModel>();
            List<LevelModuleViewModel> arrayListAll = new List<LevelModuleViewModel>();
            LevelModuleViewModel paterViewModel = null;
            if (isCache)
            {
                arrayListAll = GetLevelModuleListIsArrayAllInCache().ToList();

            }
            else
            {
                arrayListAll = GetLevelMudleListIsArrayAll().ToList();
            }
            if (arrayListAll == null) return new List<LevelModuleViewModel>();
            if (arrayListAll.Count == 0 || moduleLevelCount == -1) return resultList;
            if (string.IsNullOrEmpty(parentCode))
            {
                paterViewModel = arrayListAll.FirstOrDefault(c => c.module_level == 1);
                if (paterViewModel == null) return resultList;
                else
                {
                    resultList.Add(paterViewModel);
                }
            }
            else
            {
                paterViewModel = arrayListAll.FirstOrDefault(c => c.parent_code == parentCode);
                if (paterViewModel == null) return resultList;
                resultList = arrayListAll.Where(c => c.parent_code == parentCode).ToList();

            }

            if (moduleLevelCount == 1) return resultList;
            else
            {
                moduleLevelCount = resultList[0].module_level + moduleLevelCount;
                List<LevelModuleViewModel> sonList = arrayListAll.Where(c => c.module_level > resultList[0].module_level && c.module_level < moduleLevelCount).ToList();
                resultList.AddRange(sonList);

            }
            return resultList;
        }
       
        #endregion


        /// <summary>
        ///获取所有的模块,以Ztree可以识别的Ztree格式返回,并且根据角色权限设置对应模块为选中状态
        /// <param name="id">角色id</param>
        /// <returns></returns>
        public string GetModuleMacheRoleTree(int id)
        {

            List<LevelModuleViewModel> moduleList = GetLevelMudleListIsArrayAll().Where(c => c.module_status == 1).ToList();
            List<JsonZtree> jsonZtreeList = ZtreeManage.IniJsonZtreeList(moduleList, moduleList);
            if (id > 0)
            {
                List<RoleModuleRelationDb> list = PermissionDal.Set<RoleModuleRelationDb>()
                .Where(c => c.role_id== id).ToList();
                foreach (var m in jsonZtreeList)
                {
                    if (list.FirstOrDefault(c => c.module_code == m.id) != null)
                    {
                        m.Checked = "true";
                        m.open = "true";
                    }
                }
            }
            return ZtreeManage.GetZtreeJson(jsonZtreeList);
        }
        /// <summary>
        /// 获取指定模块下的指定层数的子模块的json,如果parentCode为空,那只获取级别为-1的
        /// </summary>
        /// <param name="parentCode">父模块code,为空时,表示从最顶级开始获取</param>
        /// <param name="moduleLevelCount">需要获取的子模块的层级数量  -1为获取全部</param>
        /// <param name="isCache">是否从缓存获取 </param>
        /// <returns></returns>
        public  string GetModuleTreeJson(string parentCode, int moduleLevelCount, bool isCache = false)
        {
            List<LevelModuleViewModel> viewList = GetSonModuleViewListIsArrayInCache(parentCode, moduleLevelCount, isCache);
            List<LevelModuleViewModel> viewListAll;
            if (isCache) viewListAll = GetLevelModuleListIsArrayAllInCache();
            else viewListAll = GetLevelMudleListIsArrayAll();
            List<JsonZtree> jsonZtreeList = ZtreeManage.IniJsonZtreeList(viewList, viewListAll);
            return ZtreeManage.GetZtreeJson(jsonZtreeList);
        }


        ///// <summary>
        ///// 获取指定模块下的指定层数的子模块的json,如果parentCode为空,那只获取级别为-1的
        ///// </summary>
        ///// <param name="parentCode">父模块code,为空时,表示从最顶级开始获取</param>
        ///// <param name="moduleLevelCount">需要获取的子模块的层级数量  -1为获取全部</param>
        ///// <param name="isCache">是否从缓存获取 </param>
        ///// <returns></returns>
        //public static string GetModuleTreeJson(string parentCode, int moduleLevelCount, bool isCache = false)
        //{
        //    List<LevelModuleViewModel> viewList = ModuleBll.Instance.GetSonModuleViewListIsArray(parentCode, moduleLevelCount,
        //                                                                                     isCache);
        //    foreach (var view in viewList)
        //    {
        //        if (view.module_level == 1)
        //        {
        //            view.open = "true";
        //        }
        //        if (view.SonList == null || view.SonList.Count == 0)
        //        {
        //            view.isParent = false;
        //        }
        //        else
        //        {
        //            view.isParent = true;
        //        }
        //    }
        //    List<IJsonZtree> list =
        //       viewList.ToList<IJsonZtree>();
        //    return SystemCeter.Service.Common.Ztree.ZtreeManage.GetZtreeJson(list);
        //}

    }
}
