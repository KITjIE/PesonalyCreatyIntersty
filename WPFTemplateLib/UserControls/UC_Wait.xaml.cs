using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
/*
 * 源码已托管：https://gitee.com/dlgcy/WPFTemplate
 */
namespace WPFTemplateLib.UserControls
{
    /// <summary>
    /// 等待动画用户控件
    /// （参考：https://www.bilibili.com/video/BV1BK411P76B?p=1）
    /// </summary>
    public partial class UC_Wait : UserControl
    {
        public UC_Wait()
        {
            InitializeComponent();
        }

        private void UC_Wait_OnLoaded(object sender, RoutedEventArgs e)
        {
            RunAnimation();
        }

        private void RunAnimation()
        {
            //定义动画;
            DoubleAnimation da = new DoubleAnimation()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(1000)),
                To = 1.6,
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true,
            };

            Task.Run(async () =>
            {
                for (int i = 0; i < 4; i++)
                {
                    Dispatcher.Invoke(() =>
                    {
                        var st = FindName($"ST{i + 1}") as ScaleTransform;
                        st?.BeginAnimation(ScaleTransform.ScaleXProperty, da);
                        st?.BeginAnimation(ScaleTransform.ScaleYProperty, da);
                    });

                    await Task.Delay(300);
                }
            });
        }
    }
}
