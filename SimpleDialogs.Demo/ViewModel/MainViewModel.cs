using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleDialogs.Controls;
using SimpleDialogs.Demo.Enumerators;
using SimpleDialogs.Enumerators;
using System;
using System.Diagnostics;
using System.Timers;

namespace SimpleDialogs.Demo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<DialogStyle> ShowDialogCommand { get; private set; }
        public RelayCommand<DialogResult> DialogCloseCommand { get; private set; }
        public RelayCommand<Uri> OpenLinkCommand { get; private set; }

        public DialogResult? DialogResult { get; private set; }

        public MainViewModel()
        {
            OpenLinkCommand = new RelayCommand<Uri>((uri) =>
            {
                Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
            });

            ShowDialogCommand = new RelayCommand<DialogStyle>((dialogType) =>
            {
                if(dialogType == DialogStyle.DefinedProgressDialog)
                {
                    ProgressDialog dialog = new ProgressDialog()
                    {
                        CanClose = false,
                        Content = "Please wait...",
                        Title = "Working..."
                    };

                    dialog.DialogClosed += DialogClosed;

                    Timer t = new Timer(30);

                    t.Elapsed += (s, e) =>
                    {
                        MainWindow.Instance.Dispatcher.Invoke(() =>
                        {
                            if (dialog.Progress < 100)
                            {
                                dialog.Progress++;
                            }
                            else
                            {
                                dialog.CanClose = true;
                                dialog.Title = "It's done!";
                                dialog.Content = "I don't know what I was doing, just know that it's finished.";

                                t.Stop();
                            }
                        });
                    };

                    DialogManager.ShowDialog(this, dialog);

                    t.Start();
                }
                else if(dialogType == DialogStyle.UndefinedProgressDialog)
                {
                    var dialog = new ProgressDialog()
                    {
                        IsUndefined = true,
                        Content = "Please wait...",
                        Title = "Working..."
                    };

                    dialog.DialogClosed += DialogClosed;

                    DialogManager.ShowDialog(this, dialog);
                }
                else
                {
                    AlertDialog dialog = new AlertDialog()
                    {
                        Title = "Alert dialog",
                    };

                    dialog.DialogClosed += DialogClosed;

                    switch (dialogType)
                    {
                        case DialogStyle.InformationDialog:
                            dialog.MessageSeverity = MessageSeverity.Information;
                            dialog.Content = "This is some information that we wanted to show you!";
                            break;

                        case DialogStyle.SuccessDialog:
                            dialog.MessageSeverity = MessageSeverity.Success;
                            dialog.Content = "WE DID IT BOYS!!!";
                            break;

                        case DialogStyle.WarningDialog:
                            dialog.MessageSeverity = MessageSeverity.Warning;
                            dialog.Content = "PLEASE BE CAREFUL (THIS IS A WARNING)";
                            break;

                        case DialogStyle.ErrorDialog:
                            dialog.MessageSeverity = MessageSeverity.Error;
                            dialog.ShowAuxiliaryButton = true;
                            dialog.PrimaryButtonContent = "YES";
                            dialog.AuxiliaryButtonContent = "NO";
                            dialog.Content = "You can also confirm something while displaying an alert or message dialog... Got it?";
                            break;

                        case DialogStyle.CriticalDialog:
                            dialog.MessageSeverity = MessageSeverity.Critical;
                            dialog.Content = "Something really bad happend, so I'm gona leave a big message here:\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.";
                            break;

                        case DialogStyle.ExceptionDialog:
                            dialog.MessageSeverity = MessageSeverity.Warning;
                            dialog.Content = "Some exception was caught. The details are contained in the clipboard after 'CopyToClipboard' button is clicked.";
                            dialog.ShowCopyToClipboardButton = true;
                            dialog.Exception = new Exception("This is some exception that I got for you <3", new Exception("And this is its inner exception <3 <3"));
                            break;
                    }

                    DialogManager.ShowDialog(this, dialog);
                }
            });
        }

        private void DialogClosed(object sender, DialogClosedEventArgs e)
        {
            DialogResult = e.Result;
        }
    }
}