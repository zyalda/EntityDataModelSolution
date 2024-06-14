using BusinessLayer.Services;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainViewWindow : Window
    {
        EmployeeReader _reader;
       
        public MainViewWindow(EmployeeReader reader)
        {
            InitializeComponent();
            Title = "With Solid and Dependency Injection";
            _reader = reader;
            DataContext = _reader;
        }
        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            _reader.PrintEmployees();
   
            ShowRepositoryType();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _reader.ClearEmployee();
            ClearRepositoryType();
        }

        private void ShowRepositoryType()
        {
            RepositoryTypeTextBlock.Text = _reader.DataReaderType;
        }

        private void ClearRepositoryType()
        {
            RepositoryTypeTextBlock.Text = string.Empty;
        }
    }
}
