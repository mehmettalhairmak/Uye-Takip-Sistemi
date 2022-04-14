using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Uye_Takip_Sistemi
{
    internal class DatabaseServices
    {
        //readonly FormAnaMenu formAnaMenu = new FormAnaMenu();

        DataTable dataTable;
        SqlDataAdapter adapter;
        SqlConnection connection;
        readonly string fetchDataString = "SELECT * FROM Students";
        readonly string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Uye Takip Sistemi.mdf;";

        public DataTable fetchData()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            adapter = new SqlDataAdapter(fetchDataString, connection);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            connection.Close();
            return dataTable;
        }
    }
}
