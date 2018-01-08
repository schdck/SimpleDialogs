using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleDialogs.Controls
{
    public abstract class BaseDialog : Control
    {
        public static DependencyProperty ExitDialogCommandProperty = DependencyProperty.Register(nameof(ExitDialogCommand), typeof(ICommand), typeof(BaseDialog));

        public ICommand ExitDialogCommand
        {
            get => (ICommand)GetValue(ExitDialogCommandProperty);
            set => SetValue(ExitDialogCommandProperty, value);
        }

        public static DependencyProperty OkButtonContentProperty = DependencyProperty.Register(nameof(OkButtonContent), typeof(object), typeof(BaseDialog), new PropertyMetadata("OK"));

        public object OkButtonContent
        {
            get => GetValue(OkButtonContentProperty);
            set => SetValue(OkButtonContentProperty, value);
        }

        public static DependencyProperty ShowOverlayProperty = DependencyProperty.Register(nameof(ShowOverlay), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));

        public bool ShowOverlay
        {
            get => (bool)GetValue(ShowOverlayProperty);
            set => SetValue(ShowOverlayProperty, value);
        }

        public static DependencyProperty TitleForegroundProperty = DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(BaseDialog), new PropertyMetadata(Brushes.Black));

        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BaseDialog));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(BaseDialog));
        
        public string Message
        {
            get => (string) GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static DependencyProperty DialogWidthProperty = DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(BaseDialog), new PropertyMetadata(600d));

        public double DialogWidth
        {
            get => (double)GetValue(DialogWidthProperty);
            set => SetValue(DialogWidthProperty, value);
        }

        public static DependencyProperty DialogHeightProperty = DependencyProperty.Register(nameof(DialogHeight), typeof(double), typeof(BaseDialog), new PropertyMetadata(200d));

        public double DialogHeight
        {
            get => (double)GetValue(DialogHeightProperty);
            set => SetValue(DialogHeightProperty, value);
        }
    }
}
