using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class Message
    {  
        public string id { get; set; }
        public string subject { get; set; }
        public string content { get; set; }   

        public Message(string i, string s, string c)
        {
            this.id = i;
            this.subject = s;
            this.content = c;
        }
    }
    
}
