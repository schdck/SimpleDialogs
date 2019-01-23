using SimpleDialogs.Commands;
using SimpleDialogs.Enumerators;
using SimpleDialogs.Helpers;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleDialogs.Controls
{
    [TemplatePart(Name = "PART_FirstButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SecondButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ThirdButton", Type = typeof(Button))]
    public abstract class BaseDialog : ContentControl
    {
        public static readonly DependencyProperty AlternativeForegroundProperty = DependencyProperty.Register(nameof(AlternativeForeground), typeof(Brush), typeof(BaseDialog));

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(nameof(TitleFontSize), typeof(double), typeof(BaseDialog));
        public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(nameof(TitleFontWeight), typeof(FontWeight), typeof(BaseDialog));
        public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(nameof(TitleFontFamily), typeof(FontFamily), typeof(BaseDialog));
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register(nameof(TitleAlignment), typeof(TextAlignment), typeof(BaseDialog));
        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(BaseDialog));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BaseDialog));

        public static readonly DependencyProperty ShowOverlayProperty = DependencyProperty.Register(nameof(ShowOverlay), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));
        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));
        public static readonly DependencyProperty CanCloseProperty = DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));

        public static readonly DependencyProperty DialogWidthProperty = DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(BaseDialog), new PropertyMetadata(double.NaN));
        public static readonly DependencyProperty DialogHeightProperty = DependencyProperty.Register(nameof(DialogHeight), typeof(double), typeof(BaseDialog), new PropertyMetadata(double.NaN));
        public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(BaseDialog));

        public static readonly DependencyProperty ShowFirstButtonProperty = DependencyProperty.Register(nameof(ShowFirstButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowSecondButtonProperty = DependencyProperty.Register(nameof(ShowSecondButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowThirdButtonProperty = DependencyProperty.Register(nameof(ShowThirdButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty FirstButtonContentProperty = DependencyProperty.Register(nameof(FirstButtonContent), typeof(string), typeof(MessageDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty SecondButtonContentProperty = DependencyProperty.Register(nameof(SecondButtonContent), typeof(string), typeof(MessageDialog), new PropertyMetadata("CANCEL"));
        public static readonly DependencyProperty ThirdButtonContentProperty = DependencyProperty.Register(nameof(ThirdButtonContent), typeof(string), typeof(MessageDialog));
        public static readonly DependencyProperty AutoFocusedButtonProperty = DependencyProperty.Register(nameof(AutoFocusedButton), typeof(DialogButton), typeof(MessageDialog), new PropertyMetadata(DialogButton.None));

        public Brush AlternativeForeground
        {
            get => (Brush)GetValue(AlternativeForegroundProperty);
            set => SetValue(AlternativeForegroundProperty, value);
        }

        public double TitleFontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public FontWeight TitleFontWeight
        {
            get => (FontWeight)GetValue(TitleFontWeightProperty);
            set => SetValue(TitleFontWeightProperty, value);
        }

        public FontFamily TitleFontFamily
        {
            get => (FontFamily)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
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

        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }

        public bool ShowOverlay
        {
            get => (bool)GetValue(ShowOverlayProperty);
            set => SetValue(ShowOverlayProperty, value);
        }

        public bool IsOpen
        {
            get => (bool)GetValue(IsOpenProperty);
            set => SetValue(IsOpenProperty, value);
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

        public double TitleBarHeight
        {
            get => (double)GetValue(TitleBarHeightProperty);
            set => SetValue(TitleBarHeightProperty, value);
        }

        public bool ShowFirstButton
        {
            get => (bool)GetValue(ShowFirstButtonProperty);
            set => SetValue(ShowFirstButtonProperty, value);
        }

        public bool ShowSecondButton
        {
            get => (bool)GetValue(ShowSecondButtonProperty);
            set => SetValue(ShowSecondButtonProperty, value);
        }

        public bool ShowThirdButton
        {
            get => (bool)GetValue(ShowThirdButtonProperty);
            set => SetValue(ShowThirdButtonProperty, value);
        }

        public string FirstButtonContent
        {
            get => (string)GetValue(FirstButtonContentProperty);
            set => SetValue(FirstButtonContentProperty, value);
        }

        public string SecondButtonContent
        {
            get => (string)GetValue(SecondButtonContentProperty);
            set => SetValue(SecondButtonContentProperty, value);
        }

        public string ThirdButtonContent
        {
            get => (string)GetValue(ThirdButtonContentProperty);
            set => SetValue(ThirdButtonContentProperty, value);
        }

        public DialogButton AutoFocusedButton
        {
            get => (DialogButton)GetValue(AutoFocusedButtonProperty);
            set => SetValue(AutoFocusedButtonProperty, value);
        }

        public event EventHandler<DialogClosingEventArgs> Closing;
        public event EventHandler<DialogClosedEventArgs> Closed;

        public BaseDialog()
        {
            Loaded += HandleInitializedEvent;
        }

        public void Close()
        {
            CloseDialogWithResult(DialogButton.None);
        }

        internal void CloseDialogWithResult(DialogButton result)
        {
            var closingEventArgs = new DialogClosingEventArgs(result);

            Closing?.Invoke(this, closingEventArgs);

            if(!closingEventArgs.Cancel)
            {
                DialogManager.CloseDialog(this, result);

                Closed?.Invoke(this, new DialogClosedEventArgs(result));
            }
        }

        private void HandleInitializedEvent(object sender, EventArgs eargs)
        {
            Button first = GetTemplateChild("PART_FirstButton") as Button,
                   second = GetTemplateChild("PART_SecondButton") as Button,
                   third = GetTemplateChild("PART_ThirdButton") as Button;

            first.Click += (s, e) =>
            {
                if (CanClose)
                {
                    CloseDialogWithResult(DialogButton.FirstButton);
                }
            };

            second.Click += (s, e) =>
            {
                if (CanClose)
                {
                    CloseDialogWithResult(DialogButton.SecondButton);
                }
            };

            third.Click += (s, e) =>
            {
                if(CanClose)
                {
                    CloseDialogWithResult(DialogButton.ThirdButton);
                }
            };

            switch (AutoFocusedButton)
            {
                case DialogButton.FirstButton:
                    KeyboardHelper.Focus(first);
                    break;

                case DialogButton.SecondButton:
                    KeyboardHelper.Focus(second);
                    break;

                case DialogButton.ThirdButton:
                    KeyboardHelper.Focus(third);
                    break;
            }
        }
    }
}