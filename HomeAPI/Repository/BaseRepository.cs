using HomeAPI.IRepository;
using SqlSugar;
using System.Linq.Expressions;

namespace HomeAPI.Repository
{
    /// <summary>
    /// 数据仓储层基类
    /// 数据库增、删、改、查操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 声明SqlSugarScope用于进行事务管理和作用域控制
        /// </summary>
        public SqlSugarScope _sqlSugar;
        /// <summary>
        /// 负责SqlSugarScope的创建和销毁，确保在同一线程中始终使用相同的SqlSugarScope对象，以避免事务管理的混乱和错误
        /// </summary>
        public SqlSugarScopeProvider _sqlSugarProvider;
        public BaseRepository(SqlSugarScope sqlSugar)
        {
            _sqlSugar = sqlSugar;
            _sqlSugarProvider = sqlSugar.GetConnectionScopeWithAttr<TEntity>();
        }

        #region 新增
        /// <summary>
        /// 插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        public int AddScalar(TEntity entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteReturnIdentity();
        }

        /// <summary>
        /// 异步插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        public Task<int> AddScalarAsync(TEntity entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回是否插入成功</returns>
        public bool Insert(TEntity entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteCommand()>0;
        }

        /// <summary>
        /// 异步插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响行数</returns>
        public Task<int> InsertAsync(TEntity entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteCommandAsync();
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回是否插入成功</returns>
        public bool Insert(List<TEntity> entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteCommand()>0;
        }

        /// <summary>
        /// 异步批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响行数</returns>
        public Task<int> InsertAsync(List<TEntity> entity)
        {
            return _sqlSugarProvider.Insertable(entity).ExecuteCommandAsync();
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(dynamic keyValue)
        {
            return _sqlSugarProvider.Deleteable<TEntity>(keyValue).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>是否删除成功</returns>
        public Task<bool> DeleteAsync(dynamic keyValue)
        {
            return _sqlSugarProvider.Deleteable<TEntity>(keyValue).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(TEntity entity)
        {
            return _sqlSugarProvider.Deleteable(entity).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否删除成功</returns>
        public Task<bool> DeleteAsync(TEntity entity)
        {
            return _sqlSugarProvider.Deleteable(entity).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Deleteable<TEntity>().Where(expression).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>是否删除成功</returns>
        public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Deleteable<TEntity>().Where(expression).ExecuteCommandHasChangeAsync();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>是否删除成功</returns>
        public bool Delete(List<dynamic> keys)
        {
            return _sqlSugarProvider.Deleteable<TEntity>(keys).ExecuteCommandHasChange();
        }
        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="keys">主键集合</param>
        /// <returns>是否删除成功</returns>
       public Task<bool> DeleteAsync(List<dynamic> keys)
        {
            return _sqlSugarProvider.Deleteable<TEntity>(keys).ExecuteCommandHasChangeAsync();
        }
        #endregion

        #region 修改
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        /// <returns>是否更新成功</returns>
        public bool Update(TEntity entity, bool isNoUpdateNull = false)
        {
            return _sqlSugarProvider.Updateable(entity).IgnoreColumns(isNoUpdateNull).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        /// <returns>是否更新成功</returns>
        public Task<bool> UpdateAsync(TEntity entity, bool isNoUpdateNull = false)
        {
            return _sqlSugarProvider.Updateable(entity).IgnoreColumns(isNoUpdateNull).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        /// <returns></returns>
        public bool Update(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns)
        {
            return _sqlSugarProvider.Updateable(entity).IgnoreColumns(ignoreColumns).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns)
        {
            return _sqlSugarProvider.Updateable(entity).IgnoreColumns(ignoreColumns).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        /// <returns></returns>
        public bool Update(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns)
        {
            return _sqlSugarProvider.Updateable(entity).Where(expression).IgnoreColumns(ignoreColumns).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns)
        {
            return _sqlSugarProvider.Updateable(entity).Where(expression).IgnoreColumns(ignoreColumns).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 通过条件修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>是否成功</returns>
        public bool Update(TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Updateable(entity).Where(expression).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 通过条件修改（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>是否成功</returns>
        public Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Updateable(entity).Where(expression).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        /// <returns>是否修改成功</returns>
        public bool Update(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition)
        {
            return _sqlSugarProvider.Updateable<TEntity>().UpdateColumns(expression).Where(condition).ExecuteCommandHasChange();
        }

        /// <summary>
        /// 修改指定字段（异步）
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        /// <returns>是否修改成功</returns>
        public Task<bool> UpdateAsync(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition)
        {
            return _sqlSugarProvider.Updateable<TEntity>().UpdateColumns(expression).Where(condition).ExecuteCommandHasChangeAsync();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取所有集合
        /// </summary>
        /// <returns>集合</returns>
        public List<TEntity> Queryable()
        {
            return _sqlSugarProvider.Queryable<TEntity>().ToList();
        }
        /// <summary>
        /// 获取所有集合（异步）
        /// </summary>
        /// <returns>集合</returns>
        public Task<List<TEntity>> QueryableAsync()
        {
            return _sqlSugarProvider.Queryable<TEntity>().ToListAsync();
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns>返回实体</returns>
        public TEntity FindEntity(object keyValue)
        {
            return _sqlSugarProvider.Queryable<TEntity>().InSingle(keyValue);
        }
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>返回实体</returns>
        public TEntity FindEntity(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).First();
        }
        /// <summary>
        /// 根据条件获取实体（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>返回实体</returns>
        public Task<TEntity> FindEntityAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).FirstAsync();
        }
        /// <summary>
        /// 查询数据总条数
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns>数据总数量</returns>
        public int QueryableCount(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
            {
                return _sqlSugarProvider.Queryable<TEntity>().Count();
            }
            return _sqlSugarProvider.Queryable<TEntity>().Count(expression);
        }
        /// <summary>
        /// 查询数据总条数（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        /// <returns></returns>
        public Task<int> QueryableCountAsync(Expression<Func<TEntity, bool>> expression)
        {
            if (expression == null)
            {
                return _sqlSugarProvider.Queryable<TEntity>().CountAsync();
            }
            return _sqlSugarProvider.Queryable<TEntity>().CountAsync(expression);
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>集合</returns>
        public List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).ToList();
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <returns>集合</returns>
        public Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).ToListAsync();
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <returns>集合</returns>
        public List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(orderby != null, orderby, isDesc ? OrderByType.Desc : OrderByType.Asc).ToList();
        }
        /// <summary>
        /// 根据条件获取集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <returns>集合</returns>
        public Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(orderby != null, orderby, isDesc ? OrderByType.Desc : OrderByType.Asc).ToListAsync();
        }
        /// <summary>
        /// 根据条件获取指定条数集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        /// <returns>集合</returns>
        public List<TEntity> Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(orderby != null, orderby, isDesc ? OrderByType.Desc : OrderByType.Asc).Take(top).ToList();
        }
        /// <summary>
        /// 根据条件获取指定条数集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        /// <returns>集合</returns>
        public Task<List<TEntity>> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top)
        {
            return _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(orderby != null, orderby, isDesc ? OrderByType.Desc : OrderByType.Asc).Take(top).ToListAsync();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>集合|总条数</returns>
        public Tuple<List<TEntity>, int> QueryableByPage(Expression<Func<TEntity, bool>>? expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int pageIndex, int pageSize)
        {
            var total = 0;
            return Tuple.Create(_sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression).OrderByIF(orderby != null, orderby, isDesc ? OrderByType.Desc : OrderByType.Asc).ToPageList(pageIndex, pageSize, ref total), total);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>集合|总条数</returns>
        public Tuple<List<TEntity>, int> QueryableByPage(Expression<Func<TEntity, bool>> expression, Dictionary<Expression<Func<TEntity, object>>, OrderByType> orderby, int pageIndex, int pageSize)
        {
            var total = 0;
            var query = _sqlSugarProvider.Queryable<TEntity>().WhereIF(expression != null, expression);
            foreach (var item in orderby)
            {
                query.OrderBy(item.Key, item.Value);
            }
            return Tuple.Create(query.ToPageList(pageIndex, pageSize, ref total), total);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionals">查询条件</param>
        /// <param name="orderFileds">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public Tuple<List<TEntity>, int> QueryableByPage(List<IConditionalModel> conditionals, string orderFileds, int pageIndex, int pageSize)
        {
            var total = 0;
            var query = _sqlSugarProvider.Queryable<TEntity>();
            if (conditionals != null)
                query.Where(conditionals);

            query.OrderByIF(!string.IsNullOrWhiteSpace(orderFileds), orderFileds);
            return Tuple.Create(query.ToPageList(pageIndex, pageSize, ref total), total);
        }
        #endregion
    }
}
