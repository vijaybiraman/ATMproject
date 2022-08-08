using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSoftwareLib
{
    public class ATM
    {
      public int userid { get; set; }
      public  string username { get; set; }
      public  int DebitCardNumber { get; set; }
      public int Pin { get; set; }

        public static int AccountBalance = 50000;

    }
}
