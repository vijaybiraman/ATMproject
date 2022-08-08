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
    public partial class UserAccountDetails : Form
    {
        ATMDataProvider atMDataProvider = null;
        public static int withdraw;
        public static int deposite;
        public UserAccountDetails()
        {
            InitializeComponent();
            atMDataProvider = new ATMDataProvider(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        private void UserAccountDetails_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void AvaialbleBalance_Click(object sender, EventArgs e)
        {
            RemainingBalance remaining = new RemainingBalance();
            remaining.ShowDialog();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int amt = Convert.ToInt32(txtwithdrawamount.Text);
            withdraw = amt;
            int res = atMDataProvider.withdraw(amt,UserForm.username);
            if(res==0)
            {
                MessageBox.Show("Your account balance is low or you cannot withdraw more than 20000 or your daily limit is over");
            }
            else
            {
                MessageBox.Show("Amount withdraw successfully");
            }
           
        }

        private void btnSubmit1_Click(object sender, EventArgs e)
        {
            int amt= Convert.ToInt32(txtDepositeAmount.Text);
            deposite = amt;
            int res= atMDataProvider.deposite(amt);
            
            if (res == 0)
            {
                MessageBox.Show("Your daily limit is over");
            }
            else
            {
                MessageBox.Show("Amount Deposited sucessfully");
            }
        }

        private void btnrecenttransaction_Click(object sender, EventArgs e)
        {
            RecentTransactions tr = new RecentTransactions();
            tr.ShowDialog();
        }
    }
}
