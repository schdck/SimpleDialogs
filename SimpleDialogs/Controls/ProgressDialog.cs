using System.Windows;

namespace SimpleDialogs.Controls
{
    public partial class ProgressDialog : BaseDialog
    {
        public static readonly DependencyProperty IsUndefinedProperty = DependencyProperty.Register(nameof(IsUndefined), typeof(bool), typeof(ProgressDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress), typeof(int), typeof(ProgressDialog), new PropertyMetadata(), ValidateProgress);
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(ProgressDialog));

        /// <summary>
        /// Gets or sets a value indicating whether the progress is undefined
        /// </summary>
        public bool IsUndefined
        {
            get => (bool)GetValue(IsUndefinedProperty);
            set => SetValue(IsUndefinedProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog progress
        /// </summary>
        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog message
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public ProgressDialog()
        {
            InitializeComponent();
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