using SqlSugar;
using System.Linq.Expressions;

namespace HomeAPI.IRepository
{
    /// <summary>
    /// 仓储通用操作接口
    /// 数据库增、删、改、查操作
    /// </summary>
    /// <typeparam name="TEntity">数据库表映射实体</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        #region 新增
        /// <summary>
        /// 插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        int AddScalar(TEntity entity);
        /// <summary>
        /// 异步插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        Task<int> AddScalarAsync(TEntity entity);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回是否插入成功</returns>
        bool Insert(TEntity entity);
        /// <summary>
        /// 异步插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响行数</returns>
        Task<int> InsertAsync(TEntity entity);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体对象集合</param>
        /// <returns>返回是否插入成功</returns>
        bool Insert(List<TEntity> entity);
        /// <summary>
        /// 异步批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响行数</returns>
        Task<int> InsertAsync(List<TEntity> entity);
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>是否删除成功</returns>
        bool Delete(dynamic keyValue);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(dynamic keyValue);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否删除成功</returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>是否删除成功</returns>
        bool Delete(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>是否删除成功</returns>
        bool Delete(List<dynamic> keys);
        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>是否删除成功</returns>
        Task<bool> DeleteAsync(List<dynamic> keys);
        #endregion

        #region 修改
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        /// <returns>是否更新成功</returns>
        bool Update(TEntity entity, bool isNoUpdateNull = false);
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        /// <returns>是否更新成功</returns>
        Task<bool> UpdateAsync(TEntity entity, bool isNoUpdateNull = false);
        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        /// <returns></returns>
        bool Update(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        /// <returns></returns>
        bool Update(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>是否成功</returns>
        bool Update(TEntity entity, Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 通过条件修改（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        /// <returns>是否修改成功</returns>
        bool Update(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 修改指定字段（异步）
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        /// <returns>是否修改成功</returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition);
        #endregion

        #region 查询
        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns>集合</returns>
        List<TEntity> Queryable();
        /// <summary>
        /// 获取所有集合（异步）
        /// </summary>
        /// <returns>集合</returns>
        Task<List<TEntity>> QueryableAsync();
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回实体</returns>
        TEntity FindEntity(object keyValue);
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>返回实体</returns>
        TEntity FindEntity(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取实体（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>返回实体</returns>
        Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查询数据总条数
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>数据总数量</returns>
        int QueryableCount(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查询数据总条数（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>数据总数量</returns>
        Task<int> QueryableCountAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>集合</returns>
        List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>集合</returns>
        Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <returns>集合</returns>
        List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc);
        /// <summary>
        /// 根据条件获取集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <returns>集合</returns>
        Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc);
        /// <summary>
        /// 根据条件获取指定条数集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        /// <returns>集合</returns>
        List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top);
        /// <summary>
        /// 根据条件获取指定条数集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        /// <returns>集合</returns>
        Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>集合|总条数</returns>
        Tuple<List<TEntity>, int> QueryableByPage(Expression<Func<TEntity, bool>>? expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int pageIndex, int pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>集合|总条数</returns>
        Tuple<List<TEntity>, int> QueryableByPage(Expression<Func<TEntity, bool>> expression, Dictionary<Expression<Func<TEntity, object>>, OrderByType> orderby, int pageIndex, int pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionals">查询条件</param>
        /// <param name="orderFileds">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        Tuple<List<TEntity>, int> QueryableByPage(List<IConditionalModel> conditionals, string orderFileds, int pageIndex, int pageSize);
        #endregion
    }
}
