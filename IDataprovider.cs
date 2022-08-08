using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMSoftwareLib
{
    internal interface IDataprovider
    {
       bool validAtmUserInfo(ATM atm);
        int withdraw(int amount,string name);
        int deposite(int amount);
        int Availablebalance(string name);
        bool Transaction(Transaction transaction);
        int getUserID(string name);
        List<Transaction> RecenntTransactions(Transaction transaction);
       

       
    }
}
