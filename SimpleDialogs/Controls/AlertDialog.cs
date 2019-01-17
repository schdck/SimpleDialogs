using SimpleDialogs.Commands;
using SimpleDialogs.Enumerators;
using System;
using System.Windows;
using System.Windows.Input;

namespace SimpleDialogs.Controls
{
    public class AlertDialog : BaseDialog
    {
        public static DependencyProperty MessageSeverityProperty = DependencyProperty.Register(nameof(MessageSeverity), typeof(MessageSeverity), typeof(AlertDialog));

        public MessageSeverity MessageSeverity
        {
            get => (MessageSeverity)GetValue(MessageSeverityProperty);
            set => SetValue(MessageSeverityProperty, value);
        }

        public static DependencyProperty ShowCopyToClipboardButtonProperty = DependencyProperty.Register(nameof(ShowCopyToClipboardButton), typeof(bool), typeof(AlertDialog));

        public bool ShowCopyToClipboardButton
        {
            get => (bool)GetValue(ShowCopyToClipboardButtonProperty);
            set => SetValue(ShowCopyToClipboardButtonProperty, value);
        }

        public static DependencyProperty CopyToClipboardButtonContentProperty = DependencyProperty.Register(nameof(CopyToClipboardButtonContent), typeof(object), typeof(AlertDialog), new PropertyMetadata("Copy details to clipboard"));

        public object CopyToClipboardButtonContent
        {
            get => GetValue(CopyToClipboardButtonContentProperty);
            set => SetValue(CopyToClipboardButtonContentProperty, value);
        }

        public static DependencyProperty ExceptionProperty = DependencyProperty.Register(nameof(Exception), typeof(Exception), typeof(AlertDialog));

        public Exception Exception
        {
            get => (Exception)GetValue(ExceptionProperty);
            set => SetValue(ExceptionProperty, value);
        }

        public ICommand CopyToClipboardCommand { get; private set; }

        public AlertDialog()
        {
            CopyToClipboardCommand = new SimpleCommand(() =>
            {
                Clipboard.SetText(string.Format("Title: {0}\r\nMessage: {1}\r\nException: {2}", Title, Content, Exception == null ? "null" : Exception.ToString()));
            });
        }
    }
}
