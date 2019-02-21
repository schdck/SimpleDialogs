using SimpleDialogs.Enumerators;

namespace SimpleDialogs.Controls
{
    public class DialogButtonClickedEventArgs
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
