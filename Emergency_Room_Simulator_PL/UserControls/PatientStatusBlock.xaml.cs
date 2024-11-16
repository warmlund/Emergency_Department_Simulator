using System.Windows;
using System.Windows.Controls;

namespace Emergency_Department_Simulator_PL
{
    /// <summary>
    /// Interaction logic for PatientStatusBlock.xaml
    /// </summary>
    public partial class PatientStatusBlock : UserControl
    {
        public PatientStatusBlock()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(PatientStatusBlock), new PropertyMetadata(0));

        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(PatientStatusBlock), new PropertyMetadata("Default Description"));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
    }
}
