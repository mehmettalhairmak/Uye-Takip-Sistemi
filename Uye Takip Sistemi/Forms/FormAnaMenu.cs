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
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        DataTable dataTable;
        SqlCommand command;
        SqlDataAdapter adapter;
        SqlConnection connection;

        readonly string connectionString = "Data Source=sql.dhs.com.tr\\MSSQLSERVER2019;Initial Catalog=altinba1_alperen;User ID=altinba1_alpy;Password=Alpy1453*";

        readonly string fetchDataString = "SELECT * FROM Students";

        readonly string insertDataString = "INSERT INTO Students (student_name, student_surname, student_number, student_class, " +
            "student_email, student_phone_number, student_identity_number, student_education_level) " +
            "values (@name, @surname, @number, @class, @email, @phoneNumber, @identityNumber, @educationLevel)";

        readonly string updateDataString = "UPDATE Students SET student_name = @name, student_surname = @surname, student_number = @number, " +
            "student_class = @class, student_email = @email, student_phone_number = @phoneNumber, student_identity_number = @identityNumber, " +
            "student_education_level = @educationLevel WHERE student_id = @id";

        readonly string deleteDataString = "DELETE FROM Students WHERE student_id = @id";
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void fetchData()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            adapter = new SqlDataAdapter(fetchDataString, connection);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            connection.Close();

            dataGridView.DataSource = dataTable;

            dataGridView.Columns["student_id"].HeaderText = "ID";
            dataGridView.Columns["student_name"].HeaderText = "İsim";
            dataGridView.Columns["student_surname"].HeaderText = "Soyisim";
            dataGridView.Columns["student_number"].HeaderText = "Numara";
            dataGridView.Columns["student_class"].HeaderText = "Sınıf";
            dataGridView.Columns["student_email"].HeaderText = "E-Posta";
            dataGridView.Columns["student_phone_number"].HeaderText = "Telefon Numarası";
            dataGridView.Columns["student_identity_number"].HeaderText = "Kimlik Numarası";
            dataGridView.Columns["student_education_level"].HeaderText = "Eğitim Seviyesi";
        }

        public void insertData()
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand(insertDataString, connection);

            command.Parameters.AddWithValue("@name", textBox_ad.Text);
            command.Parameters.AddWithValue("@surname", textBox_soyad.Text);
            command.Parameters.AddWithValue("@number", Convert.ToInt32(textBox_numara.Text));
            command.Parameters.AddWithValue("@class", textBox_sinif.Text);
            command.Parameters.AddWithValue("@email", textBox_mail.Text);
            command.Parameters.AddWithValue("@phoneNumber", textBox_iletisimNo.Text);
            command.Parameters.AddWithValue("@identityNumber", textBox_tcKimlik.Text);
            command.Parameters.AddWithValue("@educationLevel", textBox_egitimSeviyesi.Text);

            command.ExecuteNonQuery();

            connection.Close();

            fetchData();
        }

        public void updateData()
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand(updateDataString, connection);

            command.Parameters.AddWithValue("@name", textBox_ad.Text);
            command.Parameters.AddWithValue("@surname", textBox_soyad.Text);
            command.Parameters.AddWithValue("@number", Convert.ToInt32(textBox_numara.Text));
            command.Parameters.AddWithValue("@class", textBox_sinif.Text);
            command.Parameters.AddWithValue("@email", textBox_mail.Text);
            command.Parameters.AddWithValue("@phoneNumber", textBox_iletisimNo.Text);
            command.Parameters.AddWithValue("@identityNumber", textBox_tcKimlik.Text);
            command.Parameters.AddWithValue("@educationLevel", textBox_egitimSeviyesi.Text);
            command.Parameters.AddWithValue("@id", textBox_id.Text);

            command.ExecuteNonQuery();

            connection.Close();

            fetchData();
        }

        public void deleteData()
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand(deleteDataString, connection);

            command.Parameters.AddWithValue("@id", textBox_id.Text);

            command.ExecuteNonQuery();

            connection.Close();

            fetchData();
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Form1_Load(object sender, EventArgs e)
        {
            fetchData();
        }

        private void button_goster_Click(object sender, EventArgs e)
        {
            fetchData();
        }

        private void button_ekle_Click(object sender, EventArgs e)
        {
            insertData();
        }

        private void button_guncelle_Click(object sender, EventArgs e)
        {
            updateData();
        }

        private void button_sil_Click(object sender, EventArgs e)
        {
            deleteData();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = dataGridView.CurrentRow.Cells["student_id"].Value.ToString();
            textBox_ad.Text = dataGridView.CurrentRow.Cells["student_name"].Value.ToString();
            textBox_soyad.Text = dataGridView.CurrentRow.Cells["student_surname"].Value.ToString();
            textBox_numara.Text = dataGridView.CurrentRow.Cells["student_number"].Value.ToString();
            textBox_sinif.Text = dataGridView.CurrentRow.Cells["student_class"].Value.ToString();
            textBox_mail.Text = dataGridView.CurrentRow.Cells["student_email"].Value.ToString();
            textBox_iletisimNo.Text = dataGridView.CurrentRow.Cells["student_phone_number"].Value.ToString();
            textBox_tcKimlik.Text = dataGridView.CurrentRow.Cells["student_identity_number"].Value.ToString();
            textBox_egitimSeviyesi.Text = dataGridView.CurrentRow.Cells["student_education_level"].Value.ToString();
        }
    }
}