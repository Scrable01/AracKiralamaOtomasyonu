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
    public partial class AracTeslim : Form
    {
        
        public AracTeslim()
        {
            InitializeComponent();
        }
        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        string tc;
        string adSoyad;
        string tel;
        string plaka;
        string kiraSekli;
        string kiraUcreti;
        string tutar;
        string cıkıs;
        string donus;
        public void Arac_Listele()
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesi = "Select * From Araclar where Durumu = 'Dolu'"; //Araclar tablosundaki Durumu Dolu araçları getirir
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
        private void AracTeslim_Load(object sender, EventArgs e)
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
            {//Seçilen Plakaya göre Araclar tablosundaki istenilen bilgileri getirir
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();
                string komutCumlesi = "Select * From Araclar where Plaka like '" + cboxAraç.SelectedItem + "'"; 
                SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    txtMarka.Text = read["Marka"].ToString();
                    txtSeri.Text = read["Seri"].ToString();
                    txtModel.Text = read["Model"].ToString();
                    txtRenk.Text = read["Renk"].ToString();
                }
                baglanti.Close();
            }
            
            {//Seçilen plakaya göre bilgileri txtboxlara getirir
                SqlConnection baglanti = new SqlConnection(bgl.Adres);
                baglanti.Open();
                string komutCumlesiUp = "Select * From Sozlesme where Plaka like '" + cboxAraç.SelectedItem + "'";
                SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);
                SqlDataReader reader = komutUp.ExecuteReader();
                while (reader.Read())
                {
                    tc= reader["Tc_No"].ToString();
                    adSoyad = reader["Ad_Soyad"].ToString();
                    tel = reader["Telefon"].ToString();
                    plaka = reader["Plaka"].ToString();
                    kiraSekli = reader["Kira_Sekli"].ToString();
                    kiraUcreti = reader["Kira_Ucreti"].ToString();
                    tutar = reader["Tutar"].ToString();
                    cıkıs = reader["Cikis_Tarihi"].ToString();
                    donus = reader["Donus_Tarihi"].ToString();

                    
                }
                baglanti.Close();
            }

        }

        private void btnAracteslim_Click(object sender, EventArgs e)
        {
            DataGridViewRow satir = dataGridView1.CurrentRow;

            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();

            string komutCumlesi = "Delete from Sozlesme where Plaka = '" + cboxAraç.SelectedItem + "'"; //cboxAraçta seçilen satırı Sözlesme tablosundan siler
            SqlCommand komut = new SqlCommand(komutCumlesi, baglanti);
            komut.ExecuteNonQuery();

            string komutCumlesiUp = "Update Araclar set Durumu = 'Boş' where Plaka ='" + cboxAraç.SelectedItem + "'"; //Araç Durumunu Boş olarak değiştirir
            SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);
            komutUp.ExecuteNonQuery();

            string komutCumlesiSatis = "Insert Into Satis Values (@tc_no,@AdSoyad,@telefon,@plaka,@kirasekli,@kiraücreti,@tutar,@cikistarih,@dönüstarih)"; //Bilgileri Satis tablosuna girer
            SqlCommand komutSatis = new SqlCommand(komutCumlesiSatis, baglanti);

            komutSatis.Parameters.AddWithValue("@tc_no", tc.ToString());
            komutSatis.Parameters.AddWithValue("@AdSoyad", adSoyad.ToString());
            komutSatis.Parameters.AddWithValue("@telefon", tel.ToString());
            komutSatis.Parameters.AddWithValue("@plaka", plaka.ToString());
            komutSatis.Parameters.AddWithValue("@kirasekli", kiraSekli.ToString());
            komutSatis.Parameters.AddWithValue("@kiraücreti", kiraUcreti.ToString());
            komutSatis.Parameters.AddWithValue("@tutar", tutar.ToString());
            komutSatis.Parameters.AddWithValue("@cikistarih", cıkıs.ToString());
            komutSatis.Parameters.AddWithValue("@dönüstarih", donus.ToString());
            komutSatis.ExecuteNonQuery();

            
            String komutCumlesiGet = "Select * From Sozlesme"; //Araç teslim sonrası dgv'ın günceller
            SqlCommand komutGet = new SqlCommand(komutCumlesiGet, baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutGet);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            MessageBox.Show("Araç teslim edildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
