using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mad.WPF.BaseControls
{
    /// <summary>
    /// MButton.xaml 的交互逻辑
    /// </summary>
    public partial class MButton : Button
    {
        #region 按钮属性设置

        /// <summary>
        /// x圆角度数
        /// </summary>
        [Bindable(true)]
        public double RadiusX
        {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }
        /// <summary>
        /// y圆角度数
        /// </summary>
        [Bindable(true)]
        public double RadiusY
        {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }

        /// <summary>
        /// 按钮按下颜色
        /// </summary>
        [Bindable(true)]
        public string PressedBrush
        {
            get { return (string)GetValue(PressedBrushProperty); }
            set { SetValue(PressedBrushProperty, value); }
        }
        /// <summary>
        /// 按钮失效颜色
        /// </summary>
        [Bindable(true)]
        public string DisenabledBrush
        {
            get { return (string)GetValue(DisenabledBrushProperty); }
            set { SetValue(DisenabledBrushProperty, value); }
        }

        #endregion

        public static readonly DependencyProperty RadiusXProperty =
            DependencyProperty.RegisterAttached("RadiusX", typeof(double), typeof(MButton));

        public static readonly DependencyProperty RadiusYProperty =
            DependencyProperty.RegisterAttached("RadiusY", typeof(double), typeof(MButton));

        public static readonly DependencyProperty PressedBrushProperty =
            DependencyProperty.RegisterAttached("PressedBrush", typeof(string), typeof(MButton));

        public static readonly DependencyProperty DisenabledBrushProperty =
            DependencyProperty.RegisterAttached("DisenabledBrush", typeof(string), typeof(MButton));
        
        public MButton()
        {
            InitializeComponent();

            DisenabledBrush = "#AAAAAA";
            FontSize = 20;
            RadiusX = 5;
            RadiusY = 5;
            Focusable = true; 
        }
    }
}
