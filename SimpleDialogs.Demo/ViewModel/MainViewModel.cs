using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleDialogs.Controls;
using SimpleDialogs.Enumerators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace SimpleDialogs.Demo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public string DialogResult { get; private set; }
        public string DialogClickedButton { get; private set; }

        public bool IsFlyoutOpen { get; private set; } = true;
        public bool IsCreatingMessageDialog => SelectedType?.ToLower() == "message dialog";
        public bool IsCreatingProgressDialog => SelectedType?.ToLower() == "progress dialog";
        public bool IsCreatingInputDialog => SelectedType?.ToLower() == "input dialog";

        public string SelectedType { get; set; }
        public MessageSeverity? SelectedSeverity { get; set; }
        public DialogButton? SelectedButton { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public Thickness Margin { get; set; }

        public int? SecondsToAutoClose { get; set; }
        public bool ShowOverlay { get; set; } = true;
        public bool CanClose { get; set; } = true;
        public bool IsUndefined { get; set; }

        public bool ShowFirstButton { get; set; } = true;
        public bool ShowSecondButton { get; set; }
        public bool ShowThirdButton { get; set; }

        public bool CloseOnFirstButton { get; set; } = true;
        public bool CloseOnSecondButton { get; set; } = true;
        public bool CloseOnThirdButton { get; set; } = true;

        public string FirstButtonContent { get; set; } = "OK";
        public string SecondButtonContent { get; set; }
        public string ThirdButtonContent { get; set; }

        public ICommand ShowFlyoutCommand { get; private set; }
        public ICommand ShowDialogCommand { get; private set; }
        public ICommand CloseCurrentDialogCommand { get; private set; }
        public ICommand OpenLinkCommand { get; private set; }

        private List<BaseDialog> DialogStack;
        private MessageDialog ErrorDialog;

        public MainViewModel()
        {
            DialogStack = new List<BaseDialog>();

            ErrorDialog = new MessageDialog()
            {
                Title = "ERROR",
                MessageSeverity = MessageSeverity.Error
            };

            ShowDialogCommand = new RelayCommand(() =>
            {
                ShowDialog();

                IsFlyoutOpen = false;
            });

            CloseCurrentDialogCommand = new RelayCommand(() =>
            {
                if(DialogStack.Count > 0)
                {
                    var lastIndex = DialogStack.Count - 1;
                    var lastDialog = DialogStack[lastIndex];

                    DialogStack.RemoveAt(lastIndex);
                    DialogManager.CloseDialog(lastDialog);
                }
            });

            OpenLinkCommand = new RelayCommand<Uri>((uri) =>
            {
                Process.Start(new ProcessStartInfo(uri.AbsoluteUri));
            });

            ShowFlyoutCommand = new RelayCommand(() =>
            {
                IsFlyoutOpen = true;
            });
        }

        private void ShowDialog()
        {
            if(SelectedType == null || SelectedButton == null)
            {
                ErrorDialog.Message = "Please inform the dialog type and auto focused button";

                DialogManager.ShowDialog(this, ErrorDialog);

                return;
            }

            BaseDialog dialog = null;

            switch(SelectedType.ToLower())
            {
                case "message dialog":
                    if(SelectedSeverity == null)
                    {
                        ErrorDialog.Message = "Please inform the message severity";

                        DialogManager.ShowDialog(this, ErrorDialog);

                        return;
                    }

                    var messageDialog = new MessageDialog()
                    {
                        MessageSeverity = SelectedSeverity.Value,
                        Title = "Message dialog"
                    };

                    switch(SelectedSeverity)
                    {
                        case MessageSeverity.Information:
                            messageDialog.Message = "Move along.\n\nNothing to see here.";
                            break;

                        case MessageSeverity.Success:
                            messageDialog.Message = "WE DID IT!!";
                            messageDialog.FontSize = 14;
                            break;

                        case MessageSeverity.Warning:
                            messageDialog.Message = "PLEASE BE CAREFUL (THIS IS A WARNING)";
                            break;

                        case MessageSeverity.Error:
                            messageDialog.Message = "Some exception was caught.";
                            messageDialog.Exception = new Exception("This is some exception that I got for you <3", new Exception("And this is its inner exception"));
                            break;

                        case MessageSeverity.Critical:
                            messageDialog.Message = "Something really bad happend, so I'm gona leave a big message here:\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.\n\nLorem ipsum dolor sit amet, vim melius doctus at. An modo movet vituperata eos, sit id doming noster. Quo quando putent et, mei in verterem adolescens. No dolore nemore referrentur pro, per mollis patrioque at. Saepe volumus petentium ei vel.";
                            break;

                    } 

                    dialog = messageDialog;

                    break;

                case "progress dialog":
                    var progressDialog = new ProgressDialog()
                    {
                        IsUndefined = IsUndefined,
                        Title = "PROGRESS DIALOG",
                        Message = "Working...."
                    };

                    if(!IsUndefined)
                    { 
                        var t = new Timer(50)
                        {
                            AutoReset = false
                        };

                        t.Elapsed += (s, e) =>
                        {
                            MainWindow.Instance.Dispatcher.Invoke(() =>
                            {
                                if (!progressDialog.IsClosed)
                                {
                                    if (progressDialog.Progress < 100)
                                    {
                                        progressDialog.Progress++;
                                        progressDialog.Title = $"Working... ({progressDialog.Progress}%)";
                                        t.Start();
                                    }
                                    else
                                    {
                                        progressDialog.Title = "Done!";
                                        progressDialog.Message = "I don't know what I was doing, just know that it's finished.";
                                    }
                                }
                            });
                        };

                        t.Start();
                    }

                    dialog = progressDialog;

                    break;

                case "input dialog":
                    var inputDialog = new InputDialog()
                    {
                        Title = "INPUT DIALOG",
                        Description = "Please insert any text you want",
                        Watermark = "Insert your text here",
                    };

                    dialog = inputDialog;

                    break;
            }

            if(dialog != null)
            {
                dialog.ShowOverlay = ShowOverlay;
                dialog.CanClose = CanClose;
                dialog.AutoFocusedButton = SelectedButton.Value;

                dialog.ShowFirstButton = ShowFirstButton;
                dialog.ShowSecondButton = ShowSecondButton;
                dialog.ShowThirdButton = ShowThirdButton;

                dialog.FirstButtonContent = FirstButtonContent;
                dialog.SecondButtonContent = SecondButtonContent;
                dialog.ThirdButtonContent = ThirdButtonContent;

                dialog.HorizontalAlignment = HorizontalAlignment;
                dialog.VerticalAlignment = VerticalAlignment;
                dialog.Margin = Margin;

                dialog.Closed += DialogClosed;
                dialog.ButtonClicked += DialogButtonClicked;

                if(SecondsToAutoClose != null)
                {
                    dialog.SecondsToAutoClose = SecondsToAutoClose.Value;
                }

                DialogStack.Add(dialog);
                DialogManager.ShowDialog(this, dialog);
            }
        }

        private void DialogButtonClicked(object sender, DialogButtonClickedEventArgs e)
        {
            if (e.Button == DialogButton.FirstButton)
            {
                e.CloseDialogAfterHandle = CloseOnFirstButton;
            }
            else if (e.Button == DialogButton.SecondButton)
            {
                e.CloseDialogAfterHandle = CloseOnSecondButton;
            }
            else if (e.Button == DialogButton.ThirdButton)
            {
                e.CloseDialogAfterHandle = CloseOnThirdButton;
            }
        }

        private void DialogClosed(object sender, DialogClosedEventArgs e)
        {
            if (sender is BaseDialog dialog)
            {
                DialogStack.Remove(dialog);
                DialogResult = e.Result?.ToString();

                switch (e.ClickedButton)
                {
                    case DialogButton.FirstButton:
                        DialogClickedButton = $"FirstButton ({dialog.FirstButtonContent})";
                        break;

                    case DialogButton.SecondButton:
                        DialogClickedButton = $"SecondButton ({dialog.SecondButtonContent})";
                        break;

                    case DialogButton.ThirdButton:
                        DialogClickedButton = $"ThirdButton ({dialog.ThirdButtonContent})";
                        break;
                }
            }
        }
    }
}