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

namespace NBMFS
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

        }

        private void manualBtn_Click(object sender, RoutedEventArgs e)
        {
            ManualForm mF = new ManualForm();
            this.NavigationService.Navigate(mF);
        }

        private void viewSingleBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayOne dO = new DisplayOne();
            this.NavigationService.Navigate(dO);
        }
    }
}
