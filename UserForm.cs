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
    public partial class UserForm : Form
    {
        ATMDataProvider atMDataProvidera = null;
        public static string username;
       
        
        public UserForm()
        {          
            InitializeComponent();
            atMDataProvidera = new ATMDataProvider(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
           
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void btnSubmit_Click(object sender, EventArgs e)
        {

            string name =txtUsername.Text;
            username = txtUsername.Text;
            int cardno=Convert.ToInt32(txtDebitcard.Text);
            int pin = Convert.ToInt32(txtPin.Text);
            ATM atm = new ATM();
            atm.username = name;
            atm.DebitCardNumber = cardno;
            atm.Pin = pin;
            
            bool res = atMDataProvidera.validAtmUserInfo(atm);
            if(res==true)
            {
                UserAccountDetails userAccountDetails = new UserAccountDetails();
                userAccountDetails.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid info");
            }

        }

        private void UserForm_Load(object sender, EventArgs e)
        {

        }
    }
}
