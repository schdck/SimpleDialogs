using SimpleDialogs.Enumerators;
using SimpleDialogs.Helpers;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleDialogs.Controls
{
    [TemplatePart(Name = "PART_FirstButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SecondButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ThirdButton", Type = typeof(Button))]
    public abstract class BaseDialog : ContentControl
    {
        private static readonly DependencyPropertyKey IsClosedPropertyKey = DependencyProperty.RegisterReadOnly(nameof(IsClosed), typeof(bool), typeof(BaseDialog), new PropertyMetadata(false));

        public static readonly DependencyProperty AlternativeForegroundProperty = DependencyProperty.Register(nameof(AlternativeForeground), typeof(Brush), typeof(BaseDialog));

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(nameof(TitleFontSize), typeof(double), typeof(BaseDialog));
        public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(nameof(TitleFontWeight), typeof(FontWeight), typeof(BaseDialog));
        public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(nameof(TitleFontFamily), typeof(FontFamily), typeof(BaseDialog));
        public static readonly DependencyProperty TitleAlignmentProperty = DependencyProperty.Register(nameof(TitleAlignment), typeof(TextAlignment), typeof(BaseDialog));
        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register(nameof(TitleForeground), typeof(Brush), typeof(BaseDialog));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(BaseDialog));

        public static readonly DependencyProperty SecondsToAutoCloseProperty = DependencyProperty.Register(nameof(SecondsToAutoClose), typeof(int), typeof(BaseDialog), new PropertyMetadata(-1, DependencyPropertyChanged));
        public static readonly DependencyProperty ShowOverlayProperty = DependencyProperty.Register(nameof(ShowOverlay), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));
        public static readonly DependencyProperty IsClosedProperty = IsClosedPropertyKey.DependencyProperty;
        public static readonly DependencyProperty CanCloseProperty = DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(BaseDialog), new PropertyMetadata(true));

        public static readonly DependencyProperty DialogWidthProperty = DependencyProperty.Register(nameof(DialogWidth), typeof(double), typeof(BaseDialog), new PropertyMetadata(double.NaN));
        public static readonly DependencyProperty DialogHeightProperty = DependencyProperty.Register(nameof(DialogHeight), typeof(double), typeof(BaseDialog), new PropertyMetadata(double.NaN));
        public static readonly DependencyProperty TitleBarHeightProperty = DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(BaseDialog));

        public static readonly DependencyProperty ShowFirstButtonProperty = DependencyProperty.Register(nameof(ShowFirstButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(true));
        public static readonly DependencyProperty ShowSecondButtonProperty = DependencyProperty.Register(nameof(ShowSecondButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowThirdButtonProperty = DependencyProperty.Register(nameof(ShowThirdButton), typeof(bool), typeof(MessageDialog), new PropertyMetadata(false));
        public static readonly DependencyProperty FirstButtonContentProperty = DependencyProperty.Register(nameof(FirstButtonContent), typeof(string), typeof(MessageDialog), new PropertyMetadata("OK"));
        public static readonly DependencyProperty SecondButtonContentProperty = DependencyProperty.Register(nameof(SecondButtonContent), typeof(string), typeof(MessageDialog), new PropertyMetadata("CANCEL"));
        public static readonly DependencyProperty ThirdButtonContentProperty = DependencyProperty.Register(nameof(ThirdButtonContent), typeof(string), typeof(MessageDialog), new PropertyMetadata(null));
        public static readonly DependencyProperty AutoFocusedButtonProperty = DependencyProperty.Register(nameof(AutoFocusedButton), typeof(DialogButton), typeof(MessageDialog), new PropertyMetadata(DialogButton.None, DependencyPropertyChanged));

        private bool _InitializedButtons;
        private Timer _AutoCloseTimer;
        private Button _FirstButton;
        private Button _SecondButton;
        private Button _ThirdButton;

        internal DialogContainer Container { get; set; }

        public Brush AlternativeForeground
        {
            get => (Brush)GetValue(AlternativeForegroundProperty);
            set => SetValue(AlternativeForegroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the title FontSize
        /// </summary>
        public double TitleFontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the title FontWeight
        /// </summary>
        public FontWeight TitleFontWeight
        {
            get => (FontWeight)GetValue(TitleFontWeightProperty);
            set => SetValue(TitleFontWeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the title FontFamily
        /// </summary>
        public FontFamily TitleFontFamily
        {
            get => (FontFamily)GetValue(TitleFontFamilyProperty);
            set => SetValue(TitleFontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the title TextAlignment
        /// </summary>
        public TextAlignment TitleAlignment
        {
            get => (TextAlignment)GetValue(TitleAlignmentProperty);
            set => SetValue(TitleAlignmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the title Foreground
        /// </summary>
        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog title
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        /// <summary>
        /// Gets or sets the seconds remaining until the dialog auto closes
        /// </summary>
        /// <value>
        /// The seconds until the dialog auto close or zero to disable auto close feature
        /// </value>
        public int SecondsToAutoClose
        {
            get => (int)GetValue(SecondsToAutoCloseProperty);
            set => SetValue(SecondsToAutoCloseProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the dialog should be shown with an overlay 
        /// </summary>
        public bool ShowOverlay
        {
            get => (bool)GetValue(ShowOverlayProperty);
            set => SetValue(ShowOverlayProperty, value);
        }

        /// <summary>
        /// Gets a value indicating if the dialog has been closed
        /// </summary>
        public bool IsClosed
        {
            get => (bool)GetValue(IsClosedProperty);
            private set => SetValue(IsClosedPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets a value indicating if the dialog can be closed
        /// </summary>
        public bool CanClose
        {
            get => (bool)GetValue(CanCloseProperty);
            set => SetValue(CanCloseProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog width
        /// </summary>
        public double DialogWidth
        {
            get => (double)GetValue(DialogWidthProperty);
            set => SetValue(DialogWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the dialog height
        /// </summary>
        public double DialogHeight
        {
            get => (double)GetValue(DialogHeightProperty);
            set => SetValue(DialogHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the title bar height
        /// </summary>
        public double TitleBarHeight
        {
            get => (double)GetValue(TitleBarHeightProperty);
            set => SetValue(TitleBarHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the first button of the dialog
        /// </summary>
        public bool ShowFirstButton
        {
            get => (bool)GetValue(ShowFirstButtonProperty);
            set => SetValue(ShowFirstButtonProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the second button of the dialog
        /// </summary>
        public bool ShowSecondButton
        {
            get => (bool)GetValue(ShowSecondButtonProperty);
            set => SetValue(ShowSecondButtonProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the third button of the dialog
        /// </summary>
        public bool ShowThirdButton
        {
            get => (bool)GetValue(ShowThirdButtonProperty);
            set => SetValue(ShowThirdButtonProperty, value);
        }

        /// <summary>
        /// Gets or sets the first button content
        /// </summary>
        public string FirstButtonContent
        {
            get => (string)GetValue(FirstButtonContentProperty);
            set => SetValue(FirstButtonContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the second button content
        /// </summary>
        public string SecondButtonContent
        {
            get => (string)GetValue(SecondButtonContentProperty);
            set => SetValue(SecondButtonContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the content of the third dialog button
        /// </summary>
        public string ThirdButtonContent
        {
            get => (string)GetValue(ThirdButtonContentProperty);
            set => SetValue(ThirdButtonContentProperty, value);
        }

        /// <summary>
        /// Gets or sets the button that should be automatically focused when the dialog is open
        /// </summary>
        public DialogButton AutoFocusedButton
        {
            get => (DialogButton)GetValue(AutoFocusedButtonProperty);
            set => SetValue(AutoFocusedButtonProperty, value);
        }

        public event EventHandler<DialogButtonClickedEventArgs> ButtonClicked;
        public event EventHandler<DialogClosedEventArgs> Closed;

        public BaseDialog()
        {
            _AutoCloseTimer = new Timer(1000)
            {
                AutoReset = false
            };

            _AutoCloseTimer.Elapsed += (s, e) => Dispatcher.Invoke(() =>
            {
                SecondsToAutoClose--;
            });

            Loaded += HandleLoadedEvent;
        }

        protected virtual object GetResult()
        {
            return null;
        }

        protected void CloseDialog(DialogButton clickedButton)
        {
            SecondsToAutoClose = -1;
            IsClosed = true;

            Closed?.Invoke(this, new DialogClosedEventArgs(clickedButton, GetResult()));
        }

        protected internal bool StartToCloseDialog(DialogButton clickedButton)
        {
            var closingEventArgs = new DialogButtonClickedEventArgs(clickedButton);

            ButtonClicked?.Invoke(this, closingEventArgs);

            if (closingEventArgs.CloseDialogAfterHandle)
            {
                CloseDialog(clickedButton);

                return true;
            }
            
            return false;
        }

        internal Task WaitForLoadAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            RoutedEventHandler loadHandler = null;

            loadHandler = (sender, args) =>
            {
                Loaded -= loadHandler;

                taskCompletionSource.TrySetResult(null);
            };

            Loaded += loadHandler;

            return taskCompletionSource.Task;
        }

        internal Task<DialogClosedEventArgs> WaitForCloseAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<DialogClosedEventArgs>();

            EventHandler<DialogClosedEventArgs> closeHandler = null;

            closeHandler = (sender, args) =>
            {
                Closed -= closeHandler;

                taskCompletionSource.TrySetResult(args);
            };

            Closed += closeHandler;

            return taskCompletionSource.Task;
        }

        private void HandleLoadedEvent(object sender, EventArgs eargs)
        {
            if(_InitializedButtons == false)
            {
                _InitializedButtons = true;

                _FirstButton = GetTemplateChild("PART_FirstButton") as Button;
                _SecondButton = GetTemplateChild("PART_SecondButton") as Button;
                _ThirdButton = GetTemplateChild("PART_ThirdButton") as Button;

                _FirstButton.Click += (s, e) =>
                {
                    if (CanClose)
                    {
                        DialogManager.CloseDialogAsync(this, DialogButton.FirstButton);
                    }
                };

                _SecondButton.Click += (s, e) =>
                {
                    if (CanClose)
                    {
                        DialogManager.CloseDialogAsync(this, DialogButton.SecondButton);
                    }
                };

                _ThirdButton.Click += (s, e) =>
                {
                    if (CanClose)
                    {
                        DialogManager.CloseDialogAsync(this, DialogButton.ThirdButton);
                    }
                };
            }

            switch (AutoFocusedButton)
            {
                case DialogButton.FirstButton:
                    KeyboardHelper.Focus(_FirstButton);
                    break;

                case DialogButton.SecondButton:
                    KeyboardHelper.Focus(_SecondButton);
                    break;

                case DialogButton.ThirdButton:
                    KeyboardHelper.Focus(_ThirdButton);
                    break;
            }

            CheckForSecondsToAutoClose();
        }

        private void CheckForSecondsToAutoClose()
        {
            if (SecondsToAutoClose == 0)
            {
                var result = 
                    AutoFocusedButton == DialogButton.None ? 
                        DialogButton.FirstButton : AutoFocusedButton;

                DialogManager.CloseDialogAsync(this, result);
            }
            else if (SecondsToAutoClose > 0)
            {
                Button defaultButton = null;
                object defaultButtonContent = null;

                switch (AutoFocusedButton)
                {
                    case DialogButton.None:
                    case DialogButton.FirstButton:
                        defaultButton = _FirstButton;
                        defaultButtonContent = FirstButtonContent;
                        break;

                    case DialogButton.SecondButton:
                        defaultButton = _SecondButton;
                        defaultButtonContent = SecondButtonContent;
                        break;

                    case DialogButton.ThirdButton:
                        defaultButton = _ThirdButton;
                        defaultButtonContent = ThirdButtonContent;
                        break;
                }

                if (defaultButton != null)
                {
                    defaultButton.Content = $"{defaultButtonContent} ({SecondsToAutoClose})";
                }

                _AutoCloseTimer.Start();
            }
        }

        private static void DependencyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if(sender is BaseDialog dialog)
            {
                if(e.Property == AutoFocusedButtonProperty && dialog.IsLoaded)
                {
                    // Reset the buttons content in case we changed it with the close countdown
                    dialog._FirstButton.Content = dialog.FirstButtonContent;
                    dialog._SecondButton.Content = dialog.SecondButtonContent;
                    dialog._ThirdButton.Content = dialog.ThirdButtonContent;
                }
                else if(e.Property == SecondsToAutoCloseProperty)
                {
                    dialog.CheckForSecondsToAutoClose();
                }
            }
        }
    }
}