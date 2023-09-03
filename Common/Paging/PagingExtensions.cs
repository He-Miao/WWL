namespace Common.Paging
{
    /// <summary>
    ///     分页扩展
    /// </summary>
    static public class PagingExtensions
    {
        /// <summary>
        ///     执行分页
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="data">要执行分页的数据源</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <returns></returns>
        static public IPagedList<T> ToPagedList<T>(this IQueryable<T> data, int currentPage, int pageSize) => new PagedList<T>(data, currentPage, pageSize);

        /// <summary>
        ///     执行分页
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="data">要执行分页的数据源</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示数据量</param>
        /// <returns></returns>
        static public IPagedList<T> ToPagedList<T>(this IEnumerable<T> data, int currentPage, int pageSize) => new PagedList<T>(data, currentPage, pageSize);
    }
}
