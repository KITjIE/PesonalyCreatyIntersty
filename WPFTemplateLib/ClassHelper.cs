using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
/*
 * 代码已托管 https://gitee.com/dlgcy/dotnetcodes/tree/dlgcy/DotNet.Utilities/Object
 */
namespace DotNet.Utilities
{
    public class ClassHelper
    {
        #region 深拷贝

        //————————————————
        //版权声明：本文为CSDN博主「韩山童」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
        //原文链接：https://blog.csdn.net/HQ1356466973/article/details/83658756

        /// <summary>
        /// 深拷贝，通过序列化与反序列化实现
        /// </summary>
        /// <typeparam name="T">深拷贝的类类型</typeparam>
        /// <param name="obj">深拷贝的类对象</param>
        /// <returns></returns>
        public static T DeepCopyByBin<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                //序列化成流
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                //反序列化成对象
                retval = bf.Deserialize(ms);
                ms.Close();
            }

            return (T)retval;
        }

        /// <summary>
        /// 深拷贝，通过反射实现
        /// </summary>
        /// <typeparam name="T">深拷贝的类类型</typeparam>
        /// <param name="obj">深拷贝的类对象</param>
        /// <returns></returns>
        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }

        #endregion

        #region 调用信息

        /* 参考：https://blog.csdn.net/m0_37886901/article/details/105266848 */

        /// <summary>
        /// 获取方法调用信息;
        /// </summary>
        /// <param name="index">0是本身，1是调用方，2是调用方的调用方...以此类推</param>
        /// <returns>MethodInfo 对象</returns>
        public static MethodInfo GetMethodInfo(int index)
        {
            try
            {
                index++; //由于这里是封装了方法，相当于上端想要获取本身，其实对于这里而言，上端的本身就是这里的上端，所以需要+1，以此类推
                var stack = new StackTrace(true);

                //0是本身，1是调用方，2是调用方的调用方...以此类推
                var currentFrame = stack.GetFrame(index);
                var method = currentFrame.GetMethod();
                var module = method.Module;
                var declaringType = method.DeclaringType;
                var stackFrames = stack.GetFrames();

                string callChain = string.Join(" -> ", stackFrames.Select((r, i) =>
                {
                    if (i == 0) return null;
                    var m = r.GetMethod();
                    return $"{m.DeclaringType.FullName}.{m.Name}";
                }).Where(r => !string.IsNullOrWhiteSpace(r)).Reverse());

                return new MethodInfo()
                {
                    Method = method,
                    ModuleName = module.Name,
                    Namespace = declaringType.Namespace,
                    ClassName = declaringType.Name,
                    FullClassName = declaringType.FullName,
                    MethodName = method.Name,
                    CallChain = callChain,
                    LineNumber = currentFrame.GetFileLineNumber(),
                    FileName = currentFrame.GetFileName(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 方法调用信息
        /// </summary>
        public class MethodInfo
        {
            /// <summary>
            /// 方法完整信息;
            /// </summary>
            public MethodBase Method { get; set; }

            /// <summary>
            /// 模块名
            /// </summary>
            public string ModuleName { get; set; }

            /// <summary>
            /// 命名空间
            /// </summary>
            public string Namespace { get; set; }

            /// <summary>
            /// 类名
            /// </summary>
            public string ClassName { get; set; }

            /// <summary>
            /// 完整类名
            /// </summary>
            public string FullClassName { get; set; }

            /// <summary>
            /// 方法名
            /// </summary>
            public string MethodName { get; set; }

            /// <summary>
            /// 调用链
            /// </summary>
            public string CallChain { get; set; }

            /// <summary>
            /// 行号
            /// </summary>
            public int LineNumber { get; set; }

            /// <summary>
            /// 文件名
            /// </summary>
            public string FileName { get; set; }
        }

        #endregion
    }
}
