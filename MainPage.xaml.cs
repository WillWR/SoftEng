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

        private void manualBtn_Click(object sender, RoutedEventArgs e)
        {
            ManualForm mF = new ManualForm();
            this.NavigationService.Navigate(mF);
        }

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

        private void urlsBtn_Click(object sender, RoutedEventArgs e)
        {
            Quarantine q = new Quarantine();
            this.NavigationService.Navigate(q);
        }

        private void addtoJsonBtn_Click(object sender, RoutedEventArgs e)
        {
            MsgList list = MsgList.Instance();
            int length = list.getSize();
            StreamWriter writeMessages = new StreamWriter(@"jsonMessages.txt");
            for (int i=0; i<length; i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(list.getMessage(i));
                writeMessages.WriteLine(json);     
            }
            writeMessages.Close();

            StreamWriter writeUrls = new StreamWriter(@"jsonUrls.txt");
            urlQuarantineList u = urlQuarantineList.Instance();
            for(int i=0;i<u.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(u.getUrl(i));
                writeUrls.WriteLine(json);
            }
            writeUrls.Close();

            StreamWriter writeTags = new StreamWriter(@"jsonTags.txt");
            HashTags hashTag = HashTags.Instance();
            for (int i = 0; i < hashTag.getSize(); i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(hashTag.getTag(i));
                writeTags.WriteLine(json);
            }
            writeTags.Close();

            StreamWriter writeMen = new StreamWriter(@"jsonMen.txt");
            Mentions men = Mentions.Instance();
            for(int i=0;i<men.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(men.getTid(i));
                writeMen.WriteLine(json);
            }
            writeMen.Close();

            StreamWriter writeSir = new StreamWriter(@"jsonSir.txt");
            SirList sir = SirList.Instance();
            for(int i = 0; i<sir.getSize();i++)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(sir.getSir(i));
                writeSir.WriteLine(json);
            }
            writeSir.Close();
        }

        private void autoBtn_Click(object sender, RoutedEventArgs e)
        {
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

        }

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

        private void viewTrendsBtn1_Click(object sender, RoutedEventArgs e)
        {
            DisplayTrends dT = new DisplayTrends();
            this.NavigationService.Navigate(dT);
        }

        private void sirBtn_Click(object sender, RoutedEventArgs e)
        {
            SirView sR = new SirView();
            this.NavigationService.Navigate(sR);
        }
    }
}
