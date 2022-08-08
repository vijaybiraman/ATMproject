using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSoftwareLib
{
    public class Transaction
    {
        public int transid { get; set; }
        public string username { get; set; }
        public int withdraw { get; set; }
        public  int deposite { get; set; }
        public int balance { get; set; }
        public int userid { get; set; }
    }
}
