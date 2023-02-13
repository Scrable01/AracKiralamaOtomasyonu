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
    public partial class MusteriListele : Form
    {
        public MusteriListele()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        private void MusteriListele_Load(object sender, EventArgs e)
        {
            Musteri_Listele();
            dataGridView1.Columns[0].HeaderText = "Numara";
            dataGridView1.Columns[1].HeaderText = "Tc No";
            dataGridView1.Columns[2].HeaderText = "Ad Soyad";
            dataGridView1.Columns[3].HeaderText = "Telefon No";

        }
        public void Musteri_Listele() //Musteriler tablosunu data grid view'da gösterir
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Select * from Musteriler";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //boxlara değerleri yazar
        {
            txtTcno.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefonNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e) //Tc_No harici diğer değerler güncellenebilir
        {
            if (txtTcno.Text == "" || txtAdSoyad.Text == "" || txtTelefonNo.Text == "" || txtMail.Text == "" || txtAdres.Text == "")
            {
                MessageBox.Show("Değerler boş bırakılamaz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Update Musteriler set Ad_Soyad=@adsoyad,Tel_No=@telefon,Mail=@mail,Adres=@adres where Tc_No=@tc";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@tc", txtTcno.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefonNo.Text);
            komut.Parameters.AddWithValue("@mail", txtMail.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();
            }
        }

        private void btnSil_Click(object sender, EventArgs e) //seçilen satırı tablodan siler
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Delete From Musteriler where Tc_No='" + dataGridView1.CurrentRow.Cells["Tc_No"].Value.ToString() + "'";

            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Musteri_Listele();

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
