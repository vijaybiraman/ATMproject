using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ATMSoftwareLib;

namespace WindowsFormsAppForATM
{
    public partial class RemainingBalance : Form
    {
        ATMDataProvider aTMDataProvider = null;
        public static int amount;

        public RemainingBalance()
        {
            InitializeComponent();
            aTMDataProvider = new ATMDataProvider(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
          
            int balance = aTMDataProvider.Availablebalance(UserForm.username);
            int balance1 = UserAccountDetails.deposite - UserAccountDetails.withdraw;
            int total = balance + balance1;
            Transaction transaction = new Transaction();
            transaction.username=UserForm.username;
            transaction.withdraw = UserAccountDetails.withdraw;
            UserAccountDetails.withdraw = 0;
            transaction.deposite = UserAccountDetails.deposite;
            UserAccountDetails.deposite = 0;
            transaction.balance = total ;

            txtAccountHolder.Text = UserForm.username;
            string amt = total.ToString();
            txtbalance.Text = amt;
            bool i = aTMDataProvider.Transaction(transaction);
            if(balance1==0)
            {
                MessageBox.Show("not right");
            }
            if (i==true)
            {
                MessageBox.Show("record inserted");
            }
            else
            {
                MessageBox.Show("not inserted");
            }
        }
    }
}
