
using Permission.Library.Attributes;
namespace Permission.Model.Common.GlobalCode
{


    public enum IntTrueOrFalse
    {
        [GlobalCode("是")]
        True = 1,
        [GlobalCode("否")]
        Flase = 2,
    }

    public enum CommonStatus
    {
        /// <summary>
        /// 活跃的,有效的
        /// </summary>
        [GlobalCode("有效")] Active = 1,
        /// <summary>
        /// 不活跃的,无效的
        /// </summary>
        [GlobalCode("无效")] Inactive = 2,

    }

 

}
