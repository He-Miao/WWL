using HomeAPI.IService;
using HomeAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace HomeAPI.Controllers
{
    /// <summary>
    /// 上传控制器
    /// </summary>
    [Route("[controller]")]
    public class UploadController : BaseController
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadImage")]
        public IActionResult UploadImage()
        {
            IFormFile? file = Request.Form.Files[0];
            ResultData data =  _uploadService.UploadPicture(file);
            return Ok(data);
        }
    }
}
