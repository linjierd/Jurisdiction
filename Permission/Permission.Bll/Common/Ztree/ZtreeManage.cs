using Permission.Model.Common.ZTree;
using Permission.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Permission.Bll
{
   

    public class ZtreeManage
    {
        /// <summary>
        /// 获取树json
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        public static string GetZtreeJson(List<JsonZtree> jsonList)
        {
             List<Dictionary<string,string>> list=new List<Dictionary<string,string>>();
            foreach (var jsonZtree in jsonList)
            {
                Dictionary<string,string> di=new Dictionary<string,string>();
                di.Add("id",jsonZtree.id);
                di.Add("pId",string.IsNullOrEmpty(jsonZtree.pId) ? "0" : jsonZtree.pId);
                di.Add("name",jsonZtree.name);
                di.Add("open",jsonZtree.open??"false");
                di.Add("checked",jsonZtree.Checked??"false");
                di.Add("isParent",jsonZtree.isParent.ToString().ToLower());
                list.Add(di);
            }
           return Newtonsoft.Json.JsonConvert.SerializeObject(list);
        }

        public static List<JsonZtree> IniJsonZtreeList(List<LevelModuleViewModel> levelModuleViewModelList, List<LevelModuleViewModel> levelModuleViewModelListAll)
        {
            List<JsonZtree> ztreeList = new List<JsonZtree>();
            foreach (var item in levelModuleViewModelList)
            {
                JsonZtree ztree = IniJsonZtree(item);
                var sonList = levelModuleViewModelListAll.Where(c => c.parent_code == item.module_code).ToList();
                if (sonList == null || sonList.Count == 0)
                {
                    ztree.isParent = false;
                }
                else
                {
                    ztree.isParent = true;
                }
                ztreeList.Add(ztree);
                
            }
            return ztreeList;
        }
        public static JsonZtree IniJsonZtree(LevelModuleViewModel model)
        {
            JsonZtree jz = new JsonZtree();
            jz.id = model.module_code;
            jz.pId = model.parent_code;
            jz.name = model.module_name;
            if (model.module_level == 1)
            {
                jz.open = "true";
            }
            return jz;
        }
    }
}
