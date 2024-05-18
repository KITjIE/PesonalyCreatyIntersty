using Newtonsoft.Json;
using System;
using System.IO;
using WPFTemplateLib;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplate
{
    /// <summary>
    /// 配置帮助类
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static string _configFilePath = Path.Combine(@"C:\APPConfig", $"{nameof(ConfigItems)}.txt");//AppDomain.CurrentDomain.BaseDirectory

        /// <summary>
        /// 配置文件备份路径
        /// </summary>
        private static string _configFileBakPath => $"{_configFilePath}.bak";

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <returns></returns>
        public static bool SaveConfig(IConfigItems configs)
        {
            bool result = true;

            try
            {
                string configStr = JsonConvert.SerializeObject(configs);

                if (File.Exists(_configFilePath))
                {
                    File.Copy(_configFilePath, _configFileBakPath, true);
                }

                File.WriteAllText(_configFilePath, configStr);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 载入配置
        /// </summary>
        /// <returns></returns>
        public static bool LoadConfig<T>(ref T configs)
        where T : IConfigItems, new()
        {
            bool result = true;

            try
            {
                if (!File.Exists(_configFilePath))
                {
                    if (File.Exists(_configFileBakPath))
                    {
                        File.Copy(_configFileBakPath, _configFilePath, true);
                    }
                }

                if (File.Exists(_configFilePath))
                {
                    string configStr = File.ReadAllText(_configFilePath);
                    configs = JsonConvert.DeserializeObject<T>(configStr);
                }
                else
                {
                    result = false;

                    if (configs == null)
                    {
                        configs = new T();
                    }

                    SaveConfig(configs);

                    Console.WriteLine($"配置文件[{_configFilePath}]不存在，已自动生成。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }

            return result;
        }
    }
}
