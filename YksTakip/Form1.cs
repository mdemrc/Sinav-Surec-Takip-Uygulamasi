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

namespace YksTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 0;
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 1;
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 2;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 3;
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 4;
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {
            dgwDenemeSonuclari.PageIndex = 5;
        }

        public void gizle()
        {
            dgwDersler.Columns[0].Visible = false;
            dgwKaynaklar.Columns[0].Visible = false;
            DgwKonular.Columns[0].Visible = false;
            dgwKonuDurumlari.Columns[0].Visible = false;
        }
        public void verilerigosterdersler(string verilerders)
        {
            SqlDataAdapter da = new SqlDataAdapter(verilerders,bgl.baglanti());
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgwDersler.DataSource = ds.Tables[0];
            gizle();
        }
        public void verikaynak(string kaynak)
        {
            SqlDataAdapter dakaynak = new SqlDataAdapter(kaynak, bgl.baglanti());
            DataSet dskaynak = new DataSet();
            dakaynak.Fill(dskaynak);
            dgwKaynaklar.DataSource = dskaynak.Tables[0];
            gizle();
        }

        public void verikonu(string konu)
        {
            SqlDataAdapter dakonu = new SqlDataAdapter(konu, bgl.baglanti());
            DataSet dskonu = new DataSet();
            dakonu.Fill(dskonu);
            DgwKonular.DataSource = dskonu.Tables[0];
            gizle();
        }
 
        public void verikonudurumu(string konudurumu, SqlConnection sqlConnection)
        {
            SqlDataAdapter dakonudurumu = new SqlDataAdapter(konudurumu, bgl.baglanti());
            DataSet dskonudurumu = new DataSet();
            dakonudurumu.Fill(dskonudurumu);
            dgwKonuDurumlari.DataSource = dskonudurumu.Tables[0];
            gizle();
        }

        private void BtnDersGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand dersguncelle = new SqlCommand("update tbl_dersler set dersad=@p1 where dersid=@p2", bgl.baglanti());
            dersguncelle.Parameters.AddWithValue("@p1", TxtDersIsmi.Text);
            dersguncelle.Parameters.AddWithValue("@p2", lbldersid.Text);
            dersguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verilerigosterdersler("select dersid,dersad from tbl_dersler");
            MessageBox.Show("Ders başarıyla güncellendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtDersIsmi.Text = "";
        }

        private void BtnDersEkle_Click(object sender, EventArgs e)
        {
            SqlCommand dersekle = new SqlCommand("insert into tbl_dersler (dersad) values (@p1)", bgl.baglanti());
            dersekle.Parameters.AddWithValue("@p1", TxtDersIsmi.Text);
            dersekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verilerigosterdersler("select dersid,dersad as 'Ders Adı' from tbl_dersler");
            MessageBox.Show("Ders başarıyla eklendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtDersIsmi.Text="";
        }

        private void BtnDersSil_Click(object sender, EventArgs e)
        {
            SqlCommand derssil = new SqlCommand("delete from tbl_dersler where dersid=@p1", bgl.baglanti());
            derssil.Parameters.AddWithValue("@p1", lbldersid.Text);
            derssil.ExecuteNonQuery();
            bgl.baglanti().Close();
            verilerigosterdersler("select dersid,dersad from tbl_dersler");
            TxtDersIsmi.Text = "";
            MessageBox.Show("Ders başarıyla silindi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtDersIsmi.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Dersleri Comboboxlara Çekme
            SqlCommand dersyukle = new SqlCommand("select dersad from tbl_dersler", bgl.baglanti());
            SqlDataReader dr = dersyukle.ExecuteReader();
            while (dr.Read())
            {
                CmbHangiDerseAitKaynak.Items.Add(dr[0]);
                CmbHangiDerseAitKonu.Items.Add(dr[0]);
                CmbDersKonuDurum.Items.Add(dr[0]);
            }

            //Ders Dgw Yüklenmesi
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select dersid,dersad as 'Ders Adı' from tbl_dersler", bgl.baglanti());
            da1.Fill(dt1);
            dgwDersler.DataSource = dt1;
            bgl.baglanti().Close();

            //Kaynak Dgw Yüklenmesi
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select kaynakid,kaynakad as 'Kaynak Adı',kaynakders as 'Ders' from tbl_kaynaklar", bgl.baglanti());
            da2.Fill(dt2);
            dgwKaynaklar.DataSource = dt2;

            //Konu Dgw Yüklenmesi
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("select konuid,konu as 'Konu Adı',konuders as 'Ders' from tbl_konular", bgl.baglanti());
            da3.Fill(dt3);
            DgwKonular.DataSource = dt3;

            //Konu Durum Dgw Yüklenmesi
            DataTable dt4 = new DataTable();
            SqlDataAdapter da4 = new SqlDataAdapter("select konudurumid,KonuAd as 'Konu Adı',KonuDers as ' Ders',KonuKaynak as 'Kaynak Adı',konubittimi as 'Konu Bitti Mi?' from tbl_konudurum", bgl.baglanti());
            da4.Fill(dt4);
            dgwKonuDurumlari.DataSource = dt4;
            gizle();
        }

        private void dgwDersler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Dgw Dersler Çift Tıklama ile Çekme
            int secilen = dgwDersler.SelectedCells[0].RowIndex;
            TxtDersIsmi.Text = dgwDersler.Rows[secilen].Cells[1].Value.ToString();
            lbldersid.Text = dgwDersler.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnKaynakEkle_Click(object sender, EventArgs e)
        {
            SqlCommand kaynakekle = new SqlCommand("insert into tbl_kaynaklar (kaynakad,kaynakders) values (@p1,@p2)", bgl.baglanti());
            kaynakekle.Parameters.AddWithValue("@p1", TxtKaynakIsmi.Text);
            kaynakekle.Parameters.AddWithValue("@p2", CmbHangiDerseAitKaynak.Text);
            kaynakekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikaynak("select kaynakid,kaynakad as 'Kaynak Adı',kaynakders as 'Ders' from tbl_kaynaklar");
            MessageBox.Show("Kaynak başarıyla eklendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKaynakIsmi.Text = "";
        }

        private void BtnKaynakGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand kaynakguncelle = new SqlCommand("update tbl_kaynaklar set kaynakad=@p1,kaynakders=@p2 where kaynakid=@p3", bgl.baglanti());
            kaynakguncelle.Parameters.AddWithValue("@p1", TxtKaynakIsmi.Text);
            kaynakguncelle.Parameters.AddWithValue("@p2", CmbHangiDerseAitKaynak.Text);        
            kaynakguncelle.Parameters.AddWithValue("@p3", lblkaynakid.Text);        
            kaynakguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikaynak("select kaynakid,kaynakad,kaynakders from tbl_kaynaklar");
            MessageBox.Show("Kaynak başarıyla güncellendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKaynakIsmi.Text = "";
        }

        private void BtnKaynakSil_Click(object sender, EventArgs e)
        {
            SqlCommand kaynaksil = new SqlCommand("delete from tbl_kaynaklar where kaynakid=@p1", bgl.baglanti());
            kaynaksil.Parameters.AddWithValue("@p1", lblkaynakid.Text);
            kaynaksil.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikaynak("select kaynakid,kaynakad,kaynakders from tbl_kaynaklar");
            MessageBox.Show("Kaynak başarıyla silindi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKaynakIsmi.Text = "";
        }

        private void dgwKaynaklar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Dgw Kaynaklar Çift Tıklama ile Çekme
            int secilen = dgwKaynaklar.SelectedCells[0].RowIndex;
            TxtKaynakIsmi.Text = dgwKaynaklar.Rows[secilen].Cells[1].Value.ToString();
            CmbHangiDerseAitKaynak.Text=dgwKaynaklar.Rows[secilen].Cells[2].Value.ToString();
            lblkaynakid.Text = dgwKaynaklar.Rows[secilen].Cells[0].Value.ToString();
            
        }

        private void BtnKonuEkle_Click(object sender, EventArgs e)
        {
            SqlCommand konuekle = new SqlCommand("insert into tbl_konular (konu,konuders) values (@p1,@p2)", bgl.baglanti());
            konuekle.Parameters.AddWithValue("@p1", TxtKonuIsmi.Text);
            konuekle.Parameters.AddWithValue("@p2", CmbHangiDerseAitKonu.Text);
            konuekle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikonu("select konuid,konu as 'Konu Adı',konuders as 'Ders' from tbl_konular");
            MessageBox.Show("Konu başarıyla eklendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKonuIsmi.Text = "";
        }

        private void BtnKonuGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand konuguncelle = new SqlCommand("update tbl_konular set konu=@p1,konuders=@p2 where konuid=@p3", bgl.baglanti());
            konuguncelle.Parameters.AddWithValue("@p1", TxtKonuIsmi.Text);
            konuguncelle.Parameters.AddWithValue("@p2", CmbHangiDerseAitKonu.Text);
            konuguncelle.Parameters.AddWithValue("@p3", lblkonuid.Text);
            konuguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikonu("select konuid,konu,konuders from tbl_konular");
            MessageBox.Show("Konu başarıyla güncellendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKonuIsmi.Text = "";
        }

        private void BtnKonuSil_Click(object sender, EventArgs e)
        {
            SqlCommand konusil = new SqlCommand("delete from tbl_konular where konuid=@p1", bgl.baglanti());
            konusil.Parameters.AddWithValue("@p1", lblkonuid.Text);
            konusil.ExecuteNonQuery();
            bgl.baglanti().Close();
            verikonu("select konuid,konu,konuders from tbl_konular");
            MessageBox.Show("Konu başarıyla silindi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TxtKonuIsmi.Text = "";
        }

        private void DgwKonular_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Dgw Konular Çift Tıklama ile Çekme
            int secilen = DgwKonular.SelectedCells[0].RowIndex;
            TxtKonuIsmi.Text = DgwKonular.Rows[secilen].Cells[0].Value.ToString();
            CmbHangiDerseAitKonu.Text = DgwKonular.Rows[secilen].Cells[1].Value.ToString();

        }

        private void CheckboxCozulduMu_CheckedChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            if (CheckboxCozulduMu.Checked==true)
            {
                lblcheckboxdurum.Text = "true";
            }
            else
            {
                lblcheckboxdurum.Text = "false";
            }
        }

        private void CmbDersKonuDurum_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbKaynakKonuDurum.Items.Clear();
            CmbKonuadKonuDurum.Items.Clear();
            SqlCommand konugetir = new SqlCommand("select konu from tbl_konular where konuders=@p1", bgl.baglanti());
            konugetir.Parameters.AddWithValue("@p1", CmbDersKonuDurum.Text);
            SqlDataReader drkonudurum = konugetir.ExecuteReader();
            while (drkonudurum.Read())
            {
                CmbKonuadKonuDurum.Items.Add(drkonudurum[0]);
            }
            bgl.baglanti().Close();

            SqlCommand kaynakgetir = new SqlCommand("select kaynakad from tbl_kaynaklar where kaynakders=@p1", bgl.baglanti());
            kaynakgetir.Parameters.AddWithValue("@p1", CmbDersKonuDurum.Text);
            SqlDataReader drkaynakgetir = kaynakgetir.ExecuteReader();
            while (drkaynakgetir.Read())
            {
                CmbKaynakKonuDurum.Items.Add(drkaynakgetir[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnKonuDurumKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand konudurumkaydet = new SqlCommand("insert into tbl_konudurum (konuad,konuders,konukaynak,konubittimi) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            konudurumkaydet.Parameters.AddWithValue("@p1", CmbKonuadKonuDurum.Text);
            konudurumkaydet.Parameters.AddWithValue("@p2", CmbDersKonuDurum.Text);
            konudurumkaydet.Parameters.AddWithValue("@p3", CmbKaynakKonuDurum.Text);
            konudurumkaydet.Parameters.AddWithValue("@p4", lblcheckboxdurum.Text);
            konudurumkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            CmbDersKonuDurum.Text = "";
            CmbKonuadKonuDurum.Text = "";
            CmbKaynakKonuDurum.Text = "";
            verikonudurumu("select konudurumid,KonuAd as 'Konu Adı',KonuDers as ' Ders',KonuKaynak as 'Kaynak Adı',konubittimi as 'Konu Bitti Mi?' from tbl_konudurum", bgl.baglanti());
            MessageBox.Show("Konu durumu başarıyla kaydedildi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            SqlCommand konudurumguncelle = new SqlCommand("update tbl_konudurum set konuad=@p1,konuders=@p2,konukaynak=@p3,konubittimi=@p4 where konudurumid=@p5", bgl.baglanti());
            konudurumguncelle.Parameters.AddWithValue("@p1", CmbKonuadKonuDurum.Text);
            konudurumguncelle.Parameters.AddWithValue("@p2", CmbDersKonuDurum.Text);
            konudurumguncelle.Parameters.AddWithValue("@p3", CmbKaynakKonuDurum.Text);
            konudurumguncelle.Parameters.AddWithValue("@p4", lblcheckboxdurum.Text);
            konudurumguncelle.Parameters.AddWithValue("@p5", lblkonudurumid.Text);
            konudurumguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            CmbDersKonuDurum.Text = "";
            CmbKonuadKonuDurum.Text = "";
            CmbKaynakKonuDurum.Text = "";
            verikonudurumu("select konudurumid,KonuAd as 'Konu Adı',KonuDers as ' Ders',KonuKaynak as 'Kaynak Adı',konubittimi as 'Konu Bitti Mi?' from tbl_konudurum", bgl.baglanti());
            MessageBox.Show("Konu durumu başarıyla güncellendi.", "İşlem başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
