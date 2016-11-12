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
        //Constructor for class, creates new list if no instance is currently present;
        public HashTags()
        {
            if(tagList == null)
            {
                tagList = new List<Tags>();
            }
        }
        //Checks to see if any list instance is currently active. if active instance is present, returns instance.
        public static HashTags Instance()
        {
            if(instance == null)
            {
                lock(syncRoot)
                {
                    instance = new HashTags();
                }
            }
            return instance;
        }

        public void addTag(Tags t)
        {
            tagList.Add(t);
        }

        public string showID(int i)
        {
            return tagList[i].id;
        }

        public string showTag(int i)
        {
            return tagList[i].tag;
        }

        public int getSize()
        {
            return tagList.Count;
        }

        public Tags getTag(int i)
        {
            return tagList[i];
        }
    }
    class Tags
    {
        public string id { get; set; }
        public string tag { get; set; }    

        public Tags(string id, string t)
        {
            this.id = id;
            this.tag = t;
        }
    }


}

