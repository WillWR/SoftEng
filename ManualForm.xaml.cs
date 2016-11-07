using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
          
            String eAccepted = @"[eE]+[0-9]{9}$";
            String sAccepted = @"[sS]+[0-9]{9}$";
            String tAccepted = @"[tT]+[0-9]{9}$";
            if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, eAccepted))))
            {
                String emailCheck = @"[a-zA-Z._]+[@]+[a-zA-Z]+[.]+[a-zA-Z.]{2,5}$";
                
                if ((addSenderBox.Text != "") && (Regex.IsMatch(addSenderBox.Text, emailCheck)))
                {
                    String subjectCheck = "^[a-zA-Z0-9.*]*${20}$";
                    if ((addSubjectBox.Text !="") && (Regex.IsMatch(addSubjectBox.Text, subjectCheck)))
                    {
                        String contentCheck = @"(.*?){1028}$";
                        if((addContentBox.Text!="") &&(Regex.IsMatch(addContentBox.Text, contentCheck)))
                        {

                            if(addContentBox.Text.Contains("http://"))
                            {
                                
                                String secondHalf;
                                String firstHalf;
                                String replace = "<URL Quanantined>";
                                string del = "http://";
                                int i = addContentBox.Text.LastIndexOf(del);
                                string copy = addContentBox.Text.Remove(0, i+1);
                                String url = copy.Substring(0, copy.IndexOf(""));
                                secondHalf = copy.Remove(0, copy.IndexOf(" ")); 
                                firstHalf = addContentBox.Text.Substring(0, i-1);
                               // addContentBox.Text = secondHalf;
                                addContentBox.Text = firstHalf + " " + replace + " " +secondHalf;
                                
                                
                            }
                            MsgList list = MsgList.Instance();
                            Message email = new Message(addIdBox.Text, addSenderBox.Text, addSubjectBox.Text, addContentBox.Text);
                            list.addMessage(email);
                            statusLbl.Text = "Message Added!";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
                            addSenderBox.Text = "";
                            addSubjectBox.Text = "";
                        }//CONTENT CHECK IF ENDS
                        else
                        {
                            statusLbl.Text = "invalid content.";
                            addContentBox.Text = "";
                        }//CONTENT CHECK ELSE ENDS

                    }//SUBJECT CHECK IF ENDS
                    else
                    {
                        statusLbl.Text = "Subject invalid, please ensure 20 characters or less.";
                        addSubjectBox.Text = "";
                        addContentBox.Text = "";
                    }//SUBJECT CHECK ELSE ENDS
                    
                }//SENDER CHECK IF ENDS
                else
                {
                    statusLbl.Text = "Sender Invalid, please enter valid email";
                    addSenderBox.Text = "";
                    addSubjectBox.Text = "";
                    addContentBox.Text = "";
                }//SENDER CHECK ELSE ENDS

            }//ID CHECK IF ENDS
            else
            {
                statusLbl.Text = "Message Id invalid. Please use S,E or T followed by 9 numbers.";
                addIdBox.Text = "";
                addSenderBox.Text = "";
                addSubjectBox.Text = "";
                addContentBox.Text = "";
            }//ID CHECK ELSE ENDS
        }
    }
}
