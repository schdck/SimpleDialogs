using SimpleDialogs.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        internal static void ShowDialog(object sender, BaseDialog dialog)
        {
            var type = sender.GetType();

            foreach(var listener in _Listeners)
            {
                if(listener.Item2.IsAssignableFrom(type))
                {
                    listener.Item1.DisplayDialog(dialog);
                }
            }
        }

        public static void HideDialog(BaseDialog dialog)
        {
            foreach (var listener in _Listeners)
            {
                listener.Item1.CloseDialog(dialog);
            }
        }

        public static void HideAllVisibleDialogs()
        {
            foreach(var listener in _Listeners)
            {
                listener.Item1.CloseAllDialogs();
            }
        }
    }
}
