using System;
using System.IO;
using System.Text;
/*
 * 代码已托管 https://gitee.com/dlgcy/dotnetcodes/tree/dlgcy/DotNet.Utilities/ConsoleHelper
 */
namespace DotNet.Utilities.ConsoleHelper
{
    /// <summary>
    /// [dlgcy] Console 输出重定向
    /// 其他版本：DotNet.Utilities.WinformHelper.TextBoxWriter
    /// 用法示例：
    /// 在构造器里加上：Console.SetOut(new ConsoleWriter(s => { LogHelper.Write(s); }));
    /// </summary>
    /// <example>
    /// <code>
    /// public class Example
    /// {
    ///     public Example()
    ///     {
    ///         Console.SetOut(new ConsoleWriter(s => { LogHelper.Write(s); }));
    ///     }
    /// }
    /// </code>
    /// </example>
    public class ConsoleWriter : TextWriter
    {
        private readonly Action<string> _Write;
        private readonly Action<string> _WriteLine;
        private readonly Action<string, string, string, int> _WriteCallerInfo;

        /// <summary>
        /// Console 输出重定向
        /// </summary>
        /// <param name="write">日志方法委托（针对于 Write）</param>
        /// <param name="writeLine">日志方法委托（针对于 WriteLine）</param>
        public ConsoleWriter(Action<string> write, Action<string> writeLine)
        {
            _Write = write;
            _WriteLine = writeLine;
        }

        /// <summary>
        /// Console 输出重定向
        /// </summary>
        /// <param name="write">日志方法委托</param>
        public ConsoleWriter(Action<string> write)
        {
            _Write = write;
            _WriteLine = write;
        }

        /// <summary>
        /// Console 输出重定向（带调用方信息）
        /// </summary>
        /// <param name="write">日志方法委托（后三个参数为 CallerFilePath、CallerMemberName、CallerLineNumber）</param>
        public ConsoleWriter(Action<string, string, string, int> write)
        {
            _WriteCallerInfo = write;
        }

        /// <summary>
        /// 使用 UTF-16 避免不必要的编码转换
        /// </summary>
        public override Encoding Encoding => Encoding.Unicode;

        /// <summary>
        /// 最低限度需要重写的方法
        /// </summary>
        /// <param name="value">消息</param>
        public override void Write(string value)
        {
            if (_WriteCallerInfo != null)
            {
                WriteWithCallerInfo(value);
                return;
            }

            _Write(value);
        }

        /// <summary>
        /// 为提高效率直接处理一行的输出
        /// </summary>
        /// <param name="value">消息</param>
        public override void WriteLine(string value)
        {
            if (_WriteCallerInfo != null)
            {
                WriteWithCallerInfo(value);
                return;
            }

            _WriteLine(value);
        }

        /// <summary>
        /// 带调用方信息进行写消息
        /// </summary>
        /// <param name="value">消息</param>
        private void WriteWithCallerInfo(string value)
        {
            //3、System.Console.WriteLine -> 2、System.IO.TextWriter + SyncTextWriter.WriteLine -> 1、DotNet.Utilities.ConsoleHelper.ConsoleWriter.WriteLine -> 0、DotNet.Utilities.ConsoleHelper.ConsoleWriter.WriteWithCallerInfo
            var callInfo = ClassHelper.GetMethodInfo(4);
            _WriteCallerInfo(value, callInfo?.FileName, callInfo?.MethodName, callInfo?.LineNumber ?? 0);
        }
    }
}
