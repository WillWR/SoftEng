﻿using System;
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
        //SHOWS INITIAL FIRST MESSAGE.
        int current = 0;
        public DisplayOne()
        {
            InitializeComponent();
            MsgList list = MsgList.Instance();
            showId.Text = list.showID(0);
            showSen.Text = list.showSender(0);
            showSub.Text = list.showSub(0);
            showCon.Text = list.showCon(0);
        }
        //METHOD FOR BACK BUTTON CLICK
        private void bckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage home = new MainPage();
            this.NavigationService.Navigate(home);
        }
        //METHOD FOR NEXT BUTTON CLICK, RETRIEVES NEXT MESSAGE IN LIST.
        private void next_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            int max = list.getSize() - 1;
            if(current!=max)
            {
                showId.Text = list.showID(current + 1);
                showSen.Text = list.showSender(current + 1);
                showSub.Text = list.showSub(current + 1);
                showCon.Text = list.showCon(current + 1);
                current++;
            }
            else
            {
                current = 0;
                showId.Text = list.showID(current);
                showSen.Text = list.showSender(current);
                showSub.Text = list.showSub(current);
                showCon.Text = list.showCon(current);
            }
            
        }
        //METHOD FOR PREVIOUS BUTTON CLICK, RETREIVES PREVIOUS MESSAGE IN LIST.
        private void prev_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            int max = list.getSize()-1;
            if(current==0)
            {
                current = max;
                showId.Text = list.showID(current);
                showSen.Text = list.showSender(current);
                showSub.Text = list.showSub(current);
                showCon.Text = list.showCon(current);
            }
            else
            {
                showId.Text = list.showID(current - 1);
                showSen.Text = list.showSender(current - 1);
                showSub.Text = list.showSub(current - 1);
                showCon.Text = list.showCon(current - 1);
                current--;
            }
        }
    }
}
