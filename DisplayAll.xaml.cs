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
    /// Interaction logic for DisplayAll.xaml
    /// </summary>
    public partial class DisplayAll : Page
    {
        public DisplayAll()
        {
            InitializeComponent();
            MsgList list = MsgList.Instance();
            int size = list.getSize();
            for(int i=0; i<size; i++)
            {
                string message =  "Message ID:" + list.showID(i) + " Sender: " + list.showSender(i) + " Subject: " + list.showSub(i) + " Content: " + list.showCon(i);
                messageView.Items.Add(message);
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainPage mP = new MainPage();
            this.NavigationService.Navigate(mP);
        }
    }
}
