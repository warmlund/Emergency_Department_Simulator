using Emergency_Department_Simulator_PL.Modal;
using System.Windows;

namespace Emergency_Department_Simulator_PL.Events
{
    public class EventManager
    {
        #region event for closing modal windows
        public static bool GetEnableCloseModalEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableCloseModalEventsProperty);
        }

        public static void SetEnableCloseModalEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableCloseModalEventsProperty, value);
        }

        public static readonly DependencyProperty EnableCloseModalEventsProperty = DependencyProperty.RegisterAttached("EnableCloseModalEvents", typeof(bool), typeof(EventManager), new PropertyMetadata(false, EnableCloseModalChanged));

        private static void EnableCloseModalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window) //Checks if the objet is a window
            {
                window.DataContextChanged += (s, e) => //event handler for when datacontext has changed
                {
                    if (window.DataContext is AddPatientViewModel vm) //Checks if the data context is the view model
                    {
                        vm.Close = () => //event handler for closing the window
                        {
                            window.DialogResult = vm.DialogResult; //sets the window dialog result to the viewmodel dialog result
                            window.Close(); //close the window
                        };
                    }
                };
            }
        }
        #endregion
    }
}
