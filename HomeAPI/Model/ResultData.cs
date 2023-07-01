using Common.Enums;

namespace HomeAPI.Model
{
    public class ResultData
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public ResultCode Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }=new object();
    }
}
