using System;
using System.IO;
using System.Windows;
using WPFTemplateLib.WpfHelpers;

namespace WPFTemplateLib.Attached
{
    /// <summary>
    /// 导出图片附加属性类
    /// </summary>
    public class ExportPicAttached : DependencyObject
    {
        #region 是否开始导出

        public static bool GetIsExporting(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsExportingProperty);
        }

        public static void SetIsExporting(DependencyObject obj, bool value)
        {
            obj.SetValue(IsExportingProperty, value);
        }

        /// <summary>
        /// 是否正在导出（运行时设置为 true 则将附加的元素导出为图片）
        /// </summary>
        public static readonly DependencyProperty IsExportingProperty =
            DependencyProperty.RegisterAttached("IsExporting", typeof(bool), typeof(ExportPicAttached),
                new PropertyMetadata(false, OnIsExportingValueChanged));

        private static void OnIsExportingValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;

            if (element == null)
                return;

            if ((e.NewValue as bool?) == false)
                return;

            try
            {
                string exportPath = GetExportPath(d);
                if (string.IsNullOrEmpty(exportPath))
                {
                    exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                        "Export");
                }

                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                }

                string filePath = Path.Combine(exportPath, $"{DateTime.Now:yyyyMMddHHmmss}.png");

                bool success = ExportPicHelper.SaveToImage(element, filePath, out string errorMsg);
                if (success)
                {
                    MessageBox.Show($"导出成功");
                }
                else
                {
                    Console.WriteLine($"导出失败：{errorMsg}");
                    MessageBox.Show($"导出失败 {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导出异常：{ex}");
                MessageBox.Show($"导出异常：{ex.Message}");
            }
            finally
            {
                //此处设置为 false 没什么用，还是需要业务层在设置为 true 前先设置为 false 才行。
                SetIsExporting(d, false);
            }
        }

        #endregion

        #region 导出文件夹

        public static string GetExportPath(DependencyObject obj)
        {
            return (string)obj.GetValue(ExportPathProperty);
        }

        public static void SetExportPath(DependencyObject obj, string value)
        {
            obj.SetValue(ExportPathProperty, value);
        }

        /// <summary>
        /// 导出文件夹路径
        /// </summary>
        public static readonly DependencyProperty ExportPathProperty =
            DependencyProperty.RegisterAttached("ExportPath", typeof(string), typeof(ExportPicAttached), new PropertyMetadata(string.Empty));

        #endregion
    }
}
