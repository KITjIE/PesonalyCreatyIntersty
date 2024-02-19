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
        public double InputWidth { get; set; } = 100;

        public double InputHeight { get; set; } = 30;

        public string InputNo { get; set; } = "A001";
    }
}
