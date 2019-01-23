using SimpleDialogs.Enumerators;
using System;
using System.Windows;

namespace SimpleDialogs.Controls
{
    public partial class ProgressDialog : BaseDialog
    {
        public static readonly DependencyProperty IsUndefinedProperty = DependencyProperty.Register(nameof(IsUndefined), typeof(bool), typeof(ProgressDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty ProgressProperty = DependencyProperty.Register(nameof(Progress), typeof(int), typeof(ProgressDialog), new PropertyMetadata(), ValidateProgress);
        public static readonly DependencyProperty PrimaryButtonContentProperty = DependencyProperty.Register(nameof(PrimaryButtonContent), typeof(string), typeof(ProgressDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty ShowAuxiliaryButtonProperty = DependencyProperty.Register(nameof(ShowAuxiliaryButton), typeof(bool), typeof(ProgressDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty AuxiliaryButtonContentProperty = DependencyProperty.Register(nameof(AuxiliaryButtonContent), typeof(string), typeof(ProgressDialog), new PropertyMetadata("CANCEL"));
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof(MessageBox), typeof(string), typeof(ProgressDialog));

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

        public string PrimaryButtonContent
        {
            get => (string)GetValue(PrimaryButtonContentProperty);
            set => SetValue(PrimaryButtonContentProperty, value);
        }

        public bool ShowAuxiliaryButton
        {
            get => (bool)GetValue(ShowAuxiliaryButtonProperty);
            set => SetValue(ShowAuxiliaryButtonProperty, value);
        }

        public string AuxiliaryButtonContent
        {
            get => (string)GetValue(AuxiliaryButtonContentProperty);
            set => SetValue(AuxiliaryButtonContentProperty, value);
        }

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