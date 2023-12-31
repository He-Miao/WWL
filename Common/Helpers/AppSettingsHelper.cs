﻿using Microsoft.Extensions.Configuration;


namespace Common.Helpers
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public class AppSettingsHelper
    {
        static IConfiguration _config;
        public AppSettingsHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        /// <summary>
        ///  根据键获取配置值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="IsConn">是否获取 ConnectionStrings 配置的内容，是则为true</param>
        /// <returns></returns>
        public static string Get(string key, bool IsConn = false)
        {
            string value;
            try
            {
                if (string.IsNullOrWhiteSpace(key)) return string.Empty;
                if (IsConn)
                {
                    value = _config.GetConnectionString(key);
                }
                else
                {
                    value = _config[key];
                }
            }
            catch (Exception)
            {
                value = string.Empty;
            }
            return value;
        }
        public static string Get(params string[] sessions)
        {
            try
            {
                if (sessions.Any())
                {
                    return _config[string.Join(":", sessions)];
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        public static string Get(string key)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(key)) return null;
                return _config[key];
            }
            catch
            {
                return null;
            }
        }
        public static List<T> Get<T>(params string[] session)
        {
            var list = new List<T>();
            _config.Bind(string.Join(":", session), list);
            return list;
        }
        public static IConfigurationSection GetSection(string key)
        {
            try
            {
                return _config.GetSection(key);
            }
            catch
            {
                return null;
            }
        }
    }
}
