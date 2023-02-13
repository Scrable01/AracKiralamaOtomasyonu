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
    public partial class AracEkle : Form
    {
        public AracEkle()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();

        private void btnSave_Click(object sender, EventArgs e) // boxlara yazılan değerleri Araclar tablosuna girer
        {

            try
            {
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();

                string komutCumlesi = "Insert Into Araclar Values (@Plaka, @Marka, @Seri, @Model, @Renk, @Kilometre, @Yakıt, @Kira_Ucreti, @Durum, @Sigorta, @Kasko, @Muayene)";
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);

                komut.Parameters.AddWithValue("@Plaka", txtPlaka.Text);
                komut.Parameters.AddWithValue("@Marka", cboxMarka.SelectedItem);
                komut.Parameters.AddWithValue("@Seri", cboxSeri.SelectedItem);
                komut.Parameters.AddWithValue("@Model", txtModel.Text);
                komut.Parameters.AddWithValue("@Renk", txtRenk.Text);
                komut.Parameters.AddWithValue("@Kilometre", txtKilometre.Text);
                komut.Parameters.AddWithValue("@Yakıt", cboxYakıt.SelectedItem);
                komut.Parameters.AddWithValue("@Kira_Ucreti", txtKiraUcreti.Text);
                komut.Parameters.AddWithValue("@Durum", cboxDurumu.SelectedItem);
                komut.Parameters.AddWithValue("@Sigorta", cboxSigorta.SelectedItem);
                komut.Parameters.AddWithValue("@Kasko", cboxKasko.SelectedItem);
                komut.Parameters.AddWithValue("@Muayene", dtpMuayene.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("Araç Kaydı Başarılı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Değerler boş bırakılamaz","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void cboxMarka_SelectedIndexChanged(object sender, EventArgs e)
        //seçilen markaya göre araçları gösterir (marka değiştirildiğinde önceki modelleri göstermemesi için combobox her seferinde temizlenir)
        {
            if (cboxMarka.SelectedIndex == 0)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("Giulia");
                cboxSeri.Items.Add("Giulietta");
            }
            else if (cboxMarka.SelectedIndex == 1)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("DB11");
                cboxSeri.Items.Add("Rapide");
                cboxSeri.Items.Add("Vantage");
                cboxSeri.Items.Add("Varige");
            }
            else if (cboxMarka.SelectedIndex == 2)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("A6");
                cboxSeri.Items.Add("A7");
                cboxSeri.Items.Add("A8");
                cboxSeri.Items.Add("E-Tron GT");
                cboxSeri.Items.Add("R8");
            }
            else if (cboxMarka.SelectedIndex == 3)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("520d Gran Turismo");
                cboxSeri.Items.Add("525d xDrive");
                cboxSeri.Items.Add("730d Long");
                cboxSeri.Items.Add("840d xDrive Gran");
                cboxSeri.Items.Add("M5 Competition");
            }
            else if (cboxMarka.SelectedIndex == 4)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("CTS");
                cboxSeri.Items.Add("Escalade");
                cboxSeri.Items.Add("Lyriq");
            }
            else if (cboxMarka.SelectedIndex == 5)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("Challenger SRT Hellcat");
                cboxSeri.Items.Add("Charger 6.4");
            }
            else if (cboxMarka.SelectedIndex == 6)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("A180 CDI");
                cboxSeri.Items.Add("E63 AMG");
                cboxSeri.Items.Add("S500 L");
            }
            else if (cboxMarka.SelectedIndex == 7)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("911 GT3");
                cboxSeri.Items.Add("Cayman");
                cboxSeri.Items.Add("Taycan Turismo");
                cboxSeri.Items.Add("Panamera 4S");
            }
            else if (cboxMarka.SelectedIndex == 8)
            {
                cboxSeri.Items.Clear();
                cboxSeri.Items.Add("S60 D4");
                cboxSeri.Items.Add("S90 D5");
                cboxSeri.Items.Add("V90");
                cboxSeri.Items.Add("XC90 D6");
            }

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
