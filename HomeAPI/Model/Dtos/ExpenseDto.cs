namespace HomeAPI.Model.Dtos
{
    /// <summary>
    /// 开支
    /// </summary>
    public class ExpenseDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ExpenseId { get; set; } = 0;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 支出金额（单位分）
        /// </summary>
        public int Amount { get; set; } = 0;
        /// <summary>
        /// 购买日期
        /// </summary>
        public string PayDate { get; set; }
        /// <summary>
        /// 支出分类
        /// </summary>
        public string Classify { get; set; }

        private string _creationTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime
        {
            get
            {
                return _creationTime;
            }
            set
            {
                _creationTime = string.IsNullOrEmpty(value) ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : value;
            }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public string AuditState { get; set; }

        /// <summary>
        /// 凭证图
        /// </summary>
        public string VoucherImg { get; set; } = string.Empty;
        /// <summary>
        /// 说明
        /// </summary>
        public string Explain { get; set; } = string.Empty;
    }
}
