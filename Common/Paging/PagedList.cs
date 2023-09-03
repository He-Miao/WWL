namespace Common.Paging
{
    /// <summary>
    ///     实现分页数据接口
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class PagedList<T> : IPagedList<T>
    {
        /// <summary>
        /// 创建了一个空序列（确保Data属性总是非空的，即使没有实际数据可用。）
        /// </summary>
        private IQueryable<T> _data = Enumerable.Empty<T>().AsQueryable();

        /// <summary>
        ///     构造函数 执行分页
        /// </summary>
        /// <param name="source">要执行分页的数据源</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示数据量</param>
        public PagedList(IEnumerable<T> source, int currentPage, int pageSize)
            : this(source.AsQueryable(), currentPage, pageSize) { }

        /// <summary>
        ///     构造函数 执行分页
        /// </summary>
        /// <param name="source">要执行分页的数据源</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示数据量</param>
        public PagedList(IQueryable<T> source, int currentPage, int pageSize)
        {
            if (currentPage < 1)
                throw new ArgumentOutOfRangeException("currentPage", "当前页不能小于1");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "每页显示数量不能小于1");
            TotalItemCount = source.Count();
            if (TotalItemCount < 0) return;
            PageSize = pageSize;
            CurrentPage = currentPage;
            Totalpage = (TotalItemCount / pageSize) + (TotalItemCount % pageSize == 0 ? 0 : 1); //执行总页数计算
            _data = source.Skip(pageSize * (currentPage - 1)).Take(pageSize);
        }

        /// <summary>
        ///     要从中返回元素的 System.Linq.IQueryable<T />
        /// </summary>
        public IQueryable<T> Data
        {
            get { return _data; }
        }

        /// <summary>
        ///     每页数据量
        /// </summary>
        public int PageSize
        {
            get;
        }

        /// <summary>
        ///     当前页
        /// </summary>
        public int CurrentPage
        {
            get;
        }

        /// <summary>
        ///     总数据量
        /// </summary>
        public int TotalItemCount
        {
            get;
        }

        /// <summary>
        ///     总页数
        /// </summary>
        public int Totalpage
        {
            get;
        }
    }
}
