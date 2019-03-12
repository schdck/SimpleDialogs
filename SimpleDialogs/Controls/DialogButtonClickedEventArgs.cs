using SimpleDialogs.Enumerators;
using System;

namespace SimpleDialogs.Controls
{
    public class DialogButtonClickedEventArgs : EventArgs
    {
        public bool CloseDialogAfterHandle { get; set; }
        public DialogButton Button { get; }

        public DialogButtonClickedEventArgs(DialogButton button)
        {
            Button = button;
            CloseDialogAfterHandle = true;
        }
    }
}
