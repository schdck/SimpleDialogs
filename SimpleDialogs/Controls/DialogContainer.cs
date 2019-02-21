using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SimpleDialogs.Controls
{
    public class DialogContainer : Control
    {
        private static readonly DependencyPropertyKey CurrentDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CurrentDialog), typeof(BaseDialog), typeof(DialogContainer), new PropertyMetadata());

        public static readonly DependencyProperty CurrentDialogProperty =
            CurrentDialogPropertyKey.DependencyProperty;

        public static readonly DependencyProperty DisplayDialogsFromTypeProperty =
            DependencyProperty.Register(nameof(DisplayDialogsFromType), typeof(Type), typeof(DialogContainer), new PropertyMetadata(typeof(object), new PropertyChangedCallback(DisplayDialogsFromTypeChanged)));

        private List<BaseDialog> _DisplayedDialogs;

        public BaseDialog CurrentDialog
        {
            get => (BaseDialog)GetValue(CurrentDialogProperty);
            private set => SetValue(CurrentDialogPropertyKey, value);
        }

        public Type DisplayDialogsFromType
        {
            get => (Type)GetValue(DisplayDialogsFromTypeProperty);
            set => SetValue(DisplayDialogsFromTypeProperty, value);
        }

        public DialogContainer()
        {
            _DisplayedDialogs = new List<BaseDialog>();

            DialogManager.Subscribe(this, typeof(object));
        }

        internal void DisplayDialog(BaseDialog dialog)
        {
            Dispatcher.Invoke(() =>
            {
                _DisplayedDialogs.Add(dialog);

                CurrentDialog = dialog;
            });
        }

        internal void RemoveDialog(BaseDialog dialog)
        {
            Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < _DisplayedDialogs.Count; i++)
                {
                    if (_DisplayedDialogs[i] == dialog)
                    {
                        _DisplayedDialogs.RemoveAt(i--);
                    }
                }

                if (dialog == CurrentDialog)
                {
                    CurrentDialog = _DisplayedDialogs.LastOrDefault();
                }
            });
        }

        private static void DisplayDialogsFromTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            var container = sender as DialogContainer;

            if(container != null)
            {
                DialogManager.Unsubscribe(container);

                if(eventArgs.NewValue is Type t)
                {
                    DialogManager.Subscribe(container, t);
                }
            }
        }
    }
}