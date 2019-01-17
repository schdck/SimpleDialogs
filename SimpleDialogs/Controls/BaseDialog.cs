using SimpleDialogs.Commands;
using SimpleDialogs.Enumerators;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleDialogs.Controls
{
    public abstract class BaseDialog : ContentControl
    {
        private static DependencyPropertyKey IsOpenPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(nameof(IsOpen), typeof(bool), typeof(BaseDialog), new PropertyMetadata(false));

        public static DependencyProperty AlternativeForegroundProperty =
            DependencyProperty.Register(nameof(AlternativeForeground), typeof(Brush), typeof(BaseDialog));

        public static DependencyProperty TitleAlignmentProperty =
            DependencyProperty.Register(nameof(TitleAlignment), typeof(TextAlignment), typeof(BaseDialog));

        public static DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(BaseDialog));

        public static DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(BaseDialog));

        public static DependencyProperty PrimaryButtonContentProperty = 
            DependencyProperty.Register(nameof(PrimaryButtonContent), typeof(string), typeof(BaseDialog), new PropertyMetadata("OK"));

        public static DependencyProperty AuxiliaryButtonContentProperty = 
            DependencyProperty.Register(nameof(AuxiliaryButtonContent), typeof(string), typeof(BaseDialog), new PropertyMetadata("CANCEL"));

        public static DependencyProperty CanCloseProperty =
            DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));

        public static DependencyProperty ShowAuxiliaryButtonProperty = 
            DependencyProperty.Register(nameof(ShowAuxiliaryButton), typeof(bool), typeof(BaseDialog), new PropertyMetadata(false));

        public static DependencyProperty ShowOverlayProperty = 
            DependencyProperty.Register(nameof(ShowOverlay), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));

        public static DependencyProperty IsOpenProperty =
            IsOpenPropertyKey.DependencyProperty;

        public static DependencyProperty DialogWidthProperty = 
            DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(BaseDialog), new PropertyMetadata(600d));

        public static DependencyProperty DialogHeightProperty = 
            DependencyProperty.Register(nameof(DialogHeight), typeof(double), typeof(BaseDialog), new PropertyMetadata(200d));

        public static DependencyProperty ButtonsProperty =
            DependencyProperty.Register(nameof(Buttons), typeof(UIElement), typeof(BaseDialog));

        public ICommand CloseDialogCommand { get; private set; }

        public Brush AlternativeForeground
        {
            get => (Brush)GetValue(AlternativeForegroundProperty);
            set => SetValue(AlternativeForegroundProperty, value);
        }

        public TextAlignment TitleAlignment
        {
            get => (TextAlignment)GetValue(TitleAlignmentProperty);
            set => SetValue(TitleAlignmentProperty, value);
        }

        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string PrimaryButtonContent
        {
            get => (string)GetValue(PrimaryButtonContentProperty);
            set => SetValue(PrimaryButtonContentProperty, value);
        }

        public string AuxiliaryButtonContent
        {
            get => (string)GetValue(AuxiliaryButtonContentProperty);
            set => SetValue(AuxiliaryButtonContentProperty, value);
        }

        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }

        public bool ShowAuxiliaryButton
        {
            get => (bool)GetValue(ShowAuxiliaryButtonProperty);
            set => SetValue(ShowAuxiliaryButtonProperty, value);
        }

        public bool ShowOverlay
        {
            get => (bool)GetValue(ShowOverlayProperty);
            set => SetValue(ShowOverlayProperty, value);
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            private set => SetValue(IsOpenPropertyKey, value);
        }

        public double DialogWidth
        {
            get => (double)GetValue(DialogWidthProperty);
            set => SetValue(DialogWidthProperty, value);
        }

        public double DialogHeight
        {
            get => (double)GetValue(DialogHeightProperty);
            set => SetValue(DialogHeightProperty, value);
        }

        public UIElement Buttons
        {
            get => (UIElement)GetValue(ButtonsProperty);
            set => SetValue(ButtonsProperty, value);
        }

        public event EventHandler<DialogClosedEventArgs> DialogClosed;

        public BaseDialog()
        {
            CloseDialogCommand = new SimpleCommand<DialogResult>(x =>
            {
                DialogManager.CloseDialog(this, x);
            });
        }
        
        internal void Show()
        {
            IsOpen = true;
        }

        internal void Close(DialogResult result)
        {
            IsOpen = false;

            DialogClosed?.Invoke(this, new DialogClosedEventArgs(result));
        }
    }
}
