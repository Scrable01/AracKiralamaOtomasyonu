using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            FrmMusteriEkleme frmMusteriEkleme = new FrmMusteriEkleme();
            frmMusteriEkleme.ShowDialog();
        }

        private void btnMusteriListesi_Click(object sender, EventArgs e)
        {
            MusteriListele musteriListelefrm = new MusteriListele();
            musteriListelefrm.Show();
        }

        private void btnAraçEkle_Click(object sender, EventArgs e)
        {
            AracEkle aracEkle = new AracEkle();
            aracEkle.Show();
        }

        private void btnAraçListele_Click(object sender, EventArgs e)
        {
            AracListele aracListele = new AracListele();
            aracListele.Show();
        }

        private void btnSatıslar_Click(object sender, EventArgs e)
        {
            Sozlesme sozlesmefrm = new Sozlesme();
            sozlesmefrm.Show();
        }

        private void btnSozlesme_Click(object sender, EventArgs e)
        {
            Satis satis = new Satis();
            satis.Show();
        }

        private void btnAracTeslim_Click(object sender, EventArgs e)
        {
            AracTeslim aracTeslim = new AracTeslim();
            aracTeslim.Show();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

