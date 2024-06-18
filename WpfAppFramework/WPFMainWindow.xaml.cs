using BusinessLayer.Services;
using System.Windows;

namespace WpfAppFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        EmployeeReader _reader;

        public MainWindowView(EmployeeReader reader)
        {
            InitializeComponent();
            Title = "With Solid and Dependency Injection";
            _reader = reader;
            this.DataContext = _reader;
        }
        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            _reader.RefreshEmployee();

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

        //private void PersonListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{

        //}
    }
}