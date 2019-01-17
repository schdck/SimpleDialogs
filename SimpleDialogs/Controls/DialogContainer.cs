using SimpleDialogs.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SimpleDialogs.Controls
{
    public class DialogContainer : Control
    {
        private static DependencyPropertyKey CurrentDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CurrentDialog), typeof(BaseDialog), typeof(DialogContainer), new PropertyMetadata());

        public static DependencyProperty CurrentDialogProperty =
            CurrentDialogPropertyKey.DependencyProperty;

        public static DependencyProperty DisplayDialogsFromTypeProperty =
            DependencyProperty.Register(nameof(DisplayDialogsFromType), typeof(Type), typeof(DialogContainer), new PropertyMetadata(typeof(object), new PropertyChangedCallback(DisplayDialogsFromTypeChanged)));

        private List<BaseDialog> _VisibleDialogs;

        public BaseDialog CurrentDialog
        {
            get => (BaseDialog)GetValue(CurrentDialogProperty);
            private set => SetValue(CurrentDialogPropertyKey, value);
        }

        public Type DisplayDialogsFromType
        {
            get => (Type)GetValue(DisplayDialogsFromTypeProperty);
            private set => SetValue(DisplayDialogsFromTypeProperty, value);
        }

        public DialogContainer()
        {
            _VisibleDialogs = new List<BaseDialog>();

            DialogManager.Subscribe(this, typeof(object));
        }

        internal void DisplayDialog(BaseDialog dialog)
        {
            _VisibleDialogs.Add(dialog);

            CurrentDialog = dialog;

            dialog.Show();
        }

        internal void CloseDialog(BaseDialog dialog, DialogResult result)
        {
            for (int i = 0; i < _VisibleDialogs.Count; i++)
            {
                if (_VisibleDialogs[i] == dialog)
                {
                    _VisibleDialogs.RemoveAt(i--);
                }
            }

            if (dialog == CurrentDialog)
            {
                CurrentDialog = _VisibleDialogs.LastOrDefault();
            }

            dialog.Close(result);
        }

        internal void CloseAllDialogs()
        {
            while(CurrentDialog != null)
            {
                CloseDialog(CurrentDialog, DialogResult.None);
            }
        }

        private static void DisplayDialogsFromTypeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            var container = sender as DialogContainer;

            if(container != null)
            {
                DialogManager.Unsubscribe(container);

                DialogManager.Subscribe(container, eventArgs.NewValue as Type);
            }
        }
    }
}