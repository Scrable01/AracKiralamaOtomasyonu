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
    public partial class FrmMusteriEkleme : Form
    {
        public FrmMusteriEkleme()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();

        private void BtnKaydet_Click(object sender, EventArgs e) //boxlara yazılan değerleri Musteriler tablosuna girer
        {
            if (txtTcno.Text==""|| txtAdSoyad.Text==""|| txtTelefonNo.Text == ""|| txtMail.Text == ""|| txtAdres.Text == "")
            {
                MessageBox.Show("Değerler boş bırakılamaz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutcümlesi = "Insert Into Musteriler Values (@Tcno,@AdSoyad,@TelNo,@Mail,@Adres)";
            SqlCommand komut = new SqlCommand(komutcümlesi, baglanti);

            komut.Parameters.AddWithValue("@Tcno", txtTcno.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@TelNo", txtTelefonNo.Text);
            komut.Parameters.AddWithValue("@Mail", txtMail.Text);
            komut.Parameters.AddWithValue("@Adres", txtAdres.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarılı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
