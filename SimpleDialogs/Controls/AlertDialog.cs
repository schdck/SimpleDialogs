using SimpleDialogs.Commands;
using SimpleDialogs.Enumerators;
using System;
using System.Windows;
using System.Windows.Input;

namespace SimpleDialogs.Controls
{
    public class AlertDialog : BaseDialog
    {
        public static DependencyProperty AlertLevelProperty = DependencyProperty.Register(nameof(AlertLevel), typeof(AlertLevel), typeof(AlertDialog));

        public AlertLevel AlertLevel
        {
            get => (AlertLevel)GetValue(AlertLevelProperty);
            set => SetValue(AlertLevelProperty, value);
        }

        public static DependencyProperty ButtonsStyleProperty = DependencyProperty.Register(nameof(ButtonsStyle), typeof(DialogButtonStyle), typeof(AlertDialog));

        public DialogButtonStyle ButtonsStyle
        {
            get => (DialogButtonStyle)GetValue(ButtonsStyleProperty);
            set => SetValue(ButtonsStyleProperty, value);
        }

        public static DependencyProperty ShowCopyToClipboardButtonProperty = DependencyProperty.Register(nameof(ShowCopyToClipboardButton), typeof(bool), typeof(AlertDialog));

        public bool ShowCopyToClipboardButton
        {
            get => (bool)GetValue(ShowCopyToClipboardButtonProperty);
            set => SetValue(ShowCopyToClipboardButtonProperty, value);
        }

        public static DependencyProperty NoButtonContentProperty = DependencyProperty.Register(nameof(NoButtonContent), typeof(object), typeof(AlertDialog), new PropertyMetadata("NO"));

        public object NoButtonContent
        {
            get => GetValue(NoButtonContentProperty);
            set => SetValue(NoButtonContentProperty, value);
        }

        public static DependencyProperty YesButtonContentProperty = DependencyProperty.Register(nameof(YesButtonContent), typeof(object), typeof(AlertDialog), new PropertyMetadata("YES"));

        public object YesButtonContent
        {
            get => GetValue(YesButtonContentProperty);
            set => SetValue(YesButtonContentProperty, value);
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
                Clipboard.SetText(string.Format("Title: {0}\r\nMessage: {1}\r\nException: {2}", Title, Message, Exception == null ? "null" : Exception.ToString()));
            });
        }
    }
}
