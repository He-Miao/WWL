using SqlSugar;

namespace HomeAPI.Model.Models
{
    /// <summary>
    /// 开支表
    /// </summary>
    public class Expense
    {
        private System.Int32 _ExpenseId;
        /// <summary>
        /// 开支表
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ExpenseId { get { return this._ExpenseId; } set { this._ExpenseId = value; } }

        private System.String _Title;
        /// <summary>
        /// 标题
        /// </summary>
        public System.String Title { get { return this._Title; } set { this._Title = value?.Trim(); } }

        private System.Int32 _Amount;
        /// <summary>
        /// 支出金额（单位分）
        /// </summary>
        public System.Int32 Amount { get { return this._Amount; } set { this._Amount = value; } }

        private System.DateTime _PayDate;
        /// <summary>
        /// 日期
        /// </summary>
        public System.DateTime PayDate { get { return this._PayDate; } set { this._PayDate = value; } }

        private System.Int32 _Classify;
        /// <summary>
        /// 支出分类
        /// </summary>
        public System.Int32 Classify { get { return this._Classify; } set { this._Classify = value; } }

        private System.DateTime _CreationTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreationTime { get { return this._CreationTime; } set { this._CreationTime = value; } }

        private System.Int32 _AuditState;
        /// <summary>
        /// 审核状态
        /// </summary>
        public System.Int32 AuditState { get { return this._AuditState; } set { this._AuditState = value; } }

        private System.Byte[] _VoucherImg;
        /// <summary>
        /// 凭证图
        /// </summary>
        public System.Byte[] VoucherImg { get { return this._VoucherImg; } set { this._VoucherImg = value; } }

        private System.String _Explain;
        /// <summary>
        /// 说明
        /// </summary>
        public System.String Explain { get { return this._Explain; } set { this._Explain = value?.Trim(); } }
    }
}