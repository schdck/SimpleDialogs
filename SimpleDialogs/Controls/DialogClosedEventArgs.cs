using SimpleDialogs.Enumerators;

namespace SimpleDialogs.Controls
{
    public class DialogClosedEventArgs
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
