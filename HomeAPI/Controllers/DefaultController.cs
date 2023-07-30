using HomeAPI.Model;
using HomeAPI.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Reflection;

namespace HomeAPI.Controllers
{
    /// <summary>
    ///  其它常用的公共接口
    /// </summary>
    [Route("[controller]")]
    public class DefaultController : BaseController
    {

        /// <summary>
        /// 获取枚举值集合
        /// </summary>
        /// <param name="enumName">枚举名</param>
        /// <returns></returns>
        [HttpGet("GetEnumList")]
        public IActionResult GetEnumValuesAndDescriptions(string enumName)
        {
            ResultData data = new ResultData();
            var enumType = GetEnumType(enumName);
            if (enumType != null)
            {
                var enumValues = GetAllValuesAndDescriptions(enumType);
                data.Data = enumValues;
                return Ok(data);
            }
            data.Code = ResultCode.Error;
            data.Msg = "类型必须是枚举！";
            return Ok(data);
        }





















        #region 获取枚举方法

        /// <summary>
        /// 获取枚举的值和说明
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static List<object> GetAllValuesAndDescriptions(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            var enumValues = new List<object>();
            foreach (var value in values)
            {
                enumValues.Add(new
                {
                    Value = (int)value,
                    Description = GetDescription(enumType, value)
                });
            }
            return enumValues;
        }

        /// <summary>
        /// 获取枚举说明文字
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        private static string GetDescription(Type enumType, object value)
        {
            var field = enumType.GetField(value.ToString());
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        /// <summary>
        ///  获取枚举类型
        /// </summary>
        /// <param name="enumName">枚举名</param>
        /// <returns></returns>
        private Type GetEnumType(string enumName)
        {
            var enumTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsEnum)
                .Where(t => t.Name.Equals(enumName, StringComparison.OrdinalIgnoreCase));
            return enumTypes.FirstOrDefault();
        }

        #endregion

    }
}
