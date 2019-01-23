using SimpleDialogs.Enumerators;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimpleDialogs.TemplateSelectors
{
    internal class MessageDialogIconSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element == null || item == null)
            {
                return null;
            }
            if (item is MessageSeverity severity)
            {
                switch (severity)
                {
                    case MessageSeverity.Information:
                        return element.FindResource("IconInformation") as DataTemplate;

                    case MessageSeverity.Success:
                        return element.FindResource("IconSuccess") as DataTemplate;

                    case MessageSeverity.Warning:
                        return element.FindResource("IconWarning") as DataTemplate;

                    case MessageSeverity.Error:
                    case MessageSeverity.Critical:
                        return element.FindResource("IconError") as DataTemplate;
                }
            }
            
            throw new ApplicationException();
        }
    }
}
