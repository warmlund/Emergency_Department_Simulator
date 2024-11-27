using Emergency_Department_Simulator_BLL;
using System.Windows;

namespace Emergency_Department_Simulator_PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PatientManager patientManager = new PatientManager();
            ViewModel viewModel= new ViewModel(patientManager);
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}