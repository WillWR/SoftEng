using System;
using System.Collections.Generic;
using System.IO;
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
            //===================EMAIL MESSAGE VALIDATION STARTS===================
            //=====================================================================
            //EMAIL MESSAGE ID CHECKER STARTS
            if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, eAccepted))))
            {
                String emailCheck = @"[a-zA-Z._]+[@]+[a-zA-Z]+[.]+[a-zA-Z.]{2,5}$";
                //EMAIL SENDER CHECKER STARTS
                if ((addSenderBox.Text != "") && (Regex.IsMatch(addSenderBox.Text, emailCheck)))
                {
                    String subjectCheck = @"[a-zA-Z0-9.*]{0,20}$";
                    String sirCheck = @"([S]+[I]+[R]+[ ]+(([0-2]+[0-9])|([3]+[0-1]))+[\/]+(([0]+[0-9])|([1]+[0-2]))+[\/]+(([0]+[0-9])|([1-2]+[0-9])))";
                    //EMAIL SUBJECT CHECKER FOR SIR EMAIL IF STARTS
                    if ((addSubjectBox.Text != "") && (Regex.IsMatch(addSubjectBox.Text, sirCheck)))
                    {
                        //EMAIL CONTENT FOR SIR CHECKEr STARTS     
                        if (addContentBox.Text != "")
                        {
                            string code = addContentBox.Text.Substring(0, 8);
                            string nOI = addContentBox.Text.Substring(9);
                            SirList sirList = SirList.Instance();
                            Sir sir = new Sir(addIdBox.Text, addSenderBox.Text, code, nOI, addSubjectBox.Text, addContentBox.Text);
                            sirList.addSir(sir);
                            statusLbl.Text = "SIR " + code + " Added";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
                            addSenderBox.Text = "";
                            addSubjectBox.Text = "";
                        }//EMAIL CONTENT FOR SIR CHECKER ENDS
                        else
                        {
                            statusLbl.Text = "Content empty, please enter SIR code xx-xx-xx followed by a space then the nature of the incident";
                        }

                    }//EMAIL SUBJECT CHECKER FOR SIR EMAIL IF ENDS
                    //EMAIL SUBJECT FOR NORMAL EMAIL CHECKER STARTS
                    else if ((addSubjectBox.Text !="") && (Regex.IsMatch(addSubjectBox.Text, subjectCheck)))
                    {
                        String contentCheck = @"(.*?){0,1028}$";
                        //EMAIL CONTENT FOR NORMAL EMAIL CHECKER STARTS
                        if((addContentBox.Text!="") &&(Regex.IsMatch(addContentBox.Text, contentCheck)))
                        {
                            //EMAIL CONTENT FOR URL CHECKER STARTS
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
                            }//EMAIL CONTENT FOR URL CHECKER ENDS
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
                        }//EMAIL CONTENT CHECKER FOR INVALID ELSE ENDS

                    }//EMAIL SUBJECT FOR NORMAL EMAIL CHECKER IF ENDS
                    else//SUBJECT CHECKER ELSE STARTS
                    {
                        statusLbl.Text = "Subject invalid, please ensure 20 characters or less, or enter valid SIR dd/mm/yy";
                        addSubjectBox.Text = "";
                        addContentBox.Text = "";
                    }//SUBJECT CHECKER ELSE ENDS
                    //EMAIL SUBJECT CHECKER FOR SIR EMAIL ENDS
                }//EMAIL SENDER CHECKER IF ENDS
                else
                {
                    statusLbl.Text = "Sender Invalid, please enter valid email";
                    addSenderBox.Text = "";
                    addSubjectBox.Text = "";
                    addContentBox.Text = "";
                }//EMAIL SENDER CHECKER ELSE ENDS
            }//EMAIL ID CHECKER IF ENDS
            //=====================EMAIL MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            //=====================SMS MESSAGE VADLIDATION STARTS==================
            //=====================================================================

            //SMS ID CHECKER STARTS
            else if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, sAccepted))))
            {

                statusLbl.Text = "SMS detected, subject not required.";
                string senderNo = @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$";
                //SMS SENDER CHECKER START
                if ((addSenderBox.Text != "")&&(Regex.IsMatch(addSenderBox.Text, senderNo)))
                {
                    string txtContent = @"(.*?){0,140}";
                    //SMS CONTENT CHECKER START
                    if((addContentBox.Text != "")&&(Regex.IsMatch(addContentBox.Text, txtContent)))
                    {
                        String file = @"textwords.csv";
                        //TRY BLOCK START
                        try
                        {  
                            var reader = new StreamReader(File.OpenRead(file));
                            List<String> rows = new List<string>();
                            List<String> abvs = new List<string>();
                            List<String> extends = new List<string>();
                            //WHILE LOOP TO READ TEXTWORD FILE START
                            while(!reader.EndOfStream)
                            {
                                rows.Add(reader.ReadLine());
                            }//WHILE LOOP TO READ TEXTWORD FILE END
                            String replace;
                            //FOREACH FOR FILE ROW TO LIST START
                            foreach(string r in rows)
                            {
                                abvs.Add(r.Substring(0, r.IndexOf(",")));
                                extends.Add(r.Substring(r.IndexOf(",")+1));
                            }//FOREACH FOR FILE ROW TO LIST END
                            //FOR LOOP TO CHECK MESSAGE CONTENT AGAINST ABREVIATIONS START
                            for(int i=0; i<abvs.Count;i++)
                            {
                                replace = extends[i];
                                //IF CONDITION FOR ABREVIATION EXPANSION STARTS
                                if (addContentBox.Text.Contains(abvs[i]))
                                {
                                    string temp = " " + addContentBox.Text + " ";
                                    String secondHalf;
                                    String firstHalf;
                                    string del = abvs[i];
                                    int j = temp.LastIndexOf(del);
                                    firstHalf = temp.Substring(0, (j+del.Length));
                                    secondHalf = temp.Substring(j+del.Length+1);
                                    temp = firstHalf + "< " + replace + " >" + secondHalf;
                                    addContentBox.Text = temp;
                                }// IF CONDITION FOR ABREVIATION EXPANSION ENDS
                            }//FOR LOOP TO CHECK MESSAGE CONTENT AGAINST ABREVIATIONS END
                            //IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 STARTS
                            if (addContentBox.Text.Length<140)
                            {
                                MsgList list = MsgList.Instance();
                                Message sms = new Message(addIdBox.Text, addSenderBox.Text, addSubjectBox.Text, addContentBox.Text);
                                list.addMessage(sms);
                                statusLbl.Text = "Message Added!";
                                addContentBox.Text = "";
                                addIdBox.Text = "";
                                addSenderBox.Text = "";
                                addSubjectBox.Text = "";
                            }//IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 ENDS
                        }//TRY BLOCK END
                        //CATCH BLOCK START
                        catch(Exception ex)
                        {
                            Console.WriteLine("File not found");
                        }//CATCH BLOCK END
                    }//SMS CONTENT CHECKER END
                    else
                    {//SMS CONTENT CHECKER FOR INVALID ELSE STARTS
                        statusLbl.Text = "invalid content.";
                        addContentBox.Text = "";
                    }//SMS CONTENT CHECKER FOR INVALID ELSE ENDS
                }//SMS SENDER CHECKER ENDs
                else
                {
                    statusLbl.Text = "Sender Invalid, please enter valid number";
                    addSenderBox.Text = "";
                    addSubjectBox.Text = "";
                    addContentBox.Text = "";
                }//SMS SENDER CHECKER ELSE ENDS
            }//SMS ID CHECKER ENDS
            //=====================SMS MESSAGE VADLIDATION ENDS====================
            //=====================================================================

            //=====================TWEET MESSAGE VADLIDATION STARTS================
            //=====================================================================
            //TWEET ID CHECKER STARTS
            else if ((addIdBox.Text != "") && (Regex.IsMatch(addIdBox.Text, tAccepted)))
            {
                string senderID = @"(@[[:ascii:]]{0,15})";
                //TWEET SENDER CHECKER STARTS
                if((addSenderBox.Text!="")&&(Regex.IsMatch(addSenderBox.Text, senderID)))
                {
                    string txtContent = @"(.*?){0,140}";
                    //TWEET CONTENT CHECKER STARTS
                    if((addContentBox.Text!="")&&(Regex.IsMatch(addContentBox.Text,txtContent)))
                    {
                        String file = @"textwords.csv";
                        //TRY BLOCK START
                        try
                        { 
                            var reader = new StreamReader(File.OpenRead(file));
                            List<String> rows = new List<string>();
                            List<String> abvs = new List<string>();
                            List<String> extends = new List<string>();
                            //WHILE LOOP TO READ TEXTWORD FILE START
                            while (!reader.EndOfStream)
                            {
                                rows.Add(reader.ReadLine());
                            }//WHILE LOOP TO READ TEXTWORD FILE END
                            String replace;
                            //FOREACH FOR FILE ROW TO LIST START
                            foreach (string r in rows)
                            {
                                abvs.Add(r.Substring(0, r.IndexOf(",")));
                                extends.Add(r.Substring(r.IndexOf(",") + 1));
                            }//FOREACH FOR FILE ROW TO LIST END
                            //FOR LOOP TO CHECK MESSAGE CONTENT AGAINST ABREVIATIONS START
                            for (int i = 0; i < abvs.Count; i++)
                            {   
                                replace = extends[i];
                                //IF CONDITION FOR ABREVIATION EXPANSION STARTS
                                if (addContentBox.Text.Contains(abvs[i]))
                                {
                                    string temp = addContentBox.Text = " " + addContentBox.Text + " ";
                                    String secondHalf;
                                    String firstHalf;
                                    string del = abvs[i];
                                    int j = temp.LastIndexOf(del);
                                    firstHalf = temp.Substring(0, (j + del.Length-1));
                                    secondHalf = temp.Substring(j + del.Length + 1);
                                    temp = firstHalf + " <" + replace + " >" + secondHalf;
                                    addContentBox.Text = temp;
                                }// IF CONDITION FOR ABREVIATION EXPANSION ENDS
                            }//FOR LOOP TO CHECK MESSAGE CONTENT AGAINST ABREVIATIONS END    
                        }//TRY BLOCK END
                        //CATCH BLOCK START
                        catch (Exception ex)
                        {
                            Console.WriteLine("File not found");
                        }//CATCH BLOCK END
                        //TWEET HASHTAG CHECKER STARTS
                        string hashCheck = @"(#([a-zA-Z0-9]+))";
                        if(Regex.IsMatch(addContentBox.Text, hashCheck))
                        {
                            HashTags t = new HashTags();
                            string copy = addContentBox.Text;
                            //WHILE LOOP TO GATHER ALL HASHTAGS STARTS
                            while(Regex.IsMatch(copy, hashCheck))
                            {
                                string first;
                                string tag;
                                string del = "#";

                                first = copy.Substring(copy.IndexOf(del));
                                //IF CONDITION FOR A SPACE BEING LEFT IN THE STRING STARTS
                                if(first.Contains(" "))
                                {
                                    tag = first.Substring(0, first.IndexOf(" "));
                                }//IF CONDITION FOR A SPACE BEING LEFT IN THE STRING ENDS
                                //ELSE CONDITION FOR A SPACE BEING LEFT IN THE STRING STARTS
                                else
                                {
                                    tag = first.Substring(0);
                                }//ELSE CONDITION FOR A SPACE BEING LEFT IN THE STRING ENDS
                                Tags hT = new Tags(addIdBox.Text,tag);
                                t.addTag(hT);
                                copy = first.Substring(first.IndexOf(del)+1);
                            }//WHILE LOOP TO GATHER ALL HASHTAGS ENDS
                        }//TWEET HASHTAG CHECKER ENDS
                        //IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 STARTS
                        if (addContentBox.Text.Length < 140)
                        {
                            MsgList list = MsgList.Instance();
                            Message sms = new Message(addIdBox.Text, addSenderBox.Text, addSubjectBox.Text, addContentBox.Text);
                            list.addMessage(sms);
                            statusLbl.Text = "Message Added!";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
                            addSenderBox.Text = "";
                            addSubjectBox.Text = "";
                            HashTags t = new HashTags();                      
                        }//IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 ENDS

                    }//TWEET CONTENT CHECKER IF ENDS
                    else
                    {
                        statusLbl.Text = "Content Empty, please enter your tweet";
                    }

                }//TWEET SENDER CHECKER ENDS
                else
                {
                    statusLbl.Text = "Please enter valid twitter ID, @ followed by 15 characters";
                    addSenderBox.Text = "";
                }
            }//TWEET ID CHECKER ENDS
            //=====================TWEET MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            else//ID CHECKER ELSE STARTS
            {
                statusLbl.Text = "Message Id invalid. Please use S,E or T followed by 9 numbers.";
                addIdBox.Text = "";
                addSenderBox.Text = "";
                addSubjectBox.Text = "";
                addContentBox.Text = "";
            }//ID CHECKER ELSE ENDS
        }
    }
}
