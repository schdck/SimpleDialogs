using SimpleDialogs.Enumerators;
using System.ComponentModel;

namespace SimpleDialogs.Controls
{
    public class DialogClosingEventArgs : CancelEventArgs
    {
        public DialogButton Result { get; }

        public DialogClosingEventArgs(DialogButton result) : base(false)
        {
            Result = result;
        }
    }
}
