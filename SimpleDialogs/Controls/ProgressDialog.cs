using System.Windows;

namespace SimpleDialogs.Controls
{
    public class ProgressDialog : BaseDialog
    {
        public static DependencyProperty IsUndefinedProperty =
            DependencyProperty.Register(nameof(IsUndefined), typeof(bool), typeof(ProgressDialog), new PropertyMetadata(false));

        public static DependencyProperty ProgressProperty =
            DependencyProperty.Register(nameof(Progress), typeof(int), typeof(ProgressDialog), new PropertyMetadata(), ValidateProgress);

        public bool IsUndefined
        {
            get => (bool)GetValue(IsUndefinedProperty);
            set => SetValue(IsUndefinedProperty, value);
        }

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        private static bool ValidateProgress(object value)
        {
            if (value != null && double.TryParse(value.ToString(), out double d))
            {
                return d >= 0 && d <= 100;
            }

            return false;
        }
    }
}
