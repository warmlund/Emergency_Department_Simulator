﻿using System.Windows.Input;

namespace Emergency_Department_Simulator_PL.Commands
{
    public class AsyncCommand : ICommand
    {
        private readonly Func<Task> _execute; //variable representing the async command that is being executed
        private readonly Func<bool> _canExecute; //variable that represents the boolean which checks if you can execute the command or not
        public event EventHandler CanExecuteChanged; // An event that is triggered if the state of _canExecute changes

        /// <summary>
        /// Constructor and sets the variable with the arguments
        /// </summary>

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Method that determines if the async command can be executed based on _canExecute
        /// </summary>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();


        /// <summary>
        /// Executes the command
        /// </summary>
        public async void Execute(object parameter)
        {
            await _execute();
        }

        /// <summary>
        /// Method that is called to raise the CanExecuteChanged
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
