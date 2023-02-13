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
    public partial class AracListele : Form
    {
        public AracListele()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();

        public void Arac_Listele() //Araclar tablosunu data grid view'da gösterir
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            String komutCumlesi = "Select * From Araclar";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        public void Arac_Guncelle() //boxlarda değiştirilen değerleri Araclar tablosunda güncelleyen metot
        {
            if (txtPlaka.Text == "" || cboxMarka.Text == "" || cboxSeri.Text == "" || txtModel.Text == "" || txtRenk.Text == "" || txtKilometre.Text == "" || cboxYakit.Text == "" || txtUcret.Text == "" || cboxDurumu.Text == "" || cboxSigorta.Text == "" || cboxKasko.Text == "" || dtpMuayene.Text == "")
            {
                MessageBox.Show("Değerler boş bırakılamaz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();

                string komutCumlesi = "Update Araclar set Marka=@marka,Seri=@seri,Model=@model,Renk=@renk,Kilometre=@km,Yakıt=@yakit,Kira_Ucreti=@ücret,Durumu=@durum,Sigorta=@sigorta, Kasko=@kasko,muayene=@Muayene where Plaka=@plaka";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                komut.Parameters.AddWithValue("@marka", cboxMarka.Text);
                komut.Parameters.AddWithValue("@seri", cboxSeri.Text);
                komut.Parameters.AddWithValue("@model", txtModel.Text);
                komut.Parameters.AddWithValue("@renk", txtRenk.Text);
                komut.Parameters.AddWithValue("@km", txtKilometre.Text);
                komut.Parameters.AddWithValue("@yakit", cboxYakit.Text);
                komut.Parameters.AddWithValue("@ücret", txtUcret.Text);
                komut.Parameters.AddWithValue("@durum", cboxDurumu.Text);
                komut.Parameters.AddWithValue("@sigorta", cboxSigorta.SelectedItem);
                komut.Parameters.AddWithValue("@kasko", cboxKasko.SelectedItem);
                komut.Parameters.AddWithValue("@muayene", dtpMuayene.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Arac_Listele();
            }

        }
        private void AracListele_Load(object sender, EventArgs e)
        {
            Arac_Listele();
            dataGridView1.Columns[0].HeaderText = "Numara";
            dataGridView1.Columns[8].HeaderText = "Kira Ücreti";
            dataGridView1.Columns[12].HeaderText = "Son Muayene Tarihi";
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Arac_Guncelle();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) //boxlara değerleri yazar
        {
            txtPlaka.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cboxMarka.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cboxSeri.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtModel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtRenk.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKilometre.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            cboxYakit.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtUcret.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            cboxDurumu.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            cboxSigorta.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            cboxKasko.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            dtpMuayene.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();


        }

        private void btnSil_Click(object sender, EventArgs e)//seçilen satırı Araclar tablosundan siler
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Delete from Araclar where Plaka='" + dataGridView1.CurrentRow.Cells["Plaka"].Value.ToString() + "'";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
            Arac_Listele();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
