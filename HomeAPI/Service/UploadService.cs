using Common.Helpers;
using HomeAPI.IService;
using HomeAPI.Model;
using HomeAPI.Model.Enums;

namespace HomeAPI.Service
{
    /// <summary>
    /// 上传服务逻辑层实现类
    /// </summary>
    public class UploadService : IUploadService
    {

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="file">请求文件接口</param>
        /// <returns></returns>
        public ResultData UploadPicture(IFormFile? file)
        {
            ResultData result = new ResultData();
            if (file != null && file.Length > 0)
            {
                //构造图片存储目录
                string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\pictures\\bill");
                saveDirectory.EnsureDirectoryExists();
                string filePath = Path.Combine(saveDirectory, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                //构造图片路径
                filePath = string.Format("{0}/uploads/pictures/bill/{1}", AppSettingsHelper.Get("Urls", false), file.FileName);
               result.Code = ResultCode.Success;
                result.Msg = "上传图片成功";
                result.Data = filePath;
                return result;
            }
            result.Code = ResultCode.Error;
            result.Msg = "文件上传失败";
            return result;
        }
    }
}
