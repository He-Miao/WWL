using Common.Enums;
using HomeAPI.IRepository;
using HomeAPI.IService;
using HomeAPI.Model;

namespace HomeAPI.Service
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity>:IBaseService<TEntity>where TEntity : class,new()
    {
        private IBaseRepository<TEntity> _repository;
        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #region 新增
        /// <summary>
        /// 插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        public virtual ResultData AddScalar(TEntity entity)
        {
            ResultData result = new ResultData();
            if (_repository.AddScalar(entity) > 0)
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
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
        public virtual ResultData Queryable()
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.Queryable();
            return result;
        }
        #endregion


    }
}
