using System.Windows;

namespace SimpleDialogs.Controls
{
    public class ProgressDialog : BaseDialog
    {
        public static DependencyProperty TitleWhenFinishedProperty = DependencyProperty.Register(nameof(TitleWhenFinished), typeof(string), typeof(ProgressDialog), new PropertyMetadata("Done!"));

        public string TitleWhenFinished
        {
            get => (string)GetValue(TitleWhenFinishedProperty);
            set => SetValue(TitleWhenFinishedProperty, value);
        }

        public static DependencyProperty CancelButtonTextProperty = DependencyProperty.Register(nameof(CancelButtonText), typeof(string), typeof(ProgressDialog), new PropertyMetadata("CANCEL"));

        public string CancelButtonText
        {
            get => (string)GetValue(CancelButtonTextProperty);
            set => SetValue(CancelButtonTextProperty, value);
        }

        public static DependencyProperty CanCancelProperty = DependencyProperty.Register(nameof(CanCancel), typeof(bool), typeof(ProgressDialog));

        public bool CanCancel
        {
            get => (bool)GetValue(CanCancelProperty);
            set => SetValue(CanCancelProperty, value);
        }

        public static DependencyProperty IsUndefinedProperty = DependencyProperty.Register(nameof(IsUndefined), typeof(bool), typeof(ProgressDialog));

        public bool IsUndefined
        {
            get => (bool)GetValue(IsUndefinedProperty);
            set => SetValue(IsUndefinedProperty, value);
        }

        public static DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress), typeof(int), typeof(ProgressDialog), new PropertyMetadata(), ValidateProgress);

        private static bool ValidateProgress(object value)
        {
            double d;

            if(value != null && double.TryParse(value.ToString(), out d))
            {
                return d >= 0 && d <= 100;
            }

            return false;
        }

        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }
    }
}
