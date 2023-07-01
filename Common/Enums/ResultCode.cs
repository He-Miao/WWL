using System.ComponentModel;

namespace Common.Enums
{
    public enum ResultCode
    {
        [Description("操作成功")]
        Success = 0,
        [Description("操作引发错误")]
        Error = 1,
        [Description("登录过期")]
        SignOut = 401,
        [Description("无权访问")]
        NoAccess = 403
    }
}
