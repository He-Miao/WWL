namespace HomeAPI.IRepository
{
    /// <summary>
    /// 仓储通用操作接口
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
        List<TEntity> Queryable();
        #endregion
    }
}
