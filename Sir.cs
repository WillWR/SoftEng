using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class Sir
    {
        public string id { get; set; }
        public string sender { get; set; }
        public string sirCode { get; set; }
        public string nOI { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        


        public Sir(string i, string sen, string sirCode, string nOI, string sub, string con)
        {
            this.id = i;
            this.sender = sen;
            this.sirCode = sirCode;
            this.nOI = nOI;
            this.subject = sen;
            this.content = con;
        }

    }
}
