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

            DialogCloseCommand = new RelayCommand<DialogResult>((result) =>
            {
                DialogResult = result;

                DialogManager.HideVisibleDialog();
            });

            ShowDialogCommand = new RelayCommand<DialogStyle>((dialogType) =>
            {
                DialogResult = null;

                if(dialogType == DialogStyle.DefinedProgressDialog)
                {
                    ProgressDialog myDialog = new ProgressDialog()
                    {
                        CanCancel = false,
                        IsUndefined = false,
                        ExitDialogCommand = DialogCloseCommand,
                        Message = "Please wait...",
                        Title = "Working..."
                    };

                    Timer t = new Timer(30);

                    t.Elapsed += (s, e) =>
                    {
                        MainWindow.Instance.Dispatcher.Invoke(() =>
                        {
                            if (myDialog.Progress < 100)
                            {
                                myDialog.Progress++;
                            }
                            else
                            {
                                t.Stop();
                            }
                        });
                    };

                    DialogManager.ShowDialog(myDialog);

                    t.Start();
                }
                else
                {
                    BaseDialog myDialog = null;

                    switch (dialogType)
                    {
                        case DialogStyle.UndefinedProgressDialog:
                            myDialog = new ProgressDialog()
                            {
                                CanCancel = true,
                                IsUndefined = true,
                                ExitDialogCommand = DialogCloseCommand,
                                Message = "Please wait...",
                                Title = "Working..."
                            };
                            break;

                        case DialogStyle.InformationDialog:
                            myDialog = new AlertDialog()
                            {
                                AlertLevel = AlertLevel.Information,
                                ButtonsStyle = DialogButtonStyle.Ok,
                                ExitDialogCommand = DialogCloseCommand,
                                ShowCopyToClipboardButton = false,
                                Message = "This is some information that I wanted to show you!",
                                Title = "Information"
                            };
                            break;

                        case DialogStyle.SuccessDialog:
                            myDialog = new AlertDialog()
                            {
                                AlertLevel = AlertLevel.Success,
                                ButtonsStyle = DialogButtonStyle.Ok,
                                ExitDialogCommand = DialogCloseCommand,
                                ShowCopyToClipboardButton = false,
                                Message = "Some operation succeeded!",
                                Title = "Success!"
                            };
                            break;

                        case DialogStyle.WarningDialog:
                            myDialog = new AlertDialog()
                            {
                                AlertLevel = AlertLevel.Warning,
                                ButtonsStyle = DialogButtonStyle.Ok,
                                ExitDialogCommand = DialogCloseCommand,
                                ShowCopyToClipboardButton = true,
                                Message = "This is a Alert (Warning) Dialog example",
                                Title = "My custom warning"
                            };
                            break;

                        case DialogStyle.ErrorDialog:
                            myDialog = new AlertDialog()
                            {
                                AlertLevel = AlertLevel.Error,
                                ButtonsStyle = DialogButtonStyle.YesNo,
                                ExitDialogCommand = DialogCloseCommand,
                                ShowCopyToClipboardButton = false,
                                Message = "You can also confirm something while displaying an alert or message dialog... Got it?",
                                Title = "Error example"
                            };
                            break;

                        case DialogStyle.CriticalDialog:
                            myDialog = new AlertDialog()
                            {
                                AlertLevel = AlertLevel.Critical,
                                ButtonsStyle = DialogButtonStyle.Ok,
                                ExitDialogCommand = DialogCloseCommand,
                                ShowCopyToClipboardButton = false,
                                Message = "Something really bad happend, so I'm gona leave a big message here:\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.",
                                Title = "Critical error"
                            };
                            break;

                        case DialogStyle.ExceptionDialog:
                            try
                            {
                                string s = null;
                                s.IndexOf('s');
                            }
                            catch (Exception e)
                            {
                                myDialog = new AlertDialog()
                                {
                                    AlertLevel = AlertLevel.Warning,
                                    ButtonsStyle = DialogButtonStyle.Ok,
                                    ExitDialogCommand = DialogCloseCommand,
                                    ShowCopyToClipboardButton = true,
                                    Exception = e,
                                    Message = "Some exception was caught. The details are contained in the clipboard after 'CopyToClipboard' button is clicked.",
                                    Title = "Exception ocurred"
                                };
                            }

                            break;
                    }

                    DialogManager.ShowDialog(myDialog);
                }
            });
        }
    }
}