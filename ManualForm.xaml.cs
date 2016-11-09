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
            //===================Email message validation starts===================
            //=====================================================================
            //EMAIL MESSAGE ID CHECK STARTS
            if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, eAccepted))))
            {
                String emailCheck = @"[a-zA-Z._]+[@]+[a-zA-Z]+[.]+[a-zA-Z.]{2,5}$";
                //EMAIL SENDER CHECK STARTS
                if ((addSenderBox.Text != "") && (Regex.IsMatch(addSenderBox.Text, emailCheck)))
                {
                    String subjectCheck = @"^[a-zA-Z0-9.*]*${20}$";
                    String sirCheck = @"([S]+[I]+[R]+[ ]+(([0-2]+[0-9])|([3]+[0-1]))+[\/]+(([0]+[0-9])|([1]+[0-2]))+[\/]+(([0]+[0-9])|([1-2]+[0-9])))";
                    //EMAIL SUBJECT FOR NORMAL EMAIL CHECK STARTS
                    if ((addSubjectBox.Text !="") && (Regex.IsMatch(addSubjectBox.Text, subjectCheck)))
                    {
                        String contentCheck = @"(.*?){1028}$";
                        //EMAIL CONTENT FOR NORMAL EMAIL CHECK STARTS
                        if((addContentBox.Text!="") &&(Regex.IsMatch(addContentBox.Text, contentCheck)))
                        {
                            //EMAIL CONTENT FOR URL CHECK STARTS
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
                                addContentBox.Text = firstHalf + " " + replace + " " +secondHalf;
                                urlQuarantineList uList = urlQuarantineList.Instance();
                                Url u = new Url(addIdBox.Text, url);
                                uList.addUrl(u);
                            }//EMAIL CONTENT FOR URL CHECK ENDS
                                MsgList list = MsgList.Instance();
                                Message email = new Message(addIdBox.Text, addSenderBox.Text, addSubjectBox.Text, addContentBox.Text);
                                list.addMessage(email);
                                statusLbl.Text = "Message Added!";
                                addContentBox.Text = "";
                                addIdBox.Text = "";
                                addSenderBox.Text = "";
                                addSubjectBox.Text = "";
                        }//EMAIL CONTENT FOR NORMAL EMAIL CHECK ENDS
                        else
                        {//EMAIL CONTENT CHECK FOR INVALID ELSE STARTS
                            statusLbl.Text = "invalid content.";
                            addContentBox.Text = "";
                        }//EMAIL CONTENT CHECK FOR INVALID ELSE ENDS

                    }//EMAIL SUBJECT FOR NORMAL EMAIL CHECK IF ENDS
                    //EMAIL SUBJECT CHECK FOR SIR EMAIL IF STARTS
                    if((addSubjectBox.Text!="") && (Regex.IsMatch(addSubjectBox.Text, sirCheck)))
                    {   
                        //EMAIL CONTENT FOR SIR CHECK STARTS     
                        if(addContentBox.Text!="")
                        {
                            string code = addContentBox.Text.Substring(0, 8);
                            string nOI = addContentBox.Text.Substring(9);
                            SirList sirList = SirList.Instance();
                            Sir sir = new Sir(addIdBox.Text, addSenderBox.Text, code, nOI, addSubjectBox.Text);
                            sirList.addSir(sir);
                            statusLbl.Text = "SIR " + code + " Added";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
                            addSenderBox.Text = "";
                            addSubjectBox.Text = "";
                        }//EMAIL CONTENT FOR SIR CHECK ENDS
                    }//EMAIL SUBJECT CHECK FOR SIR EMAIL IF ENDS
                    else//SUBJECT CHECK ELSE STARTS
                    {
                        statusLbl.Text = "Subject invalid, please ensure 20 characters or less, or enter valid SIR dd/mm/yy";
                        addSubjectBox.Text = "";
                        addContentBox.Text = "";
                    }//SUBJECT CHECK ELSE ENDS
                    //EMAIL SUBJECT CHECK FOR SIR EMAIL ENDS
                }//EMAIL SENDER CHECK IF ENDS
                else
                {
                    statusLbl.Text = "Sender Invalid, please enter valid email";
                    addSenderBox.Text = "";
                    addSubjectBox.Text = "";
                    addContentBox.Text = "";
                }//EMAIL SENDER CHECK ELSE ENDS
            }//EMAIL ID CHECK IF ENDS
            //=====================EMAIL MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            //=====================SMS MESSAGE VADLIDATION STARTS==================
            //=====================================================================
            if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, sAccepted))))
            {

            }
            //=====================SMS MESSAGE VADLIDATION ENDS====================
            //=====================================================================

            //=====================TWEET MESSAGE VADLIDATION STARTS================
            //=====================================================================

            //=====================TWEET MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            else//ID CHECK ELSE STARTS
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
