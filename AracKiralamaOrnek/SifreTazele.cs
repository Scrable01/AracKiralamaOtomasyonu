using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOrnek
{
    public partial class SifreTazele : Form
    {
        public SifreTazele()
        {
            InitializeComponent();
        }

        //private string baglanticumlesi = @"Data Source=.;Initial Catalog=AracKiralamaOrnek;Integrated Security=True";
        BaglantiSinif bgl = new BaglantiSinif();
        string Pass;
        public void Up()
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string komutCumlesiUp = "Update PersonelTablosu set Password=@sifre where MailAdress=@mailadres";
            SqlCommand komutUp = new SqlCommand(komutCumlesiUp, baglanti);
            
            komutUp.Parameters.AddWithValue("@mailadres", txtMail.Text); ;
            komutUp.Parameters.AddWithValue("@sifre", Pass.ToString());
            komutUp.ExecuteReader();
            baglanti.Close();
        }
        public void Microsoft(string GondericiMail, string GondericiPass, string AliciMail)
        {

            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komutup = new SqlCommand("SELECT * FROM PersonelTablosu ", baglanti);
            SqlDataReader read = komutup.ExecuteReader();

            while (read.Read())
            {
                if (txtMail.Text == read["MailAdress"].ToString())
                {
                    
                    Random rnd = new Random();
                    int a = rnd.Next(100000, 1000000);
                    Pass = a.ToString();
                    SmtpClient sc = new SmtpClient();
                    sc.Port = 587;
                    sc.Host = "smtp.outlook.com";
                    sc.EnableSsl = true;
                    sc.Credentials = new NetworkCredential(GondericiMail, GondericiPass);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(GondericiMail, "Araç Kiralama Projesi");
                    mail.To.Add(AliciMail);
                    mail.Subject = "Şifre Sıfırlama Talebinde Bulundunuz";
                    mail.IsBodyHtml = true;
                    mail.Body = $@"{DateTime.Now.ToString()} Tarihinde şifre sıfırlama talebinde bulundundunuz. Yeni şifreniz: {a}";

                    sc.Send(mail);

                }
                else
                {
                    MessageBox.Show("Bilgileri yanlış girdiniz", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            baglanti.Close();
        }
        
        private void btnSifremiUnuttum_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM PersonelTablosu ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                if (txtMail.Text == oku["MailAdress"].ToString())
                {
                    Microsoft("enesmemduhoglu0@hotmail.com", "E2va1hm3t174", txtMail.Text);
                    MessageBox.Show("Girilen bilgiler eşleştirilir ise şifreniz yenilenecek ve mail olarak gönderilecek.", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Up();
                    btnSifremiUnuttum.Click -= btnSifremiUnuttum_Click;
                }
                else if (txtMail.Text == "")
                {
                    MessageBox.Show("Mail adresi boş bırakılamaz", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Mail adresi ile veri tabanındaki adresler uyuşmuyor", "Durum", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            oku.Close();
            baglanti.Close();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSifremiUnuttum_Click((object)sender, (EventArgs)e);
            }
        }
    }
}
