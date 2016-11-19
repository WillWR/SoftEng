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
            addContentBox.Text = "Sen: Sub: Con:";
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
                String copy = addContentBox.Text;
                String emailCheck = @"[a-zA-Z._]+[@]+[a-zA-Z]+[.]+[a-zA-Z.]{2,5}$";
                String subCheck = @"([[:ascii:]]){0,20}$";
                String contentCheck = @"(.*?){0,1028}$";
                String sirCheck = @"([S]+[I]+[R]+[ ]+(([0-2]+[0-9])|([3]+[0-1]))+[\/]+(([0]+[0-9])|([1]+[0-2]))+[\/]+(([0]+[0-9])|([1-2]+[0-9])))";
                int length = (copy.IndexOf("Con:") - copy.IndexOf("Sub:"));
                String mSender = copy.Substring(copy.IndexOf("Sen:") + 4, copy.IndexOf("Sub:") - 5);
                String mSubject = copy.Substring(copy.IndexOf("Sub:") + 4, length - 4);
                String mContent = addContentBox.Text.Substring(addContentBox.Text.IndexOf("Con:") + 4);
                //EMAIL SENDER CHECKER STARTS
                if ((addContentBox.Text != "") && (Regex.IsMatch(mSender, emailCheck)))
                {
                    //EMAIL SUBJECT CHECKER STARTS(SIR)
                    if (Regex.IsMatch(mSubject, sirCheck))
                    {
                        //EMAIL CONTENT FOR URL CHECKER STARTS
                        if (mContent.Contains("http://"))
                        {
                            string temp = " " + mContent + " ";
                            String secondHalf;
                            String firstHalf;
                            String replace = "<URL Quanantined>";
                            string del = "http://";
                            int i = temp.IndexOf(del);
                            string tempCon = temp.Remove(0, i);
                            String url = tempCon.Substring(0, tempCon.IndexOf(" "));
                            secondHalf = tempCon.Remove(tempCon.IndexOf(" "));
                            firstHalf = temp.Substring(0, i - 1);
                            mContent = firstHalf + " " + replace + " " + secondHalf;
                            urlQuarantineList uList = urlQuarantineList.Instance();
                            Url u = new Url(addIdBox.Text, url);
                            uList.addUrl(u);
                        }//EMAIL CONTENT FOR URL CHECKER ENDS
                        length = mContent.IndexOf("//") - 9;
                        string code = mContent.Substring(0, 8);
                        string nOI = mContent.Substring(9, length);
                        String message = mContent.Substring(mContent.IndexOf("//") + 2);
                        SirList sirList = SirList.Instance();
                        Sir sir = new Sir(addIdBox.Text, mSender, code, nOI, mSubject, message);
                        sirList.addSir(sir);
                        statusLbl.Text = "SIR " + code + " Added";
                        addContentBox.Text = "";
                        addIdBox.Text = "";
                    }//EMAIL SUBJECT CHECKER ENDS(SIR)
                    //EMAIL SUBJECT CHECKER STARTS(STANDARD EMAIL)
                    else if ((Regex.IsMatch(mSubject, subCheck)) && (Regex.IsMatch(mContent, contentCheck)))
                    {
                        //EMAIL CONTENT FOR URL CHECKER STARTS
                        if (mContent.Contains("http://"))
                        {
                            string temp = " " + mContent + " ";
                            String secondHalf;
                            String firstHalf;
                            String replace = "<URL Quanantined>";
                            string del = "http://";
                            int i = temp.LastIndexOf(del);
                            string tempCon = temp.Remove(0, i);
                            String url = tempCon.Substring(0, tempCon.IndexOf(" "));
                            secondHalf = tempCon.Remove(0, tempCon.IndexOf(" "));
                            firstHalf = temp.Substring(0, i - 1);
                            mContent = firstHalf + " " + replace + " " + secondHalf;
                            urlQuarantineList uList = urlQuarantineList.Instance();
                            Url u = new Url(addIdBox.Text, url);
                            uList.addUrl(u);
                        }//EMAIL CONTENT FOR URL CHECKER ENDS
                        if (mSubject.Length <= 20)
                        {
                            MsgList list = MsgList.Instance();
                            Message email = new Message(addIdBox.Text, mSender, mSubject, mContent);
                            list.addMessage(email);
                            statusLbl.Text = "Message Added!";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
                        }
                        else statusLbl.Text = "Subject too long, please ensure Subject is 20 characters or less";
                    }//EMAIL SUBJECT CHECKER ENDS(STANDARD EMAIL)
                    else { statusLbl.Text = "Please input valid SIR in the form of Sub:SIR dd/mm/yy Con:xx-xx-xx (Code) //Message content, Or enter a standard email with Sen:(sender address) Sub:(subject line) Con:(message content)"; }
                }//EMAIL SENDER CHECKER STARTS
                else { statusLbl.Text = "Sender email address incorrect, please ensure message is of Sen:(Email address) Sub:(subject line/SIR) Con:(message body) Format"; }
            }//EMAIL ID CHECKER IF ENDS
            //=====================EMAIL MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            //=====================SMS MESSAGE VADLIDATION STARTS==================
            //=====================================================================

            //SMS ID CHECKER STARTS
            else if ((addIdBox.Text != "") && ((Regex.IsMatch(addIdBox.Text, sAccepted))))
            {
                String copy = addContentBox.Text;
                String senderNo = @"^((\+\d{1,3}(-| )?\(?\d\)?(-| )?\d{1,5})|(\(?\d{2,6}\)?))(-| )?(\d{3,4})(-| )?(\d{4})(( x| ext)\d{1,5}){0,1}$";
                int length = (copy.IndexOf("Con:") - copy.IndexOf("Sub:"));
                String mSender = copy.Substring(copy.IndexOf("Sen:") + 4, copy.IndexOf("Con:") - 5);
                String mContent = addContentBox.Text.Substring(addContentBox.Text.IndexOf("Con:") + 4);
                //SMS SENDER CHECKER START
                if ((addContentBox.Text != "")&&(Regex.IsMatch(mSender, senderNo)))
                {
                    string txtContent = @"(.*?){0,140}";
                    //SMS CONTENT CHECKER START
                    if(Regex.IsMatch(mContent, txtContent))
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
                            reader.Close();
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
                                if (mContent.Contains(abvs[i]))
                                {
                                    string temp = " " + mContent + " ";
                                    String secondHalf;
                                    String firstHalf;
                                    string del = abvs[i];
                                    int j = temp.LastIndexOf(del);
                                    firstHalf = temp.Substring(0, (j+del.Length));
                                    secondHalf = temp.Substring(j+del.Length+1);
                                    temp = firstHalf + "< " + replace + " >" + secondHalf;
                                    mContent = temp;
                                }// IF CONDITION FOR ABREVIATION EXPANSION ENDS
                            }//FOR LOOP TO CHECK MESSAGE CONTENT AGAINST ABREVIATIONS END
                            //IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 STARTS
                            if (addContentBox.Text.Length<140)
                            {
                                MsgList list = MsgList.Instance();
                                Message sms = new Message(addIdBox.Text, mSender, "", mContent);
                                list.addMessage(sms);
                                statusLbl.Text = "Message Added!";
                                addContentBox.Text = "";
                                addIdBox.Text = "";
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
                String copy = addContentBox.Text;
                String senderID = @"(@[[:ascii:]]{0,15})";
                int length = (copy.IndexOf("Con:") - copy.IndexOf("Sub:"));
                String mSender = copy.Substring(copy.IndexOf("Sen:") + 4, copy.IndexOf("Con:") - 5);
                String mContent = addContentBox.Text.Substring(addContentBox.Text.IndexOf("Con:") + 4);
                
                //TWEET SENDER CHECKER STARTS
                if(Regex.IsMatch(mSender, senderID))
                {
                    string txtContent = @"(.*?){0,140}";
                    //TWEET CONTENT CHECKER STARTS
                    if(Regex.IsMatch(mContent,txtContent))
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
                            reader.Close();
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
                                if (mContent.Contains(abvs[i]))
                                {
                                    string temp = " " + mContent + " ";
                                    String secondHalf;
                                    String firstHalf;
                                    string del = abvs[i];
                                    int j = temp.LastIndexOf(del);
                                    firstHalf = temp.Substring(0, (j + del.Length-1));
                                    secondHalf = temp.Substring(j + del.Length + 1);
                                    temp = firstHalf + " <" + replace + " >" + secondHalf;
                                    mContent = temp;
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
                            HashTags t = HashTags.Instance();
                            copy = mContent;
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
                                Tags hT = new Tags(addIdBox.Text, tag);
                                t.addTag(hT);
                                copy = first.Substring(first.IndexOf(del)+1);
                            }//WHILE LOOP TO GATHER ALL HASHTAGS ENDS
                        }//TWEET HASHTAG CHECKER ENDS

                        //TWEET MENTIONS CHECKER STARTS
                        string mentionCheck = @"(@[[:ascii:]]{0,15})";
                        if (Regex.IsMatch(addContentBox.Text, mentionCheck))
                        {
                            Mentions m = Mentions.Instance();
                            copy = mContent;
                            //WHILE LOOP TO GATHER ALL HASHTAGS STARTS
                            while (Regex.IsMatch(copy, mentionCheck))
                            {
                                string first;
                                string ids;
                                string del = "@";

                                first = copy.Substring(copy.IndexOf(del));
                                //IF CONDITION FOR A SPACE BEING LEFT IN THE STRING STARTS
                                if (first.Contains(" "))
                                {
                                    ids = first.Substring(0, first.IndexOf(" "));
                                }//IF CONDITION FOR A SPACE BEING LEFT IN THE STRING ENDS
                                //ELSE CONDITION FOR A SPACE BEING LEFT IN THE STRING STARTS
                                else
                                {
                                    ids = first.Substring(0);
                                }//ELSE CONDITION FOR A SPACE BEING LEFT IN THE STRING ENDS
                                TwitterIds tid = new TwitterIds(ids);
                                m.addTid(tid);
                                copy = first.Substring(first.IndexOf(del) + 1);
                            }//WHILE LOOP TO GATHER ALL HASHTAGS ENDS
                        }//TWEET HASHTAG CHECKER ENDS


                        //IF CONDITION TO ADD MESSAGE IF LENGTH UNDER 140 STARTS
                        if (addContentBox.Text.Length < 140)
                        {
                            MsgList list = MsgList.Instance();
                            Message sms = new Message(addIdBox.Text, mSender, "", mContent);
                            list.addMessage(sms);
                            statusLbl.Text = "Message Added!";
                            addContentBox.Text = "";
                            addIdBox.Text = "";
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
                }
            }//TWEET ID CHECKER ENDS
            //=====================TWEET MESSAGE VADLIDATION ENDS==================
            //=====================================================================

            else//ID CHECKER ELSE STARTS
            {
                statusLbl.Text = "Message Id invalid. Please use S,E or T followed by 9 numbers.";
                addIdBox.Text = "";
                addContentBox.Text = "";
            }//ID CHECKER ELSE ENDS
        }
    }
}
