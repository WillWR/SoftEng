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
    /// Interaction logic for Quarantine.xaml
    /// </summary>
    public partial class Quarantine : Page
    {
        public Quarantine()
        {
            InitializeComponent();
            urlQuarantineList u = urlQuarantineList.Instance();
            int size = u.getSize();
            for (int i = 0; i < size; i++)
            {
                quarantineURL.Items.Add(u.showUrl(i));
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage home = new MainPage();
            this.NavigationService.Navigate(home);
        }
    }
}
