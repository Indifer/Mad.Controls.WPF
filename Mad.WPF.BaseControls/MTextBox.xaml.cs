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
    /// MTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class MTextBox : TextBox
    {

        #region 按钮属性设置

        /// <summary>
        /// 默认提示内容
        /// </summary>
        [Bindable(true)]
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// 圆角度数
        /// </summary>
        [Bindable(true)]
        [TypeConverter(typeof(LengthConverter))]
        public double Radius
        {
            get { return (double)GetValue(RadiusProperty); }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        [Bindable(true)]
        public KeyboardType Keyboard
        {
            get { return (KeyboardType)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        ///// <summary>
        ///// 未获取焦点颜色
        ///// </summary>
        //[Bindable(true)]
        //public Brush BorderBrush
        //{
        //    get { return (Brush)GetValue(BorderBrushProperty); }
        //    set { SetValue(BorderBrushProperty, value); }
        //}

        /// <summary>
        /// 获取焦点颜色
        /// </summary>
        [Bindable(true)]
        public Brush FocusedBorderBrush
        {
            get { return (Brush)GetValue(FocusedBorderBrushProperty); }
            set { SetValue(FocusedBorderBrushProperty, value); }
        }



        #endregion

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(MTextBox));

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.RegisterAttached("Radius", typeof(double), typeof(MTextBox));


        public static readonly DependencyProperty KeyboardProperty =
            DependencyProperty.RegisterAttached("Keyboard", typeof(KeyboardType), typeof(MTextBox));

        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.RegisterAttached("FocusedBorderBrush", typeof(Brush), typeof(MTextBox));

        public MTextBox()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            switch (Keyboard)
            {
                case KeyboardType.Numeric:
                    this.KeyDown += RadiusTextBox_KeyDown;
                    this.TextChanged += RadiusTextBox_TextChanged;
                    break;
            }
        }

        void RadiusTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;
            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {
                double num = 0;
                if (!Double.TryParse(textBox.Text, out num))
                {
                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);
                }
            }
        }

        void RadiusTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = sender as TextBox;

            //屏蔽非法按键
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || e.Key == Key.Decimal || e.Key.ToString() == "Tab")
            {
                if (txt.Text.Contains(".") && e.Key == Key.Decimal)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else if (((e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.OemPeriod) && e.KeyboardDevice.Modifiers != ModifierKeys.Shift)
            {
                if (txt.Text.Contains(".") && e.Key == Key.OemPeriod)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public enum KeyboardType
        {
            Default = 0,
            Numeric,
            //Telephone,
            //Email
        }
    }

    //文本框的水印附加属性
    public class TextBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(TextBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));



        public static int GetTextBoxLength(DependencyObject obj)
        {
            return (int)obj.GetValue(TextBoxLengthProperty);
        }

        public static void SetTextBoxLength(DependencyObject obj, int value)
        {
            obj.SetValue(TextBoxLengthProperty, value);
        }

        public static readonly DependencyProperty TextBoxLengthProperty =
            DependencyProperty.RegisterAttached("TextBoxLength", typeof(int), typeof(TextBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tx = d as TextBox;
            if (tx == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                tx.TextChanged += new TextChangedEventHandler(tx_TextChanged);
            }
            else
            {
                tx.TextChanged -= new TextChangedEventHandler(tx_TextChanged);
            }
        }

        static void tx_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tx = sender as TextBox;
            if (tx == null)
            {
                return;
            }
            SetTextBoxLength(tx, tx.Text.Length);
        }


    }
}
