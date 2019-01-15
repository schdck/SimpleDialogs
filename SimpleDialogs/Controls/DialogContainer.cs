using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SimpleDialogs.Controls
{
    public class DialogContainer : UIElement
    {
        private List<BaseDialog> _VisibleDialogs;

        private static DependencyPropertyKey CurrentDialogPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(CurrentDialog), typeof(BaseDialog), typeof(DialogContainer), new PropertyMetadata());

        public static DependencyProperty CurrentDialogProperty =
            CurrentDialogPropertyKey.DependencyProperty;

        public BaseDialog CurrentDialog
        {
            get => (BaseDialog)GetValue(CurrentDialogProperty);
            private set => SetValue(CurrentDialogPropertyKey, value);
        }

        public static DependencyProperty DisplayDialogsFromTypeProperty =
            DependencyProperty.Register(nameof(DisplayDialogsFromType), typeof(Type), typeof(DialogContainer), new PropertyMetadata(typeof(object), new PropertyChangedCallback(DisplayDialogsFromTypeChanged)));

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

        public void DisplayDialog(BaseDialog dialog)
        {
            _VisibleDialogs.Add(dialog);

            CurrentDialog = dialog;
        }

        internal void CloseDialog(BaseDialog dialog)
        {
            for(int i = 0; i < _VisibleDialogs.Count; i++)
            {
                if(_VisibleDialogs[i] == dialog)
                {
                    _VisibleDialogs.RemoveAt(i--);
                }
            }

            if(dialog == CurrentDialog)
            {
                CurrentDialog = _VisibleDialogs.LastOrDefault();
            }
        }

        internal void CloseAllDialogs()
        {
            _VisibleDialogs.Clear();

            CurrentDialog = null;
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
