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
using Newtonsoft;
using Newtonsoft.Json;
using System.IO;
using System.Web.Script.Serialization;

namespace NBMFS
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

        }
        //METHOD FOR MANUAL FORM BUTTON CLICK
        private void manualBtn_Click(object sender, RoutedEventArgs e)
        {
            ManualForm mF = new ManualForm();
            this.NavigationService.Navigate(mF);
        }
        //METHOD FOR VIEW MESSAGE ONE BY ONE BUTTON CLICK
        private void viewSingleBtn_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            int size = list.getSize();
            if(size!=0)
            {
                DisplayOne dO = new DisplayOne();
                this.NavigationService.Navigate(dO);
            }
            else
            {
                statusBox.Text = "No messages to view, please load from json file or input manually";
            }        
        }
        //METHOD FOR VIEW URLS BUTTON CLICK
        private void urlsBtn_Click(object sender, RoutedEventArgs e)
        {
            urlQuarantineList list = urlQuarantineList.Instance();
            if(list.getSize()!=0)
            {
                Quarantine q = new Quarantine();
                this.NavigationService.Navigate(q);
            }
            else
            {
                statusBox.Text = "No Urls to view";
            }          
        }
        //METHOD FOR ADD TO JSON FILE BUTTON CLICK
        private void addtoJsonBtn_Click(object sender, RoutedEventArgs e)
        {
            //WRITE MESSAGES TO JSON
            MsgList list = MsgList.Instance();
            int length = list.getSize();
            StreamWriter writeMessages = new StreamWriter(@"jsonMessages.txt");
            for (int i=0; i<length; i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(list.getMessage(i));
                writeMessages.WriteLine(json);     
            }
            writeMessages.Close();
            //WRITE URLS TO JSON
            StreamWriter writeUrls = new StreamWriter(@"jsonUrls.txt");
            urlQuarantineList u = urlQuarantineList.Instance();
            for(int i=0;i<u.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(u.getUrl(i));
                writeUrls.WriteLine(json);
            }
            writeUrls.Close();
            //WRITE HASH TAGS TO JSON
            StreamWriter writeTags = new StreamWriter(@"jsonTags.txt");
            HashTags hashTag = HashTags.Instance();
            for (int i = 0; i < hashTag.getSize(); i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(hashTag.getTag(i));
                writeTags.WriteLine(json);
            }
            writeTags.Close();
            //WRITE MENTIONS TO JSON
            StreamWriter writeMen = new StreamWriter(@"jsonMen.txt");
            Mentions men = Mentions.Instance();
            for(int i=0;i<men.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(men.getTid(i));
                writeMen.WriteLine(json);
            }
            writeMen.Close();
            //WRITE SIR MESSAGES TO JSON
            StreamWriter writeSir = new StreamWriter(@"jsonSir.txt");
            SirList sir = SirList.Instance();
            for(int i = 0; i<sir.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(sir.getSir(i));
                writeSir.WriteLine(json);
            }
            writeSir.Close();

            statusBox.Text = "Data writen to Json.";
        }
        //METHOD FOR LOADING FROM JSON FILE BUTTON CLICK
        private void autoBtn_Click(object sender, RoutedEventArgs e)
        {
            //LOAD MESSAGES FROM JSON
            MsgList list = MsgList.Instance();
            try
            {
                var reader = new StreamReader(File.OpenRead(@"jsonMessages.txt"));
                List<string> rows = new List<string>();
                while (!reader.EndOfStream)
                {
                    rows.Add(reader.ReadLine());
                }
                reader.Close();               
                foreach (string r in rows)
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                    Message m = new Message(values["id"], values["sender"], values["subject"], values["content"]);

                    list.addMessage(m);
                }

            }
            catch(Exception ex)
            {
                statusBox.Text = "Message file not found, Please input messages manually and ensure you write to file before closing";
            }
            //LOAD URLS FROM JSON
            urlQuarantineList u = urlQuarantineList.Instance();
            try
            {
                var reader2 = new StreamReader(File.OpenRead(@"jsonUrls.txt"));
                List<string> rows2 = new List<string>();
                while(!reader2.EndOfStream)
                {
                    rows2.Add(reader2.ReadLine());
                }
                reader2.Close();
                foreach(string r in rows2)
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                    Url url = new Url(values["id"], values["QuarantinedUrl"]);

                    u.addUrl(url);
                }

            }
            catch(Exception ex)
            {
                statusBox.Text = "Url file not found, please input messages manually and ensure you write to file before closing";
            }
            //LOAD HASH TAGS FROM JSON
            HashTags tag = HashTags.Instance();
            try
            {
                var reader3 = new StreamReader(File.OpenRead(@"jsonTags.txt"));
                List<string> rows3 = new List<string>();
                while(!reader3.EndOfStream)
                {
                    rows3.Add(reader3.ReadLine());
                }
                reader3.Close();
                foreach(string r in rows3)
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                    Tags t = new Tags(values["id"], values["tag"]);

                    tag.addTag(t);
                }
            }
            catch
            {
                statusBox.Text = "Tags file not found, please input messages manually and ensure you write to file before closing";
            }
            //LOAD MENTIONS FROM JSON
            Mentions men = Mentions.Instance();
            try
            {
                var reader4 = new StreamReader(File.OpenRead(@"jsonMen.txt"));
                List<string> rows4 = new List<string>();
                while(!reader4.EndOfStream)
                {
                    rows4.Add(reader4.ReadLine());
                }
                reader4.Close();
                foreach(string r in rows4)
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                    TwitterIds tid = new TwitterIds(values["id"]);

                    men.addTid(tid);
                }
            }
            catch(Exception ex)
            {
                statusBox.Text = "Mentions file not found, please input messages manually and ensure you write to file before closing";
            }
            //LOAD SIR MESSAGES FROM JSON
            SirList sir = SirList.Instance();
            try
            {
                var reader5 = new StreamReader(File.OpenRead(@"jsonSir.txt"));
                List<string> rows5 = new List<string>();
                while(!reader5.EndOfStream)
                {
                    rows5.Add(reader5.ReadLine());
                }
                reader5.Close();
                foreach(string r in rows5)
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(r);
                    Sir s = new Sir(values["id"], values["sender"], values["sirCode"], values["nOI"], values["subject"], values["content"]);

                    sir.addSir(s);
                }
            }
            catch(Exception ex)
            {
                statusBox.Text = "SirList file not found, please input messages manually and ensure you write to file before closing";
            }
            statusBox.Text = "Data loaded from Json.";
        }
        //METHOD FOR VIEW ALL MESSAGES BUTTON CLICK
        private void viewAllBtn_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();

            int size = list.getSize();
            if(size!=0)
            {
                DisplayAll dA = new DisplayAll();
                this.NavigationService.Navigate(dA);
            }
            else
            {
                statusBox.Text = "No messages to view, please load from json file or input manually"; 
            }   
        }
        //METHOD FOR VIEW TRENDS BUTTON CLICK
        private void viewTrendsBtn1_Click(object sender, RoutedEventArgs e)
        {
            HashTags list = HashTags.Instance();
            Mentions list2 = Mentions.Instance();
            if(list.getSize()!=0 || list2.getSize()!=0)
            {
                DisplayTrends dT = new DisplayTrends();
                this.NavigationService.Navigate(dT);
            }
            else
            {
                statusBox.Text = "No hash tags or mentions to view";
            }
            
        }
        //METHOD FOR VIEW SIR BUTTON CLICK
        private void sirBtn_Click(object sender, RoutedEventArgs e)
        {
            SirList list = SirList.Instance();
            if(list.getSize()!=0)
            {
                SirView sR = new SirView();
                this.NavigationService.Navigate(sR);
            }
            else
            {
                statusBox.Text = "No messages to view, please load from json file or input SIR manually.";
            }
            
        }
    }
}
