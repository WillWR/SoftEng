﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBMFS
{
    class SirList
    {
        private static SirList instance;
        private List<Sir> sirList = null;

        private static readonly object syncRoot = new object();
        //Constructor for class, creates new list if no instance is currently present.
        public SirList()
        {
            if (sirList == null)
            {
                sirList = new List<Sir>();
            }
        }
        //Checks to see if any list instance is currently active. If active instance is present, returns instance.
        public static SirList Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    instance = new SirList();
                }
            }
            return instance;
        }
        //Method for adding new serious incident reports to the list.
        public void addSir(Sir m)
        {
            sirList.Add(m);
        }
        //method for showing serious incident reports id from list index(i)
        public string showID(int i)
        {
            return sirList[i].id;
        }
        //Method for showing serious incident reports subject from list index(i)
        public string showSub(int i)
        {
            return sirList[i].subject;
        }
        //Method for showing serious incident report SIR Code from list index(i)
        public string showSirCode(int i)
        {
            return sirList[i].sirCode;
        }
        //Method for showing serious incident report nature of incident from list index(i)
        public string showNoi(int i)
        {
            return sirList[i].nOI;
        }
    }
}
