using SimpleDialogs.Enumerators;
using System;

namespace SimpleDialogs.Controls
{
    public class DialogClosedEventArgs : EventArgs
    {
        public DialogButton ClickedButton { get; }
        public object Result { get; }

        public DialogClosedEventArgs(DialogButton clickedButton, object result)
        {
            ClickedButton = clickedButton;
            Result = result;
        }
    }
}