using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
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
