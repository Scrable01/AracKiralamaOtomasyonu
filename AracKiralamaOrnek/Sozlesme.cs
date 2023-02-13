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
using System.Xml.Serialization;
//using DevExpress.Utils.Extensions;

namespace AracKiralamaOrnek
{
    public partial class Sozlesme : Form
    {
        public Sozlesme()
        {
            InitializeComponent();
        }
        
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        public void MuayeneKaskoSigorta()
        {
            DateTime date = DateTime.Now;
            txtdate.Text = date.ToString();
            TimeSpan fark = DateTime.Parse(txtdate.Text) - DateTime.Parse(dtpMuayene.Text);
            int farkHesap = fark.Days;
            int x = farkHesap - 730;
            int y = farkHesap - 700;
            int k = 30 - y;
            if (x >= 0)
            {
                MessageBox.Show("Aracın muayeneye gitmesi gerekiyor.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (y >= 0)
            {
                MessageBox.Show("Aracın muayene süresi " + k + " gün sonra bitiyor", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (cboxSigorta.Text == "Yok")
            {
                MessageBox.Show("Aracın Sigortası Bulunmuyor", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cboxKasko.Text == "Yok")
            {
                MessageBox.Show("Aracın Kaskosu Bulunmuyor", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Arac_Listele() 
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesi = "Select * From Araclar where Durumu = 'Boş'"; //Araclar tablosundaki Durumu Boş araçları getirir
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cboxAraç.Items.Add(read["Plaka"]);
            }

        }
        public void Sozlesme_Listele() 
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            String komutCumlesi = "Select * From Sozlesme"; //Data Grid View'a Sozlesme tablosundaki bilgileri getirir
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void cboxKirasekli_SelectedIndexChanged(object sender, EventArgs e) //Kiralama şekline göre fiyat belirleme
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Select Kira_Ucreti From Araclar";
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (cboxKirasekli.SelectedIndex == 0)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 1).ToString();
                }
                else if (cboxKirasekli.SelectedIndex == 1)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 0.80).ToString();
                }
                else if (cboxKirasekli.SelectedIndex == 2)
                {
                    txtKiraUcreti.Text = (int.Parse(read["Kira_Ucreti"].ToString()) * 0.70).ToString();
                }

            }

        }

        private void Sozlesme_Load(object sender, EventArgs e)
        {
            
            Arac_Listele();
            Sozlesme_Listele();
            dataGridView1.Columns[0].HeaderText = "Numara";
            dataGridView1.Columns[1].HeaderText = "Tc No";
            dataGridView1.Columns[2].HeaderText = "Ad Soyad";
            dataGridView1.Columns[4].HeaderText = "Ehliyet No";
            dataGridView1.Columns[5].HeaderText = "Ehliyet Tarihi";
            dataGridView1.Columns[11].HeaderText = "Kira Şekli";
            dataGridView1.Columns[12].HeaderText = "Kira Ücreti";
            dataGridView1.Columns[13].HeaderText = "Kiralandığı Gün Sayısı";
            dataGridView1.Columns[15].HeaderText = "Çıkış Tarihi";
            dataGridView1.Columns[16].HeaderText = "Dönüş Tarihi";
        }

        private void cboxAraç_SelectedIndexChanged(object sender, EventArgs e) 
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesi = "Select * From Araclar where Plaka like '" + cboxAraç.SelectedItem + "'"; //Seçilen Plakaya göre Araclar tablosundaki istenilen bilgileri getirir
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtMarka.Text = read["Marka"].ToString();
                txtSeri.Text = read["Seri"].ToString();
                txtModel.Text = read["Model"].ToString();
                txtRenk.Text = read["Renk"].ToString();
                cboxSigorta.Text = read["Sigorta"].ToString();
                cboxKasko.Text = read["Kasko"].ToString();
                dtpMuayene.Text = read["Muayene"].ToString();
            }
            MuayeneKaskoSigorta();
        }
        
        private void btnHesapla_Click(object sender, EventArgs e) //Fiyat hesaplama
        {
            TimeSpan gunfarki = DateTime.Parse(dtpDonustarihi.Text) - DateTime.Parse(dtpCıkıstarihi.Text);
            int gunhesap = gunfarki.Days;
            txtGun.Text = gunhesap.ToString();
            

            txtTutar.Text = (gunhesap * int.Parse(txtKiraUcreti.Text)).ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e) 
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            if (txtTc.Text==""||txtAdsoyad.Text==""||txtTelefon.Text == "" ||txtEhliyetno.Text == "" ||txtEhliyettarihi.Text == "" ||cboxAraç.Text == "" ||txtMarka.Text == "" ||txtSeri.Text == "" ||txtRenk.Text == "" ||cboxKirasekli.Text == "" ||txtKiraUcreti.Text == "" ||txtGun.Text == "" ||txtTutar.Text == "")
            {
                MessageBox.Show("Değerler boş bırakılamaz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

            string komutCumlesi = "Insert Into Sozlesme Values (@tcno,@AdSoyad,@Telefon,@ehliyetno,@ehliyettarihi,@Plaka,@Marka,@Seri,@Model,@Renk,@kirasekli,@kiraucreti,@kiralandıgıgunsayisi,@tutar,@cikistarihi,@donustarihi)"; //Bilgileri Sözlesme tablosuna girer
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.Parameters.AddWithValue("@tcno", txtTc.Text);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdsoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@ehliyetno", txtEhliyetno.Text);
            komut.Parameters.AddWithValue("@ehliyettarihi", txtEhliyettarihi.Text);
            komut.Parameters.AddWithValue("@Plaka", cboxAraç.Text);
            komut.Parameters.AddWithValue("@Marka", txtMarka.Text);
            komut.Parameters.AddWithValue("@Seri", txtSeri.Text);
            komut.Parameters.AddWithValue("@Model", txtModel.Text);
            komut.Parameters.AddWithValue("@Renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@kirasekli", cboxKirasekli.SelectedItem);
            komut.Parameters.AddWithValue("@kiraucreti", txtKiraUcreti.Text);
            komut.Parameters.AddWithValue("@kiralandıgıgunsayisi", txtGun.Text);
            komut.Parameters.AddWithValue("@tutar", txtTutar.Text);
            komut.Parameters.AddWithValue("@cikistarihi", dtpCıkıstarihi.Value);
            komut.Parameters.AddWithValue("@donustarihi", dtpDonustarihi.Value);
            

            string komutCumlesiUp = "Update Araclar set Durumu = 'Dolu' where Plaka ='" + cboxAraç.SelectedItem + "'"; //Araclar tablosundaki Durumu Dolu olarak günceller
            SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);

            komutUp.ExecuteNonQuery();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Sozlesme_Listele();
            Arac_Listele();
            MessageBox.Show("Araç eklendi", "İşlem tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtTcileara_MaskInputRejected(object sender, MaskInputRejectedEventArgs e) //Tc_No ya göre diğer değerleri getirir
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesi = "Select * From Musteriler where Tc_No like '" + txtTcileara.Text + "'"; //Tc_No ya göre diğer değerleri getirir
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtTc.Text = read["Tc_No"].ToString();
                txtAdsoyad.Text = read["Ad_Soyad"].ToString();
                txtTelefon.Text = read["Tel_No"].ToString();

            }
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
