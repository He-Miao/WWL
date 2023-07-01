using SqlSugar;

namespace HomeAPI.Model.Models
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class OperationLog
    {
        private System.Int32 _Id;
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 Id { get { return this._Id; } set { this._Id = value; } }

        private System.String _IP;
        /// <summary>
        /// 
        /// </summary>
        public System.String IP { get { return this._IP; } set { this._IP = value?.Trim(); } }

        private System.String _Os;
        /// <summary>
        /// 操作系统
        /// </summary>
        public System.String Os { get { return this._Os; } set { this._Os = value?.Trim(); } }

        private System.String _DeviceInfo;
        /// <summary>
        /// 设备信息
        /// </summary>
        public System.String DeviceInfo { get { return this._DeviceInfo; } set { this._DeviceInfo = value?.Trim(); } }

        private System.String _ElapsedMilliseconds;
        /// <summary>
        /// 耗时（毫秒）
        /// </summary>
        public System.String ElapsedMilliseconds { get { return this._ElapsedMilliseconds; } set { this._ElapsedMilliseconds = value?.Trim(); } }

        private System.String _ApiPath;
        /// <summary>
        /// 接口地址
        /// </summary>
        public System.String ApiPath { get { return this._ApiPath; } set { this._ApiPath = value?.Trim(); } }

        private System.String _Params;
        /// <summary>
        /// 操作参数
        /// </summary>
        public System.String Params { get { return this._Params; } set { this._Params = value?.Trim(); } }

        private System.String _Result;
        /// <summary>
        /// 操作结果
        /// </summary>
        public System.String Result { get { return this._Result; } set { this._Result = value?.Trim(); } }
    }
}