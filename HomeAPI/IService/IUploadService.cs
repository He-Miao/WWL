using HomeAPI.Model;

namespace HomeAPI.IService
{
    /// <summary>
    /// 上传服务逻辑接口
    /// </summary>
    public interface IUploadService
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">文件接口</param>
        /// <returns></returns>
       ResultData UploadPicture(IFormFile? file);
    }
}
