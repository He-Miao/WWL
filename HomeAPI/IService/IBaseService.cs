
using HomeAPI.Model;
using SqlSugar;
using System.Linq.Expressions;

namespace HomeAPI.IService
{
    /// <summary>
    /// 数据库增、删、改、查操作业务逻辑层基类接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        #region 新增
        /// <summary>
        /// 插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        ResultData AddScalar(TEntity entity);
        /// <summary>
        /// 异步插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<ResultData> AddScalarAsync(TEntity entity);
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        ResultData Insert(TEntity entity);
        /// <summary>
        /// 异步插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<ResultData> InsertAsync(TEntity entity);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        ResultData Insert(List<TEntity> entity);
        /// <summary>
        /// 异步批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<ResultData> InsertAsync(List<TEntity> entity);
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        ResultData Delete(dynamic keyValue);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="keyValue">主键</param>
        Task<ResultData> DeleteAsync(dynamic keyValue);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        ResultData Delete(TEntity entity);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        Task<ResultData> DeleteAsync(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">条件</param>
        ResultData Delete(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        Task<ResultData> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键集合</param>
        ResultData Delete(List<dynamic> keys);
        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="keys">主键集合</param>
        Task<ResultData> DeleteAsync(List<dynamic> keys);
        #endregion

        #region 修改
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        ResultData Update(TEntity entity, bool isNoUpdateNull = false);
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        Task<ResultData> UpdateAsync(TEntity entity, bool isNoUpdateNull = false);
        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        ResultData Update(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        ResultData Update(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns);
        /// <summary>
        /// 通过条件修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        ResultData Update(TEntity entity, Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 通过条件修改（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        ResultData Update(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 修改指定字段（异步）
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        Task<ResultData> UpdateAsync(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition);
        #endregion

        #region 查询
        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns>集合</returns>
        ResultData Queryable();
        /// <summary>
        /// 获取所有集合（异步）
        /// </summary>
        /// <returns>集合</returns>
        Task<ResultData> QueryableAsync();
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        ResultData FindEntity(object keyValue);
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        ResultData FindEntity(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取实体（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        Task<ResultData> FindEntityAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查询数据总条数
        /// </summary>
        /// <param name="expression">条件（为null则查询全部数据）</param>
        ResultData QueryableCount(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 查询数据总条数（异步）
        /// </summary>
        /// <param name="expression">条件（为null则查询全部数据）</param>
        Task<ResultData> QueryableCountAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>集合</returns>
        ResultData Queryable(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        ResultData Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc);
        /// <summary>
        /// 根据条件获取集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc);
        /// <summary>
        /// 根据条件获取指定条数集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        ResultData Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top);
        /// <summary>
        /// 根据条件获取指定条数集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        ResultData QueryableByPage(Expression<Func<TEntity, bool>>? expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int pageIndex, int pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        ResultData QueryableByPage(Expression<Func<TEntity, bool>> expression, Dictionary<Expression<Func<TEntity, object>>, OrderByType> orderby, int pageIndex, int pageSize);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionals">查询条件</param>
        /// <param name="orderFileds">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        ResultData QueryableByPage(List<IConditionalModel> conditionals, string orderFileds, int pageIndex, int pageSize);
        #endregion
    }
}
