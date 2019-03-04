using System.Windows;

namespace SimpleDialogs.Controls
{
    public partial class InputDialog : BaseDialog
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Description), typeof(string), typeof(InputDialog));
        public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(InputDialog));

        public string Description
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        public string Text { get; set; }

        public InputDialog()
        {
            InitializeComponent();
        }

        protected override object GetResult()
        {
            return Text;
        }
    }
}