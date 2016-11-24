using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class urlQuarantineList
    {
        private static urlQuarantineList instance;
        private List<Url> urlList = null;

        private static readonly object syncRoot = new object();
        //Constructor for class, creates new list if no instance is currently present.
        public urlQuarantineList()
        {
            if (urlList == null)
            {
                urlList = new List<Url>();
            }
        }

        //Checks to see if any list instance is currently active. If active instance is present, returns instance.
        public static urlQuarantineList Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new urlQuarantineList();
                }
            }
            return instance;
        }
        //Method for adding new messages to the list.
        public void addUrl(Url m)
        {
            urlList.Add(m);
        }
        //method for showing message id from list index(i)
        public string showID(int i)
        {
            return urlList[i].id;
        }
        //method for showing url from list index(i)
        public string showUrl(int i)
        {
            return urlList[i].QuarantinedUrl;
        }
        //method for returning size of the url list.
        public int getSize()
        {
            return urlList.Count;
        }
        //method for returning the url object at index (i)
        public Url getUrl(int i)
        {
            return urlList[i];
        }
    }
    //URL OBJECT
    class Url
    {
        public string id { get; set; }
        public string QuarantinedUrl { get; set; }

        internal urlQuarantineList urlQuarantineList
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Url(string i, string u)
        {
            this.id = i;
            this.QuarantinedUrl = u;
        }
    }
}
