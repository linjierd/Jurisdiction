//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="lzh.com">
//     Copyright (c) lzh.com . All rights reserved.
// </copyright>
// <author>Zou Jian</author>
//-----------------------------------------------------------------------

namespace Permission.Library.DataTables.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Flexigrid 读取的数据实体（数组形式）
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 构造一个 Entity 对象
        /// </summary>
        /// <param name="id">生成 Table 的Id</param>
        /// <param name="data">要显示出来的数据 是一个List类型</param>
        public Entity(string id, IList<string> data)
        {
            Id = id;
            Cell = data;
        }

        /// <summary>
        /// 数据id  小写暂不变化
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 数据数组  小写暂不变化
        /// </summary>
        public IList<string> Cell { get; set; }
    }
}
