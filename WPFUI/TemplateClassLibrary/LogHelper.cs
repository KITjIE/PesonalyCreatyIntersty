using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateClassLibrary
{
    public static class LogHelper
    {
        static readonly ILog _logger = LogManager.GetLogger("LogTrace");
        public static void Info(string message)
        {
            _logger.Info(message);                               //打印事件
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);                             //调试
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);                              //警告
        }

        public static void Error(string message)
        {
            _logger.Error(message);                             //错误
        }
    }

}
