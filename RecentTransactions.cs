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
    public partial class RecentTransactions : Form
    {
        ATMDataProvider aTMDataProvider = null;
        public RecentTransactions()
        {
            InitializeComponent();
            aTMDataProvider = new ATMDataProvider(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtAccholder.Text = UserForm.username;
           int i= aTMDataProvider.getUserID(UserForm.username);
            txtUserId.Text = i.ToString();
            Transaction transaction = new Transaction();
            transaction.transid = 0;
            transaction.username = txtAccholder.Text;
            transaction.deposite = 0;
            transaction.withdraw = 0;
            transaction.balance = 0;
            transaction.userid = i;
            List<Transaction> list = aTMDataProvider.RecenntTransactions(transaction);
            if(list!=null)
            {
                GridView.DataSource = list;
            }
        }
    }
}
