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
    /// Interaction logic for ManualForm.xaml
    /// </summary>
    public partial class ManualForm : Page
    {
        

        public ManualForm()
        {
            InitializeComponent();
            
        }

        private void backManual_Click(object sender, RoutedEventArgs e)
        {
            MainPage home = new MainPage();
            this.NavigationService.Navigate(home);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            Message m = new Message(addIdBox.Text, addSubjectBox.Text, addContentBox.Text);
            MsgList list = MsgList.Instance();
            list.addMessage(m);
            addIdBox.Text = "";
            addSubjectBox.Text = "";
            addContentBox.Text = "";
        }
    }
}
