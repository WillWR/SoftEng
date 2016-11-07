using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class Message
    {
        public string id { get; set;}
        public string sender { get; set; }
        public string subject { get; set; }
        public string content { get; set; }   

        public Message(string i, string sen, string sub, string con)
        {
            this.id = i;
            this.sender = sen;
            this.subject = sen;
            this.content = con;
        }
    }
    
}
