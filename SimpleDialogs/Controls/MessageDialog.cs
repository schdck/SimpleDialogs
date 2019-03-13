using SimpleDialogs.Enumerators;
using System;
using System.Windows;

namespace SimpleDialogs.Controls
{
    public partial class MessageDialog : BaseDialog
    {
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(Message), typeof(string), typeof(MessageDialog));
        public static readonly DependencyProperty MessageSeverityProperty = DependencyProperty.Register(nameof(MessageSeverity), typeof(MessageSeverity), typeof(MessageDialog));
        public static readonly DependencyProperty ExceptionProperty = DependencyProperty.Register(nameof(Exception), typeof(Exception), typeof(MessageDialog));

        /// <summary>
        /// Gets or sets the dialog message
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog MessageSeverity
        /// </summary>
        public MessageSeverity MessageSeverity
        {
            get => (MessageSeverity)GetValue(MessageSeverityProperty);
            set => SetValue(MessageSeverityProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog Exception
        /// </summary>
        public Exception Exception
        {
            get => (Exception)GetValue(ExceptionProperty);
            set => SetValue(ExceptionProperty, value);
        }

        public MessageDialog()
        {
            InitializeComponent();
        }
    }
}