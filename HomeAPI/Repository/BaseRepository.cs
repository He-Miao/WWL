using HomeAPI.IRepository;
using SqlSugar;

namespace HomeAPI.Repository
{
    /// <summary>
    /// 数据仓储层基类
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
        #endregion

        #region 修改

        #endregion

        #region 删除

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
        #endregion
    }
}
