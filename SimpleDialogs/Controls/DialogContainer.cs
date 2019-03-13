using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Gets the current displayed dialog in this container
        /// </summary>
        public BaseDialog CurrentDialog
        {
            get => (BaseDialog)GetValue(CurrentDialogProperty);
            private set => SetValue(CurrentDialogPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets a value indicating from which sender type this container should display dialogs
        /// </summary>
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

        internal Task DisplayDialogAsync(BaseDialog dialog)
        {
            if(dialog.Container != null && dialog.Container != this)
            {
                throw new InvalidOperationException("The dialog is already on another container");
            }

            return Dispatcher.BeginInvoke(new Action(() =>
            {
                if(dialog.Container != null)
                {
                    // Remove it so we can add it to the last position of the list
                    _DisplayedDialogs.Remove(dialog);
                }

                _DisplayedDialogs.Add(dialog);

                CurrentDialog = dialog;
                CurrentDialog.Container = this;
            })).Task;
        }

        internal Task RemoveDialogAsync(BaseDialog dialog)
        {
            return Dispatcher.BeginInvoke(new Action(() =>
            {
                _DisplayedDialogs.Remove(dialog);

                if (dialog == CurrentDialog)
                {
                    CurrentDialog = _DisplayedDialogs.LastOrDefault();
                }

                dialog.Container = null;
            })).Task;
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