using HomeAPI.Tools;

namespace HomeAPI.Repositories
{
    /// <summary>
    /// 当前用户
    /// </summary>
    public class CurrentUser
    {
        public static string UserId
        {
            get
            {
                var id = BuilderExtensions.serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User?.FindFirst(ClaimAttributes.UserId);
                if (id != null)
                {
                    return id.Value;
                }
                return string.Empty;
            }
        }

        public static string UserName
        {
            get
            {
                var name = BuilderExtensions.serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User?.FindFirst(ClaimAttributes.UserName);
                if (name != null)
                {
                    return name.Value;
                }
                return string.Empty;
            }
        }

        public static class ClaimAttributes
        {
            /// <summary>
            /// 用户Id
            /// </summary>
            public const string UserId = "currentUserId";
            /// <summary>
            /// 用户名
            /// </summary>
            public const string UserName = "currentUserName";
        }
    }
}
