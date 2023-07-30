namespace Common.Helpers
{
    /// <summary>
    /// 文件目录相关帮助类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        ///  确保目录是否存在，不存在则创建
        /// </summary>
        /// <param name="directoryPath">目录路径</param>
        public static void EnsureDirectoryExists(this string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
