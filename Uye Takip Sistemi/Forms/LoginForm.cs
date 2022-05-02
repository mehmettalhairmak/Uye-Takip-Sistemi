using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Uye_Takip_Sistemi
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        SqlCommand command;
        SqlConnection connection;
        SqlDataReader dataReader;

        readonly string connectionString = "Data Source=sql.dhs.com.tr\\MSSQLSERVER2019;Initial Catalog=altinba1_alperen;User ID=altinba1_alpy;Password=Alpy1453*";
        readonly string fetchUsers = "SELECT * FROM Users WHERE username=@username AND password=@password";

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel3.Width += 5;

            if (panel3.Width >= 735)
            {
                timer1.Stop();
                panel1.Visible = false;
            }
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand(fetchUsers, connection);
            command.Parameters.AddWithValue("@username", textBox_username.Text);
            command.Parameters.AddWithValue("@password", textBox_password.Text);
            connection.Open();
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                MainForm MainForm = new MainForm();
                MainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı");
            }
            connection.Close();
        }
    }
}
