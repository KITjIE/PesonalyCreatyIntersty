using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFTemplateLib.WpfHelpers
{
    /// <summary>
    /// 导出图片帮助类
    /// </summary>
    public class ExportPicHelper
    {
        /// <summary>
        /// 保存为图片
        /// (修改自：https://blog.csdn.net/dhl11/article/details/108621634)
        /// </summary>
        /// <param name="frameworkElement">可视化元素，可以是Grid、StackPanel等类型的所有可视化元素</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="errorMsg">错误消息</param>
        /// <returns>是否成功</returns>
        public static bool SaveToImage(FrameworkElement frameworkElement, string filePath, out string errorMsg)
        {
            try
            {
                errorMsg = string.Empty;
                FileStream fs = new FileStream(filePath, FileMode.Create);
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                    (int)frameworkElement.ActualWidth, 
                    (int)frameworkElement.ActualHeight, 
                    1 / 96, 1 / 96, PixelFormats.Default);

                bmp.Render(frameworkElement);
                BitmapEncoder encoder = new TiffBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                encoder.Save(fs);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存图片异常：{ex}");
                errorMsg = ex.Message;
                return false;
            }
        }
	}
}
