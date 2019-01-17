using SimpleDialogs.Enumerators;

namespace SimpleDialogs.Controls
{
    public class DialogClosedEventArgs
    {
        public DialogResult Result { get; }

        public DialogClosedEventArgs(DialogResult result)
        {
            Result = result;
        }
    }
}
