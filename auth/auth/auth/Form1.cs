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
using System.Data.SqlClient;

namespace auth
{
    public partial class Form1 : Form
    {
        string connectionString;
        string Login;
        string Password;

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["auth.Properties.Settings._16is23ConnectionString"].ConnectionString;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login = textBox1.Text.Trim();
            Password = textBox2.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Int32 countUser = 0;
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT COUNT(*) FROM [dbo].[auth] WHERE Login = @Login and Password = @Password";
                command.Connection = connection;

                command.Parameters.Add("@Login", SqlDbType.VarChar);
                command.Parameters["@Login"].Value = Login;

                command.Parameters.Add("@Password", SqlDbType.VarChar);
                command.Parameters["@Password"].Value = Password;

                try
                {
                    connection.Open();
                    countUser = (Int32)command.ExecuteScalar();
                    if (countUser == 1)
                    {
                        MessageBox.Show("Авторизация успешна");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка авторизации");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

    }
}
