using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ATMSoftwareLib
{
    
    public class ATMDataProvider : IDataprovider
    {
        SqlCommand command = null;
        SqlConnection connection = null;
        SqlDataReader reader = null;
       static int count = 0;
       static int cnt = 0;
        public ATMDataProvider(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public int Availablebalance(string name)
        {
            try
            {
                string query = "select top 1 balance from TRANSACTIONS where username=@name order by transid desc";
                command=new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@name",name);
                if(connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();
                int amount = 0;
                while(reader.Read())
                {
                    amount = Convert.ToInt32(reader["balance"].ToString());
                }
                return amount;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(connection.State==ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int deposite(int amount)
        {
            if (count < 3)
            {
                count++;
                return amount;
                
            }
            else
            {
                return 0;
            }
        }

        public bool validAtmUserInfo(ATM atm)
        {
            try
            {
                string query = "select username,debitcard,pincode from ATM where username=@username and debitcard=@debitcard and pincode=@pincode";
                command=new SqlCommand(query,connection);
                command.Parameters.AddWithValue("@username", atm.username);
                command.Parameters.AddWithValue("@debitcard", atm.DebitCardNumber);
                command.Parameters.AddWithValue("@pincode", atm.Pin);
                if (connection.State==ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader=command.ExecuteReader();
                if(reader.HasRows)
                {
                   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw ex ;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int withdraw(int amount,string name)
        {
            if (cnt < 3 && amount<=20000)
            {
                int cash = Availablebalance(name);
                if (amount < cash)
                {
                    count++;
                    return amount;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Transaction(Transaction transaction)
        {
            try
            {
                int j = getUserID(transaction.username);
                string query = "insert into transactions(username,withdraw,deposite,balance,userid)values(@username, @withdraw, @deposite,@balance,@userid)";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username",transaction.username);
                command.Parameters.AddWithValue("@withdraw",transaction.withdraw);
                command.Parameters.AddWithValue("@deposite",transaction.deposite);
                command.Parameters.AddWithValue("@balance", transaction.balance);
                command.Parameters.AddWithValue("@userid",j );
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                int i = command.ExecuteNonQuery();
                if(i>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public int getUserID(string name)
        {
            try
            {
                string query = "select userid from atm where username=@username";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", name);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                  reader=command.ExecuteReader();
                int userid = 0;
                while(reader.Read())
                {
                    userid = Convert.ToInt32(reader["userid"].ToString());
                }
                return userid;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public List<Transaction> RecenntTransactions(Transaction transaction)
        {
            try
            {
                string query = "select top 3 * from TRANSACTIONS where userid=@userid and username=@username order by transid desc ";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username",transaction.username);
                command.Parameters.AddWithValue("@userid",transaction.userid);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                reader = command.ExecuteReader();
                List<Transaction> list = new List<Transaction>();
                while (reader.Read())
                {
                   Transaction transaction1 = new Transaction();
                   transaction1.transid = Convert.ToInt32(reader["transid"].ToString());
                   transaction1.username = reader["username"].ToString();
                   transaction1.withdraw = Convert.ToInt32(reader["withdraw"].ToString());
                   transaction1.deposite = Convert.ToInt32(reader["deposite"].ToString());
                    transaction1.balance= Convert.ToInt32(reader["balance"].ToString());
                    transaction1.userid = Convert.ToInt32(reader["userid"].ToString());
                    list.Add(transaction1);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
