namespace Common.Extensions
{
    /// <summary>
    /// 对象扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 将对象转为ToList
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static List<T> ToList<T>(this object obj)
        {
            //检查对象是否为空
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }
            //检查对象是否可以转换为 IEnumerable<T> 接口
            if (obj is not IEnumerable<T> enumerable)
            {
                throw new ArgumentException($"对象必须可转换为IEnumerable<{typeof(T).Name}>");
            }
            //创建一个新的 List<T> 并返回
            return new List<T>(enumerable);
        }
    }
}
