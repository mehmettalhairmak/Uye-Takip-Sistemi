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

namespace Uye_Takip_Sistemi
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string data;

        private void Form2_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                textBox1.Text = port;
            }
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

            }
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            data = serialPort1.ReadLine();
            this.Invoke(new EventHandler(displaydata));
        }

        private void displaydata(object sender, EventArgs e)
        {
            textBox2.Text = data;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                MessageBox.Show("Serial connection is on.Please be careful.This situation can be dangerous.");
                serialPort1.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormAnaMenu anaform = new FormAnaMenu();
            anaform.Show();
            this.Hide();
        }
    }
}
