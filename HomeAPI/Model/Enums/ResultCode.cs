using System.ComponentModel;

namespace HomeAPI.Model.Enums
{
    /// <summary>
    /// 返回码
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success = 0,
        /// <summary>
        /// 操作错误
        /// </summary>
        [Description("操作引发错误")]
        Error = 101,
    }
}
