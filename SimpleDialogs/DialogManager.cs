using SimpleDialogs.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleDialogs
{
    public static class DialogManager
    {
        public static readonly DependencyProperty DefaultDialogContainerProperty = DependencyProperty.RegisterAttached(
            nameof(DefaultDialogContainer),
            typeof(FrameworkElement),
            typeof(DialogManager),
            new PropertyMetadata(default(FrameworkElement), RegisterPropertyChangedCallback));

        private static void RegisterPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var newValue = dependencyPropertyChangedEventArgs.NewValue as FrameworkElement;

            if (newValue != null)
            {
                newValue.Loaded += (s, e) =>
                {
                    FrameworkElement container = null;

                    if (newValue is Panel)
                    {
                        container = new ContentControl();

                        (DefaultDialogContainer as Panel).Children.Add(container);
                    }
                    else if (newValue is ContentControl)
                    {
                        container = new ContentControl();

                        var contentControl = newValue as ContentControl;
                        var content = contentControl.Content as UIElement;

                        Grid grid = new Grid();

                        contentControl.Content = grid;

                        if (content != null)
                        {
                            grid.Children.Add(content);
                        }
                        grid.Children.Add(container);
                    }
                    else
                    {
                        throw new Exception("The DefaultDialogContainer is not of a supported type, make sure it descends either from ContentControl or from Panel.");
                    }

                    DefaultDialogContainer = container;
                };
            }
        }

        public static void SetDefaultDialogContainer(DependencyObject element, object context)
        {
            element.SetValue(DefaultDialogContainerProperty, context);
        }

        public static object GetDefaultDialogContainer(DependencyObject element)
        {
            return element.GetValue(DefaultDialogContainerProperty);
        }

        public static FrameworkElement DefaultDialogContainer { get; set; }

        public static void ShowDialog(BaseDialog dialog)
        {
            var container = DefaultDialogContainer as ContentControl;

            if(container == null)
            {
                throw new ArgumentNullException("The DefaultDialogContainer is not initialized.");
            }

            container.Content = dialog;
        }

        public static void HideVisibleDialog()
        {
            var container = DefaultDialogContainer as ContentControl;

            if (container == null)
            {
                throw new ArgumentNullException("The DefaultDialogContainer is not initialized.");
            }

            container.Content = null;
        }
    }
}
