using HomeAPI.IRepository;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Enums;
using SqlSugar;
using System.Linq.Expressions;

namespace HomeAPI.Service
{
    /// <summary>
    /// 数据库增、删、改、查操作业务逻辑层基类
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

        /// <summary>
        /// 异步插入数据（适用于id自动增长）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回主键ID</returns>
        public virtual async Task<ResultData> AddScalarAsync(TEntity entity)
        {
            ResultData result = new ResultData();
            if (await _repository.AddScalarAsync(entity) > 0)
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回是否插入成功</returns>
        public virtual ResultData Insert(TEntity entity)
        {
            ResultData result = new ResultData();
            if ( _repository.Insert(entity))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 异步插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual async Task<ResultData> InsertAsync(TEntity entity)
        {
            ResultData result = new ResultData();
            if (await _repository.InsertAsync(entity) > 0)
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回是否插入成功</returns>
        public virtual ResultData Insert(List<TEntity> entity)
        {
            ResultData result = new ResultData();
            if (_repository.Insert(entity))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 异步批量插入数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual async Task<ResultData> InsertAsync(List<TEntity> entity)
        {
            ResultData result = new ResultData();
            if (await _repository.InsertAsync(entity) > 0)
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

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public virtual ResultData Delete(dynamic keyValue)
        {
            ResultData result = new ResultData();
            if (_repository.Delete(keyValue))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="keyValue">主键</param>
        public virtual async Task<ResultData> DeleteAsync(dynamic keyValue)
        {
            ResultData result = new ResultData();
            if (await _repository.DeleteAsync(keyValue) > 0)
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual ResultData Delete(TEntity entity)
        {
            ResultData result = new ResultData();
            if (_repository.Delete(entity))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        public virtual async Task<ResultData> DeleteAsync(TEntity entity)
        {
            ResultData result = new ResultData();
            if (await _repository.DeleteAsync(entity))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="expression">条件</param>
        public virtual ResultData Delete(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            if (_repository.Delete(expression))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="expression">条件</param>
        public virtual async Task<ResultData> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            if (await _repository.DeleteAsync(expression))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="keys">主键集合</param>
        public virtual ResultData Delete(List<dynamic> keys)
        {
            ResultData result = new ResultData();
            if (_repository.Delete(keys))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        /// <summary>
        /// 批量删除（异步）
        /// </summary>
        /// <param name="keys">主键集合</param>
        public virtual async Task<ResultData> DeleteAsync(List<dynamic> keys)
        {
            ResultData result = new ResultData();
            if (await _repository.DeleteAsync(keys))
            {
                result.Code = ResultCode.Success;
                result.Msg = "删除成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "删除失败";
            return result;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        public virtual ResultData Update(TEntity entity, bool isNoUpdateNull = false)
        {
            ResultData result = new ResultData();
            if (_repository.Update(entity, isNoUpdateNull))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 通过主键修改（包含是否需要将null值字段提交到数据库）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="isNoUpdateNull">是否排除NULL值字段更新</param>
        public virtual async Task<ResultData> UpdateAsync(TEntity entity, bool isNoUpdateNull = false)
        {
            ResultData result = new ResultData();
            if (await  _repository.UpdateAsync(entity, isNoUpdateNull))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        public virtual ResultData Update(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns)
        {
            ResultData result = new ResultData();
            if (_repository.Update(entity, ignoreColumns))
            {
                result.Code = ResultCode.Success;
                result.Msg = "修改成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "修改失败";
            return result;
        }

        /// <summary>
        /// 通过主键修改（更新实体部分字段）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="ignoreColumns">不更新字段</param>
        public virtual async Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> ignoreColumns)
        {
            ResultData result = new ResultData();
            if (await _repository.UpdateAsync(entity, ignoreColumns))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        public virtual ResultData Update(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns)
        {
            ResultData result = new ResultData();
            if (_repository.Update(entity, expression, ignoreColumns))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 通过条件更新(不更新忽略字段)
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">条件</param>
        /// <param name="ignoreColumns">忽略更新的字段</param>
        public virtual async Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> ignoreColumns)
        {
            ResultData result = new ResultData();
            if (await _repository.UpdateAsync(entity, expression, ignoreColumns))
            {
                result.Code = ResultCode.Success;
                result.Msg = "新增成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "新增失败";
            return result;
        }

        /// <summary>
        /// 通过条件修改
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        public virtual ResultData Update(TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            if (_repository.Update(entity, expression))
            {
                result.Code = ResultCode.Success;
                result.Msg = "修改成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "修改失败";
            return result;
        }

        /// <summary>
        /// 通过条件修改（异步）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">Lambda表达式</param>
        public virtual async Task<ResultData> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            if (await _repository.UpdateAsync(entity, expression))
            {
                result.Code = ResultCode.Success;
                result.Msg = "修改成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "修改失败";
            return result;
        }

        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        public virtual ResultData Update(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition)
        {
            ResultData result = new ResultData();
            if (_repository.Update(expression, condition))
            {
                result.Code = ResultCode.Success;
                result.Msg = "修改成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "修改失败";
            return result;
        }

        /// <summary>
        /// 修改指定字段（异步）
        /// </summary>
        /// <param name="expression">需要修改的字段</param>
        /// <param name="condition">Lambda表达式条件</param>
        public virtual async Task<ResultData> UpdateAsync(Expression<Func<TEntity, object>> expression, Expression<Func<TEntity, bool>> condition)
        {
            ResultData result = new ResultData();
            if (await _repository.UpdateAsync(expression, condition))
            {
                result.Code = ResultCode.Success;
                result.Msg = "修改成功";
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "修改失败";
            return result;
        }
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
        /// <summary>
        /// 获取所有集合（异步）
        /// </summary>
        public virtual async Task<ResultData> QueryableAsync()
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data =await  _repository.QueryableAsync();
            return result;
        }
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        public virtual ResultData FindEntity(object keyValue)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.FindEntity(keyValue);
            return result;
        }
        /// <summary>
        /// 根据条件获取实体
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        public virtual ResultData FindEntity(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.FindEntity(expression);
            return result;
        }
        /// <summary>
        /// 根据条件获取实体（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        public virtual async Task<ResultData> FindEntityAsync(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data =await _repository.FindEntityAsync(expression);
            return result;
        }
        /// <summary>
        /// 查询数据总条数
        /// </summary>
        /// <param name="expression">条件（为null则查询全部数据）</param>
        public virtual ResultData QueryableCount(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.QueryableCount(expression);
            return result;
        }
        /// <summary>
        /// 查询数据总条数（异步）
        /// </summary>
        /// <param name="expression">条件（为null则查询全部数据）</param>
        public virtual async Task<ResultData> QueryableCountAsync(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data =await  _repository.QueryableCountAsync(expression);
            return result;
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">条件（为null则查询全部数据）</param>
        public virtual ResultData Queryable(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.Queryable(expression);
            return result;
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        public virtual async Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = await _repository.QueryableAsync(expression);
            return result;
        }
        /// <summary>
        /// 根据条件获取集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        public virtual ResultData Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.Queryable(expression, orderby, isDesc);
            return result;
        }
        /// <summary>
        /// 根据条件获取集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        public virtual async Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = await _repository.QueryableAsync(expression, orderby, isDesc);
            return result;
        }
        /// <summary>
        /// 根据条件获取指定条数集合
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        public virtual ResultData Queryable(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.Queryable(expression, orderby, isDesc, top);
            return result;
        }
        /// <summary>
        /// 根据条件获取指定条数集合（异步）
        /// </summary>
        /// <param name="expression">Lambda表达式</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="top">前N条数据</param>
        public virtual async Task<ResultData> QueryableAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int top)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = await _repository.QueryableAsync(expression, orderby, isDesc, top);
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="isDesc">是否降序排列</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        public virtual ResultData QueryableByPage(Expression<Func<TEntity, bool>>? expression, Expression<Func<TEntity, object>> orderby, bool isDesc, int pageIndex, int pageSize)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.QueryableByPage(expression, orderby, isDesc, pageIndex, pageSize);
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        public virtual ResultData QueryableByPage(Expression<Func<TEntity, bool>> expression, Dictionary<Expression<Func<TEntity, object>>, OrderByType> orderby, int pageIndex, int pageSize)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.QueryableByPage(expression, orderby, pageIndex, pageSize);
            return result;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="conditionals">查询条件</param>
        /// <param name="orderFileds">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        public virtual ResultData QueryableByPage(List<IConditionalModel> conditionals, string orderFileds, int pageIndex, int pageSize)
        {
            ResultData result = new ResultData();
            result.Code = ResultCode.Success;
            result.Data = _repository.QueryableByPage(conditionals, orderFileds, pageIndex, pageSize);
            return result;
        }
        #endregion


    }
}
