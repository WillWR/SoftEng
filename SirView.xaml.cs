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
    /// Interaction logic for SirView.xaml
    /// </summary>
    public partial class SirView : Page
    {
        int current = 0;
        public SirView()
        {
            //SHOWS THE FIRST SIR MESSAGE IN THE LIST.
            InitializeComponent();
            SirList list = SirList.Instance();
            showId.Text = list.showID(0);
            showSen.Text = list.showSen(0);
            showSub.Text = list.showSub(0);
            showSirNo.Text = list.showSirCode(0);
            showNoi.Text = list.showNoi(0);
            showCon.Text = list.showCon(0);
        }
        //METHOD FOR BACK BUTTON CLICK.
        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage home = new MainPage();
            this.NavigationService.Navigate(home);
        }
        //METHOD FOR PREVOIUS BUTTON CLICK
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SirList list = SirList.Instance();
            int max = list.getSize() - 1;
            if(current==0)
            {
                current = max;
                showId.Text = list.showID(current);
                showSen.Text = list.showSen(current);
                showSub.Text = list.showSub(current);
                showSirNo.Text = list.showSirCode(current);
                showNoi.Text = list.showNoi(current);
                showCon.Text = list.showCon(current);
            }
            else
            {
                showId.Text = list.showID(current - 1);
                showSen.Text = list.showSen(current-1);
                showSub.Text = list.showSub(current-1);
                showSirNo.Text = list.showSirCode(current-1);
                showNoi.Text = list.showNoi(current-1);
                showCon.Text = list.showCon(current-1);
                current--;
            }
        }
        //METHOD FOR NEXT BUTTON CLICK.
        private void next_Click(object sender, RoutedEventArgs e)
        {
            SirList list = SirList.Instance();
            int max = list.getSize() - 1;
            if (current != max)
            {
                showId.Text = list.showID(current+1);
                showSen.Text = list.showSen(current+1);
                showSub.Text = list.showSub(current+1);
                showSirNo.Text = list.showSirCode(current+1);
                showNoi.Text = list.showNoi(current+1);
                showCon.Text = list.showCon(current+1);
                current++;
            }
            else
            {
                current = 0;
                showId.Text = list.showID(current);
                showSen.Text = list.showSen(current);
                showSub.Text = list.showSub(current);
                showSirNo.Text = list.showSirCode(current);
                showNoi.Text = list.showNoi(current);
                showCon.Text = list.showCon(current);
            }
        }
    }
}
