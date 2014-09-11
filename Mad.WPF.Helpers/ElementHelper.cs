using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Mad.WPF.Helpers
{
    public static class ElementHelper
    {
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }

        public static List<T> GetChildObjects<T>(DependencyObject obj, Type typename) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType() == typename))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, typename));
            }
            return childList;
        }

        public static List<T> GetChildObjects<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).GetType().Name == name | string.IsNullOrEmpty(name)))
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child, name));
            }
            return childList;
        }

        public static T GetParent<T>(DependencyObject obj, string name) where T : FrameworkElement
        {

            var parent = VisualTreeHelper.GetParent(obj);
            if (parent is T && (((T)parent).GetType().Name == name | string.IsNullOrEmpty(name)))
            {
                return (T)parent;
            }
            else
            {
                var _parent = GetParent<T>(parent, name);
                if (_parent != null)
                    return _parent;
                return null;
            }
        }

        /// <summary>
        /// textbox退格
        /// </summary>
        /// <param name="textBox"></param>
        public static void Backspace(this System.Windows.Controls.TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                int selectionStart = textBox.SelectionStart;
                if (textBox.SelectionLength > 0)
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.SelectionStart)
                        + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength);

                    textBox.SelectionStart = selectionStart;
                    textBox.SelectionLength = 0;
                }
                else if (textBox.SelectionStart > 0)
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.SelectionStart - 1)
                        + textBox.Text.Substring(textBox.SelectionStart - 1 + textBox.SelectionLength + 1);

                    textBox.SelectionStart = selectionStart - 1;
                    textBox.SelectionLength = 0;
                }

            }
        }


        public static void Input(this System.Windows.Controls.TextBox textBox, string text, int? maxInput = null)
        {
            int selectionStart = textBox.SelectionStart;
            string tempText;
            if (textBox.SelectionLength > 0)
            {
                tempText = textBox.Text.Substring(0, textBox.SelectionStart) + text
                    + textBox.Text.Substring(textBox.SelectionStart + textBox.SelectionLength);
                if (maxInput.HasValue && tempText.Length > maxInput.Value)
                {
                    return;
                }

                textBox.Text = tempText;
                textBox.SelectionStart = selectionStart + textBox.SelectionLength + 1;
                textBox.SelectionLength = 0;
            }
            else
            {
                tempText = textBox.Text.Substring(0, textBox.SelectionStart) + text
                        + textBox.Text.Substring(textBox.SelectionStart);
                if (maxInput.HasValue && tempText.Length > maxInput.Value)
                {
                    return;
                }

                textBox.Text = tempText;
                textBox.SelectionStart = selectionStart + text.Length;
                textBox.SelectionLength = 0;
            }
        }
    }
}
