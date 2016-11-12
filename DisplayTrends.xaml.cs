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

            HashTags tag = HashTags.Instance();
            for(int i=0;i<tag.getSize();i++)
            {
                trendsView.Items.Add(tag.showTag(i));
            }

            Mentions men = Mentions.Instance();
            for(int i=0;i<men.getSize();i++)
            {
                mentionsView.Items.Add(men.showID(i));
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainPage mP = new MainPage();
            this.NavigationService.Navigate(mP);
        }
    }
}
