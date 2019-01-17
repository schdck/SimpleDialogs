using System;
using System.Windows.Input;

namespace SimpleDialogs.Commands
{
    internal class SimpleCommand : ICommand
    {
        private Action action;

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        public SimpleCommand(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }

    internal class SimpleCommand<T> : ICommand
    {
        private Action<T> action;

        #pragma warning disable 67
        public event EventHandler CanExecuteChanged;
        #pragma warning restore 67

        public SimpleCommand(Action<T> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is T param)
            {
                action?.Invoke(param);
            }
            else
            {
                throw new ArgumentException($"The parameter is not of type T ({ typeof(T) } )");
            }
        }
    }
}
