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
    /// Interaction logic for DisplayTrends.xaml
    /// </summary>
    public partial class DisplayTrends : Page
    {
        public DisplayTrends()
        {
            InitializeComponent();
            //ADDS ALL HASHTAGS TO LIST VIEW BOX.
            HashTags tag = HashTags.Instance();
            for(int i=0;i<tag.getSize();i++)
            {
                trendsView.Items.Add(tag.showTag(i));
            }
            //ADDS ALL MENTIONS TO LIST VIEW BOX.
            Mentions men = Mentions.Instance();
            for(int i=0;i<men.getSize();i++)
            {
                mentionsView.Items.Add(men.showID(i));
            }

        }
        //METHOD FOR BACK BUTTON CLICK.
        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainPage mP = new MainPage();
            this.NavigationService.Navigate(mP);
        }
    }
}
