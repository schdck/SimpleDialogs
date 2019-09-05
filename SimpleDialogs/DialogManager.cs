using SimpleDialogs.Controls;
using SimpleDialogs.Enumerators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleDialogs
{
    public static class DialogManager
    {
        private static List<Tuple<DialogContainer, Type>> _Listeners = new List<Tuple<DialogContainer, Type>>();
        private static List<Tuple<Type, BaseDialog>> _DialogsOnQueue = new List<Tuple<Type, BaseDialog>>();

        internal static void Subscribe(DialogContainer container, Type senderType)
        {
            _Listeners.Add(new Tuple<DialogContainer, Type>(container, senderType));

            while(_DialogsOnQueue.Count > 0)
            {
                container.DisplayDialogAsync(_DialogsOnQueue[0].Item2);

                _DialogsOnQueue.RemoveAt(0);
            }
        }

        internal static void Unsubscribe(DialogContainer container)
        {
            _Listeners.RemoveAll(x => x.Item1 == container);
        }

        /// <summary>
        /// Shows a dialog asynchronously
        /// </summary>
        /// <param name="sender">The object that is launching the dialog</param>
        /// <param name="dialog">The dialog to be shown</param>
        /// <returns>The task that is used to open the dialog or null if no listener is found</returns>
        public static Task ShowDialogAsync(object sender, BaseDialog dialog)
        {
            var type = sender.GetType();

            DialogContainer genericListener = null,
                            specificListener = null;

            foreach(var listener in _Listeners)
            {
                if(listener.Item2.Equals(type))
                {
                    // We found a listener specific for that type, so let's use that
                    specificListener = listener.Item1;
                    break;
                }
                else if(listener.Item2.IsAssignableFrom(type))
                {
                    /* In case we don't find a specific listener, use any that subscribed 
                     * for a type that is a parent class of the sender 
                     * 
                     * For example, if the sender is a Window and we find no one listening
                     * for that specific type, we could use someone who's listening for an
                     * UIElement. */
                    genericListener = listener.Item1;
                }
            }

            if(specificListener != null)
            {
                specificListener.DisplayDialogAsync(dialog);

                return dialog.WaitForLoadAsync();
            }
            else if(genericListener != null)
            {
                genericListener.DisplayDialogAsync(dialog);

                return dialog.WaitForLoadAsync();
            }

            _DialogsOnQueue.Add(new Tuple<Type, BaseDialog>(type, dialog));

            return null;
        }

        /// <summary>
        /// Shows a dialog and wait for it to close asynchronously
        /// </summary>
        /// <param name="sender">The object that is launching the dialog</param>
        /// <param name="dialog">The dialog to be shown</param>
        /// <returns>The DialogClosedEventArgs or null if no listener is found</returns>
        public static Task<DialogClosedEventArgs> ShowDialogForResult(object sender, BaseDialog dialog)
        {
            return ShowDialogAsync(sender, dialog)?.ContinueWith(x =>
            {
                return dialog.WaitForCloseAsync();
            }).Unwrap();
        }

        /// <summary>
        /// Closes a dialog asynchronously
        /// </summary>
        /// <param name="dialog">The dialog to be closed</param>
        /// <returns>The task used to close the dialog or null if the close is aborted</returns>
        public static Task CloseDialogAsync(BaseDialog dialog)
        {
            return CloseDialogAsync(dialog, DialogButton.None);
        }

        internal static Task CloseDialogAsync(BaseDialog dialog, DialogButton result)
        {
            if(dialog.Container == null)
            {
                throw new InvalidOperationException("The dialog is not in any container");
            }

            return dialog.Container.RemoveDialogAsync(dialog);
        }
    }
}