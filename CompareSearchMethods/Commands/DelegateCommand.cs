using System;
using System.Windows.Input;

namespace CompareSearchMethods.Commands
{
	public class DelegateCommand : ICommand
	{
		private readonly Predicate<object> _canExecute;
		private readonly Action<object> _execute;

		public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{ return _canExecute(parameter); }

		public void Execute(object parameter)
		{ _execute(parameter); }
	}
}
