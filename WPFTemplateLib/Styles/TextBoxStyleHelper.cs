using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFTemplateLib.Styles
{
    public partial class TextBoxStyleHelper
    {
        #region 限制仅数字

        /// <summary>
        /// 字符输入预处理事件方法（限制仅数字）
        /// </summary>
        private void PreviewTextInput_ForNumberOnly(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+"); //匹配非数字;
            e.Handled = re.IsMatch(e.Text);  //匹配上则输入中断（已处理）;
        }

        /// <summary>
        /// 按键按下事件方法（限制仅数字）
        /// （由于 "空格" 未被输入事件捕捉，所以在这里处理）
        /// </summary>
        private void PreviewKeyDown_ForNumberOnly(object sender, KeyEventArgs e)
        {
            // 按下空格键时，使输入中断
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 文本改变事件方法（限制仅数字）
        /// （用于处理粘贴的情况，来源：http://www.bubuko.com/infodetail-3702884.html）
        /// </summary>
        private void TextChanged_ForNumberOnly(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TextChange[] changes = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(changes, 0);

            //引起Text改变的字符串的起点
            int offset = changes[0].Offset;
            //引起Text改变的字符串的长度
            if (changes[0].AddedLength > 0)
            {
                Regex regex = new Regex(@"[^0-9]+"); //匹配非数字;
                bool isMatch = regex.IsMatch(textBox.Text);
                if (isMatch)
                {
                    /*最新的Text中若含有非法字符，由于我们已经从键盘输入事件中屏蔽了非法字符，所以基本可以断定此非法输入是由于"粘贴"引起的。*/

                    //移除粘贴的内容;
                    textBox.Text = textBox.Text.Remove(offset, changes[0].AddedLength);
                    //控制光标位置，使其还能定位到变动前的位置
                    textBox.SelectionStart = offset;
                }
            }
        }

        #endregion

        /// <summary>
        /// 文本改变事件方法（禁用中文）
        /// </summary>
        private void TextChanged_ForNoChinese(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TextChange[] changes = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(changes, 0);

            //引起Text改变的字符串的起点
            int offset = changes[0].Offset;
            //引起Text改变的字符串的长度
            if (changes[0].AddedLength > 0)
            {
                Regex regex = new Regex("^[\x20-\x7E]+$"); //数字、英文、符号 （非中文）;
                bool isMatch = regex.IsMatch(textBox.Text);
                if (!isMatch) //不是 数字、英文、符号;
                {
                    /*最新的Text中若含有非法字符，由于我们已经从键盘输入事件中屏蔽了非法字符，所以基本可以断定此非法输入是由于"粘贴"引起的。*/

                    //移除粘贴的内容;
                    textBox.Text = textBox.Text.Remove(offset, changes[0].AddedLength);
                    //控制光标位置，使其还能定位到变动前的位置
                    textBox.SelectionStart = offset;
                }
            }
        }

        /// <summary>
        /// 文本改变事件方法（用于正小数限制）
        /// </summary>
        private void TextChanged_For_PositiveDouble(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            TextChange[] changes = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(changes, 0);

            //引起Text改变的字符串的起点
            int offset = changes[0].Offset;
            //引起Text改变的字符串的长度
            if (changes[0].AddedLength > 0)
            {
                Regex regex = new Regex("^[0-9.]*$"); //数字、小数点;
                bool isMatch = regex.IsMatch(textBox.Text);
                bool isValue = double.TryParse(textBox.Text, out double doubleValue);

                if (!isMatch || !isValue)
                {
                    /*最新的Text中若含有非法字符，由于我们已经从键盘输入事件中屏蔽了非法字符，所以基本可以断定此非法输入是由于"粘贴"引起的。*/

                    //移除粘贴的内容;
                    textBox.Text = textBox.Text.Remove(offset, changes[0].AddedLength);
                    //控制光标位置，使其还能定位到变动前的位置
                    textBox.SelectionStart = offset;
                }
            }
        }
    }
}
