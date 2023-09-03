using System.ComponentModel;
using System.Reflection;
namespace Common.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// 根据枚举值获取枚举说明
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDescription(this Enum value)
        {
            // 获取枚举类型的类型
            Type enumType = value.GetType();
            // 获取枚举值对应的字段信息
            System.Reflection.FieldInfo? fieldInfo = enumType.GetField(value.ToString());
            // 获取字段上的 DescriptionAttribute 特性
            if (fieldInfo != null)
            {
                System.ComponentModel.DescriptionAttribute[] attributes =
                    (System.ComponentModel.DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                        typeof(System.ComponentModel.DescriptionAttribute), false);
                // 返回 Description 属性的值，如果找到了则返回该值，否则返回枚举值的字符串形式
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }
            return value.ToString();
        }

        /// <summary>
        ///  通过枚举说明获取其枚举值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="description">说明</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T GetEnumValueFromDescription<T>(string description) where T : struct
        {
            // 检查泛型参数是否是枚举类型
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T必须是枚举类型");
            }
            // 遍历枚举值
            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                // 获取枚举字段
                FieldInfo fieldInfo = typeof(T).GetField(enumValue.ToString());
                if (fieldInfo != null)
                {
                    // 获取 DescriptionAttribute 特性
                    var attribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
                    // 如果 DescriptionAttribute 存在并且描述匹配，则返回枚举值
                    if (attribute != null && attribute.Description == description)
                    {
                        return enumValue;
                    }
                }
            }
            // 找不到匹配的枚举值
            throw new ArgumentException("找不到与给定描述匹配的枚举值");
        }
    }
}
