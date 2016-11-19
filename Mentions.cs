using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class Mentions
    {
        private static Mentions instance;
        private List<TwitterIds> tid = null;

        private static readonly object syncRoot = new object();
        //constructor for class, creates new list if no instance is currently present;
        public Mentions()
        {
            if(tid == null)
            {
                tid = new List<TwitterIds>();
            }
        }
        //Checks to see if any list instance is currently active. If active instance is present, returns instance.
        public static Mentions Instance()
        {
            if(instance == null)
            {
                lock(syncRoot)
                {
                    instance = new Mentions();
                }
            }
            return instance;
        }
        //Method to add twitter id to mentions list.
        public void addTid(TwitterIds t)
        {
            tid.Add(t);
        }
        //Method to show twitter id at index i
        public string showID(int i)
        {
            return tid[i].id;
        }
        //Method to return object at instance i
        public TwitterIds getTid(int i)
        {
            return tid[i];
        }
        //Method to return number of elements in list
        public int getSize()
        {
            return tid.Count;
        }
    }
    //TWITTER ID OBJECT
    class TwitterIds
    {
        public string id { get; set; }
        
        public TwitterIds(string i)
        {
            this.id = i;
        }
    }
}
