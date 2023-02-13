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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        public static string ad;
        public static string soyad;
        public static string sifre;
        public void Getir()
        {

            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesi = "Select * From PersonelTablosu Where MailAdress like '" + txtMailKontrol.Text + "'";
            SqlCommand komutyeni = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komutyeni.ExecuteReader();
            while (read.Read())
            {
                ad = read["FirstName"].ToString();
                soyad = read["LastName"].ToString();
                sifre = read["Password"].ToString();
            }
            baglanti.Close();
        }
        
        private void btnGiris_Click_1(object sender, EventArgs e)
        {
            Getir();
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM PersonelTablosu ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                if (txtMailKontrol.Text == oku["MailAdress"].ToString() && txtSifreKontrol.Text == oku["Password"].ToString())
                {
                    GirisBasarili gb = new GirisBasarili();
                    gb.ShowDialog();
                    this.Hide();
                }
                else
                {   
                    MessageBox.Show("Bilgileri yanlış girdiniz", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            oku.Close();
            baglanti.Close();
        }

        private void btnSifremiUnuttum_Click_1(object sender, EventArgs e)
        {
            Getir();
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM PersonelTablosu ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                if (txtMailKontrol.Text == "")
                {
                    MessageBox.Show("Mail adresi boş bırakılamaz", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtMailKontrol.Text == oku["MailAdress"].ToString())
                {
                    
                    SifreTazele sifre = new SifreTazele();
                    sifre.Show();
                }

                else
                {
                    
                    MessageBox.Show("Mail adresi ile veri tabanındaki adresler uyuşmuyor", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtSifreKontrol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGiris_Click_1((object)sender, (EventArgs)e);
            }
        }
    }
}
