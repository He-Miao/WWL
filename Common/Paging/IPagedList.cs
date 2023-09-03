namespace Common.Paging
{
    /// <summary>
    ///     分页数据接口
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public interface IPagedList<out T>
    {
        /// <summary>
        ///     要从中返回元素的 System.Linq.IQueryable<T />
        /// </summary>
        IQueryable<T> Data { get; } 

        /// <summary>
        ///     每页数据量
        /// </summary>
        int PageSize { get; }

        /// <summary>
        ///     当前页
        /// </summary>
        int CurrentPage { get; }

        /// <summary>
        ///     总数据量
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        ///     总页数
        /// </summary>
        int Totalpage { get; }
    }
}
