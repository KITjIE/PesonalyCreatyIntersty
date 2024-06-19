using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;
using WPFTemplateLib.UserControls;

namespace WPFTemplate.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class TestInputViewModel : CustomInfoViewModel
    {
        public string InputWidth { get; set; } = string.Empty;

        public string InputHeight { get; set; } = string.Empty;

        public string InputNo { get; set; } = string.Empty;
    }
}
