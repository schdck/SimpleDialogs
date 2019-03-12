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

        internal static void Subscribe(DialogContainer container, Type senderType)
        {
            _Listeners.Add(new Tuple<DialogContainer, Type>(container, senderType));
        }

        internal static void Unsubscribe(DialogContainer container)
        {
            _Listeners.RemoveAll(x => x.Item1 == container);
        }

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

            return null;
        }

        public static Task<DialogClosedEventArgs> ShowDialogForResult(object sender, BaseDialog dialog)
        {
            return ShowDialogAsync(sender, dialog)?.ContinueWith(x =>
            {
                return dialog.WaitForCloseAsync();
            }).Unwrap();
        }

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

            if(dialog.StartToCloseDialog(result))
            {
                return dialog.Container.RemoveDialogAsync(dialog);
            }

            return null;
        }
    }
}