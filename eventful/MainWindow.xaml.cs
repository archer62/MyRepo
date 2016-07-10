using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace eventful
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (searchButton.Content.ToString() == "Search")
                {
                    searchButton.Content = "Cancel";

                    await _viewModel.GetResponse(textBoxLocation.Text, datePickerStartDate.SelectedDate, datePickerEndDate.SelectedDate, textBoxKeywords.Text);
                    searchButton.Content = "Search";
                    MessageBox.Show("Done!");
                }
                else
                {
                    _viewModel.Cancel();
                    searchButton.Content = "Search";
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ooooops");
            }
        }
    }
}
