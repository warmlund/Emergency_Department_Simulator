using System.Windows.Input;

namespace Emergency_Department_Simulator_PL.Commands
{
    class Command : ICommand
    {
        private Action methodToExecute = null; //variable representing the command that is being executed
        private Func<bool> canMethodBeExecuted = null; //variable that represents the boolean which checks if you can execute the command or not
        public event EventHandler CanExecuteChanged;  // An event that is triggered if the state of _canExecute changes

        /// <summary>
        /// Constructor and sets the variable with the arguments
        /// </summary>
        public Command(Action methodToExecute, Func<bool> canMethodBeExecuted)
        {
            this.methodToExecute = methodToExecute;
            this.canMethodBeExecuted = canMethodBeExecuted;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        public void Execute(object parameter)
        {
            methodToExecute?.Invoke();
        }

        /// <summary>
        /// Method that determines if the command can be executed based on _canExecute
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (canMethodBeExecuted == null)
            {
                return true;
            }
            else
            {
                return canMethodBeExecuted();
            }
        }
    }
