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

namespace AracKiralamaOrnek
{
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        private void Satis_Load(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Select * From Satis";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].HeaderText = "Numara";
            dataGridView1.Columns[1].HeaderText = "Tc No";
            dataGridView1.Columns[2].HeaderText = "Ad Soyad";
            dataGridView1.Columns[5].HeaderText = "Kira Şekli";
            dataGridView1.Columns[6].HeaderText = "Kira Ücreti";
            dataGridView1.Columns[8].HeaderText = "Çıkış Tarihi";
            dataGridView1.Columns[9].HeaderText = "Dönüş Tarihi";
            baglanti.Close();

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
