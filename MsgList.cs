//The MsgList class uses the singleton pattern to hold a List of Message objects that can be globally accessed and altered from all pages in the application.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class MsgList
    {

        private static MsgList instance;
        private List<Message> messageList = null;
 
        private static readonly object syncRoot = new object();
        //Constructor for class, creates new list if no instance is currently present.
        public MsgList()
        {
            if (messageList == null)
            {
                messageList = new List<Message>();
            }
        }

        //Checks to see if any list instance is currently active. If active instance is present, returns instance.
        public static MsgList Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new MsgList();
                }
            }
            return instance;
        }
        //Method for adding new messages to the list.
        public void addMessage(Message m)
        {
            messageList.Add(m);
        }
        //method for showing message id from list index(i)
        public string showID(int i)
        {
            return messageList[i].id;
        }
        //Method for showing message sender from list index(i)
        public string showSender(int i)
        {
            return messageList[i].sender;
        }
        //Method for showing message subject from list index(i)
        public string showSub(int i)
        {
            return messageList[i].subject;
        }
        //Method for showing message subject from list index(i)
        public string showCon(int i)
        {
            return messageList[i].content;
        }
        //Method to return size of list at current state.
        public int getSize()
        {
            return messageList.Count;
        }
        //Method to return message object at list index(i)
        public Message getMessage(int i)
        {
            return messageList[i];
        }
    }
    //MESSAGE OBJECT.
    class Message
    {
        public string id { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string content { get; set; }

        internal MsgList MsgList
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Message(string i, string sen, string sub, string con)
        {
            this.id = i;
            this.sender = sen;
            this.subject = sub;
            this.content = con;
        }
    }

}
