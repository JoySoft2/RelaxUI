using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TemaSzerkeszto.ViewModel
{
    /// <summary>
    /// ICommand interfészt megvalósító osztály
    /// </summary>
    public class Parancs : ICommand
    {
        private readonly Action<object> _vegrehajt = null;
        private readonly Predicate<object> _vegrehajthat = null;

        public Parancs(Action<object> execute) : this(execute, null) { }
        public Parancs(Action<object> execute, Predicate<object> canExecute)
        {
            _vegrehajt = execute;
            _vegrehajthat = canExecute;
        }

        //public event EventHandler CanExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _vegrehajthat == null || _vegrehajthat(parameter);
        //public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        public void Execute(object parameter) => _vegrehajt?.Invoke(parameter);
    }
}
