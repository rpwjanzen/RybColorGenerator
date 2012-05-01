using System;
using System.Windows.Input;

namespace ColorSpace
{
    public sealed class RelayCommand : ICommand
    {
        private readonly Action<object> m_action;

        public RelayCommand(Action<object> action)
        {
            m_action = action;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            m_action(parameter);
        }

        #endregion
    }
}
