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
    /// Interaction logic for DisplayOne.xaml
    /// </summary>
    public partial class DisplayOne : Page
    {
        int current = 0;
        public DisplayOne()
        {
            InitializeComponent();
            MsgList list = MsgList.Instance();
            showId.Text = list.showID(0);
            showSub.Text = list.showSub(0);
            showCon.Text = list.showCon(0);
        }

        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage home = new MainPage();
            this.NavigationService.Navigate(home);
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            showId.Text = list.showID(current + 1);
            showSub.Text = list.showSub(current + 1);
            showCon.Text = list.showCon(current + 1);
            current++;
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            showId.Text = list.showID(current - 1);
            showSub.Text = list.showSub(current - 1);
            showCon.Text = list.showCon(current - 1);
            current--;

        }
    }
}
