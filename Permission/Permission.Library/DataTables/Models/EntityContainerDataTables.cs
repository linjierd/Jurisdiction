using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Permission.Library.DataTables.Models;

namespace Permission.Library
{
     [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EntityContainerDataTables
    {
        /// <summary>
        /// 返回DataTables 可用的json
        /// </summary>
        /// <typeparam name="T">当前对象</typeparam>
        /// <param name="entityContainer"></param>
        /// <param name="m"></param>
        /// <returns></returns>
         public static DataTablesJson GetDataTableJson<T>(this EntityContainer<T> entityContainer,SearchModel m) where T : class
        {
            DataTablesJson json = new DataTablesJson();
            if (entityContainer != null && entityContainer.Rows != null && entityContainer.Rows.Count > 0)
            {

                json.draw = m.draw;
                json.recordsTotal = entityContainer.Total;
                json.recordsFiltered = entityContainer.Total;
                json.data=new List<Dictionary<string, object>>();
                foreach (var row in entityContainer.Rows)
                {
                    Dictionary<string,object> di=new Dictionary<string, object>();
                    for (int i = 0; i < entityContainer.Keys.Count; i++)
                    {
                        di.Add(entityContainer.Keys[i],row.Cell[i]);
                    }
                    json.data.Add(di);
                }
            }
            if (json.data ==null)json.data=new List<Dictionary<string, object>>();
            return json;
        }
    }
}
