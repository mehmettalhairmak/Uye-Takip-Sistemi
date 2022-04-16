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
    public partial class FormAnaMenu : Form
    {
        public FormAnaMenu()
        {
            InitializeComponent();

        }

        readonly DatabaseServices databaseServices = new DatabaseServices();

        public void fetchData()
        {
            dataGridView1.DataSource = databaseServices.fetchData();

            dataGridView1.Columns["student_id"].HeaderText = "ID";
            dataGridView1.Columns["student_name"].HeaderText = "İsim";
            dataGridView1.Columns["student_surname"].HeaderText = "Soyisim";
            dataGridView1.Columns["student_number"].HeaderText = "Numara";
            dataGridView1.Columns["student_email"].HeaderText = "E-Posta";
            dataGridView1.Columns["student_phone_number"].HeaderText = "Telefon Numarası";
            dataGridView1.Columns["student_identity_number"].HeaderText = "Kimlik Numarası";
            dataGridView1.Columns["student_class"].HeaderText = "Sınıf";
            dataGridView1.Columns["student_education_level"].HeaderText = "Eğitim Seviyesi";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button_goster_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
        }
    }
}
