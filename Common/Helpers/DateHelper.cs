namespace Common.Helpers
{
    public static class DateHelper
    {
        /// <summary>
        /// 将DateTime转为 "yyyy-MM-dd HH:mm:ss" 格式
        /// </summary>
        /// <param name="time">日期</param>
        /// <returns></returns>
        public static string DateTimeToString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
