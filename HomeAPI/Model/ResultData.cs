using Common.Helpers;
using HomeAPI.Model.Enums;
namespace HomeAPI.Model
{
    /// <summary>
    /// 接口响应数据格式
    /// </summary>
    public class ResultData
    {
        private string _message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public ResultCode Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = String.IsNullOrWhiteSpace(value) ? Code.GetEnumDescription() : value;
            }
        }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; } = new object[] { };
    }
}
