using System.ComponentModel;

namespace HomeAPI.Model.Enums
{
    /// <summary>
    ///  审核状态
    /// </summary>
    public enum AuditState
    {
        /// <summary>
        /// 审核通过
        /// </summary>
        [Description("通过")]
        Pass = 1,
        /// <summary>
        /// 审核不通过
        /// </summary>
        [Description("不通过")]
        NoPass = 2,
        /// <summary>
        /// 待审核
        /// </summary>
        [Description("待审核")]
        Audit = 3
    }

    /// <summary>
    ///  账目类别
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// 餐饮美食
        /// </summary>
        [Description("餐饮美食")]
        Catering = 1,

        /// <summary>
        /// 服饰装扮
        /// </summary>
        [Description("服饰装扮")]
        Clothing = 2,

        /// <summary>
        /// 日用百货
        /// </summary>
        [Description("日用百货")]
        DailyNecessities = 3,

        /// <summary>
        /// 家居家装
        /// </summary>
        [Description("家居家装")]
        HomeFurnishing = 4,

        /// <summary>
        /// 数码产品
        /// </summary>
        [Description("数码产品")]
        Electronics = 5,

        /// <summary>
        /// 电器
        /// </summary>
        [Description("电器")]
        Appliances = 6,

        /// <summary>
        /// 美发
        /// </summary>
        [Description("美发")]
        Hairdressing = 7,

        /// <summary>
        /// 美容
        /// </summary>
        [Description("美容")]
        Beauty = 8,

        /// <summary>
        /// 交通出行
        /// </summary>
        [Description("交通出行")]
        Transportation = 9,

        /// <summary>
        /// 住房物业
        /// </summary>
        [Description("住房物业")]
        Housing = 10,

        /// <summary>
        /// 酒店旅行
        /// </summary>
        [Description("酒店旅行")]
        HotelTravel = 11,

        /// <summary>
        /// 户外游玩
        /// </summary>
        [Description("户外游玩")]
        OutdoorActivities = 12,

        /// <summary>
        /// 教育
        /// </summary>
        [Description("教育")]
        Education = 13,

        /// <summary>
        /// 书籍产品
        /// </summary>
        [Description("书籍产品")]
        Books = 14,

        /// <summary>
        /// 医疗健康
        /// </summary>
        [Description("医疗健康")]
        Healthcare = 15,

        /// <summary>
        /// 公益捐赠
        /// </summary>
        [Description("公益捐赠")]
        CharityDonation = 16,

        /// <summary>
        /// 投资理财
        /// </summary>
        [Description("投资理财")]
        Investment = 17,

        /// <summary>
        /// 保险
        /// </summary>
        [Description("保险")]
        Insurance = 18,

        /// <summary>
        /// 信用借还
        /// </summary>
        [Description("信用借还")]
        CreditBorrowing = 19,

        /// <summary>
        /// 话费充值
        /// </summary>
        [Description("话费充值")]
        PhoneBillRecharge = 20,

        /// <summary>
        /// 收入
        /// </summary>
        [Description("收入")]
        Income = 21,

        /// <summary>
        /// 人情来往
        /// </summary>
        [Description("人情来往")]
        HumanRelations = 22,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 23,
    }

}
