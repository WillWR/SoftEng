using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class HashTags
    {
        private static HashTags instance;
        private List<Tags> tagList = null;

        private static readonly object syncRoot = new object();
        //Constructor for class, creates new list if no instance is currently present.
        public HashTags()
        {
            if (tagList == null)
            {
                tagList = new List<Tags>();
            }
        }
        //Checks to see if any list instance is currently active. If active instance is present, returns instance.
        public static HashTags Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new HashTags();
                }
            }
            return instance;
        }
        //Method for adding new serious incident reports to the list.
        public void addTag(Tags m)
        {
            tagList.Add(m);
        }
        //method for showing serious incident reports id from list index(i)
        public string showTags(int i)
        {
            return tagList[i].tag;
        }

        public string ShowID(int i)
        {
            return tagList[i].id;
        }
        public int getSize()
        {
            return tagList.Count;
        }

    }

    class Tags
    {
        public string tag { get; set; }
        public string id { get; set; }

        public Tags(string id, string t)
        {
            this.id = id;
            this.tag = t;
        }
    }


}

