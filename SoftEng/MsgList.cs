﻿//The MsgList class uses the singleton pattern to hold a List of Message objects that can be globally accessed and altered from all pages in the application.
using System;
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
    }
}