using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Uye_Takip_Sistemi
{
    public partial class MainForm : Form
    {

        private Form activeForm;
        public MainForm()
        {
            InitializeComponent();
        }

        SqlCommand command;
        SqlConnection connection;
        SqlDataReader dataReader;

        readonly string connectionString = "Data Source=sql.dhs.com.tr\\MSSQLSERVER2019;Initial Catalog=altinba1_alperen;User ID=altinba1_alpy;Password=Alpy1453*";
        readonly string fetchUsers = "SELECT * FROM Students WHERE student_identity_number=@identityNumber ";

        private string data;

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                textBox1.Text = port;
            }

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
            timer1.Start();
        }

        private void displaydata(object sender, EventArgs e)
        {
            string localData = data;
            localData = localData.Substring(0, localData.Length - 1);

            connection = new SqlConnection(connectionString);
            command = new SqlCommand("SELECT * From Students WHERE student_identity_number=@identityNumber", connection);
            command.Parameters.AddWithValue("@identityNumber", localData);

            connection.Open();
            dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                MessageBox.Show("Eşleşmesi başarılı");
            }
            else
            {
                MessageBox.Show("Veri Gönderilemedi");
            }
            connection.Close();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displaydata));
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                MessageBox.Show("Serial connection is on.Please be careful.This situation can be dangerous.");
                serialPort1.Close();
                connection.Close();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
            panel10.Visible = false;
            panel11.Visible = false;
            panel12.Visible = false;

            OpenChildForm(new ProfileForm(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel10.Visible = true;
            panel11.Visible = false;
            panel12.Visible = false;
            OpenChildForm(new DataForm(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = true;
            panel12.Visible = false;
            OpenChildForm(new SituationForm(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel12.Visible = true;
            OpenChildForm(new SettingsForm(), sender);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else { WindowState = FormWindowState.Normal; }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel12.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            button9.Visible = false;
            button10.Enabled = true;
            button10.Visible = true;


            try
            {
                serialPort1.BaudRate = 9600;
                serialPort1.PortName = textBox1.Text;
                serialPort1.Open();
                serialPort1.Write("3");

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR. Please be careful");
                button10.Enabled = false;
                button10.Visible = false;
                button9.Enabled = true;
                button9.Visible = true;

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false;
            button10.Visible = false;
            button9.Enabled = true;
            button9.Visible = true;
            serialPort1.Close();
        }

        private void OpenChildForm(Form childform, object Btnsender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childform);
            this.panelDesktop.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
    }
}
