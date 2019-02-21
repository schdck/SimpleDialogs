using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleDialogs.Controls;
using SimpleDialogs.Demo.Enumerators;
using SimpleDialogs.Enumerators;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;

namespace SimpleDialogs.Demo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<DialogStyle> ShowDialogCommand { get; private set; }
        public RelayCommand<DialogButton> DialogCloseCommand { get; private set; }
        public RelayCommand<Uri> OpenLinkCommand { get; private set; }

        public string DialogResult { get; private set; }

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
                        Message = "Please wait...",
                        Title = "Working..."
                    };

                    dialog.Closed += DialogClosed;

                    Timer t = new Timer(50);

                    t.Elapsed += (s, e) =>
                    {
                        MainWindow.Instance.Dispatcher.Invoke(() =>
                        {
                            if (dialog.Progress < 100)
                            {
                                dialog.Progress++;
                                dialog.Title = $"Working... ({dialog.Progress}%)";
                            }
                            else
                            {
                                dialog.CanClose = true;
                                dialog.Title = "Done!";
                                dialog.Message = "I don't know what I was doing, just know that it's finished.";

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
                        Message = "Please wait...",
                        Title = "Working..."
                    };

                    dialog.Closed += DialogClosed;

                    DialogManager.ShowDialog(this, dialog);
                }
                else
                {
                    MessageDialog dialog = new MessageDialog()
                    {
                        Title = "MESSAGE DIALOG",
                    };

                    dialog.Closed += DialogClosed;
                    dialog.ButtonClicked += DialogButtonClicked;

                    switch (dialogType)
                    {
                        case DialogStyle.InformationDialog:
                            dialog.MessageSeverity = MessageSeverity.Information;
                            dialog.Message = "Move along.\n\nNothing to see here.";
                            dialog.SecondsToAutoClose = 3;
                            break;

                        case DialogStyle.SuccessDialog:
                            dialog.MessageSeverity = MessageSeverity.Success;
                            dialog.Message = "WE DID IT!!";
                            dialog.FontWeight = FontWeights.SemiBold;
                            dialog.FontSize = 14;
                            break;

                        case DialogStyle.WarningDialog:
                            dialog.MessageSeverity = MessageSeverity.Warning;
                            dialog.Message = "PLEASE BE CAREFUL (THIS IS A WARNING)";
                            break;

                        case DialogStyle.ErrorDialog:
                            dialog.MessageSeverity = MessageSeverity.Error;
                            dialog.ShowSecondButton = true;
                            dialog.ShowThirdButton = true;
                            dialog.FirstButtonContent = "YES";
                            dialog.SecondButtonContent = "NO";
                            dialog.ThirdButtonContent = "COPY DETAILS";
                            dialog.Message = "You can also confirm something while displaying an alert or message dialog... Got it?";
                            break;

                        case DialogStyle.CriticalDialog:
                            dialog.MessageSeverity = MessageSeverity.Critical;
                            dialog.Message = "Something really bad happend, so I'm gona leave a big message here:\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.";
                            break;

                        case DialogStyle.ExceptionDialog:
                            dialog.MessageSeverity = MessageSeverity.Warning;
                            dialog.Message = "Some exception was caught. The details are contained in the clipboard after 'CopyToClipboard' button is clicked.";
                            dialog.ShowThirdButton = true;
                            dialog.ThirdButtonContent = "COPY DETAILS";
                            dialog.Exception = new Exception("This is some exception that I got for you <3", new Exception("And this is its inner exception <3 <3"));
                            break;
                    }

                    DialogManager.ShowDialog(this, dialog);
                }
            });
        }

        private void DialogButtonClicked(object sender, DialogButtonClickedEventArgs e)
        {
            // Since the only third button we have in this demo is to copy to clipboard
            // We will prevent the dialog from closing when the user select this option
            if(e.Button == DialogButton.ThirdButton && sender is MessageDialog dialog)
            {
                Clipboard.SetText($"Title: {dialog.Title}\r\nMessage: {dialog.Message}\r\nException: {(dialog.Exception?.ToString() ?? "null")}");

                e.CloseDialogAfterHandle = false;
            }
        }

        private void DialogClosed(object sender, DialogClosedEventArgs e)
        {
            if(sender is BaseDialog dialog)
            {
                switch(e.Result)
                {
                    case DialogButton.FirstButton:
                        DialogResult = $"FirstButton ({dialog.FirstButtonContent})";
                        break;

                    case DialogButton.SecondButton:
                        DialogResult = $"SecondButton ({dialog.SecondButtonContent})";
                        break;

                    case DialogButton.ThirdButton:
                        DialogResult = $"ThirdButton ({dialog.ThirdButtonContent})";
                        break;
                }
            }
        }
    }
}