using System;
using System.Collections.Generic;
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

using Mad.WPF.Helpers;

namespace Mad.WPF.ToolControls
{
    /// <summary>
    /// NumericKeypad.xaml 的交互逻辑
    /// </summary>
    public partial class NumericKeypad : UserControl
    {
        private UIElement inputElement;
        private int? maxInput;

        public Action CloseAction { get; set; }

        public NumericKeypad()
        {
            InitializeComponent();
            ButtonHandlerBind();

            this.Loaded += NumberPanelControl_Loaded;
        }

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NumberPanelControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.btn_numpad_1.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_2.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_3.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_4.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_5.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_6.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_7.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_8.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_9.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_0.Cursor = System.Windows.Input.Cursors.Hand;

            this.btn_numpad_c.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_back.Cursor = System.Windows.Input.Cursors.Hand;

            this.btn_numpad_point.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_comma.Cursor = System.Windows.Input.Cursors.Hand;
            this.btn_numpad_close.Cursor = System.Windows.Input.Cursors.Hand;

            this.btn_numpad_1.Focusable = false;
            this.btn_numpad_2.Focusable = false;
            this.btn_numpad_3.Focusable = false;
            this.btn_numpad_4.Focusable = false;
            this.btn_numpad_5.Focusable = false;
            this.btn_numpad_6.Focusable = false;
            this.btn_numpad_7.Focusable = false;
            this.btn_numpad_8.Focusable = false;
            this.btn_numpad_9.Focusable = false;
            this.btn_numpad_0.Focusable = false;

            this.btn_numpad_back.Focusable = false;
            this.btn_numpad_c.Focusable = false;

            this.btn_numpad_point.Focusable = false;
            this.btn_numpad_comma.Focusable = false;
            this.btn_numpad_close.Focusable = false;
        }

        /// <summary>
        /// 设置输入element对象
        /// </summary>
        /// <param name="ele"></param>
        public void SetInputElement(UIElement ele, int? maxInput = null)
        {
            this.inputElement = ele;
            this.maxInput = maxInput;
        }

        private void ButtonHandlerBind()
        {
            this.btn_numpad_1.Click += btnNumberClick;
            this.btn_numpad_2.Click += btnNumberClick;
            this.btn_numpad_3.Click += btnNumberClick;
            this.btn_numpad_4.Click += btnNumberClick;
            this.btn_numpad_5.Click += btnNumberClick;
            this.btn_numpad_6.Click += btnNumberClick;
            this.btn_numpad_7.Click += btnNumberClick;
            this.btn_numpad_8.Click += btnNumberClick;
            this.btn_numpad_9.Click += btnNumberClick;
            this.btn_numpad_0.Click += btnNumberClick;

            this.btn_numpad_c.Click += btn_numpad_c_Click;
            this.btn_numpad_back.Click += btn_numpad_back_Click;

            this.btn_numpad_point.Click += btnNumberClick;
            this.btn_numpad_comma.Click += btnNumberClick;
            this.btn_numpad_close.Click += btn_numpad_close_Click;
        }

        void btn_numpad_close_Click(object sender, RoutedEventArgs e)
        {
            if (CloseAction != null)
            {
                CloseAction();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNumberClick(object sender, RoutedEventArgs e)
        {
            if (inputElement != null)
            {
                Button button = sender as Button;

                if (inputElement is TextBox)
                {
                    (inputElement as TextBox).Input(button.Content.ToString(), maxInput);
                }
            }
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_numpad_c_Click(object sender, RoutedEventArgs e)
        {
            if (inputElement is TextBox)
            {
                (inputElement as TextBox).Text = "";
            }
        }

        /// <summary>
        /// 退格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>duanlian
        private void btn_numpad_back_Click(object sender, RoutedEventArgs e)
        {
            if (inputElement is TextBox)
            {
                (inputElement as TextBox).Backspace();
            }
        }
    }
}
