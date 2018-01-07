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

        public static DependencyProperty NoButtonTextProperty = DependencyProperty.Register(nameof(NoButtonText), typeof(string), typeof(AlertDialog), new PropertyMetadata("NO"));

        public string NoButtonText
        {
            get => (string)GetValue(NoButtonTextProperty);
            set => SetValue(NoButtonTextProperty, value);
        }

        public static DependencyProperty YesButtonTextProperty = DependencyProperty.Register(nameof(YesButtonText), typeof(string), typeof(AlertDialog), new PropertyMetadata("YES"));

        public string YesButtonText
        {
            get => (string)GetValue(YesButtonTextProperty);
            set => SetValue(YesButtonTextProperty, value);
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
