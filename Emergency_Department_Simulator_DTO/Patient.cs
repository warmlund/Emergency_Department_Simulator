using System.ComponentModel;

namespace Emergency_Department_Simulator_DTO
{
    /// <summary>
    /// patient class
    /// </summary>
    public class Patient : INotifyPropertyChanged
    {
        private StatusType _status;

        public string PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public StatusType Status //Statustype has an onpropertychanged so the ui can detect and update of the status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
