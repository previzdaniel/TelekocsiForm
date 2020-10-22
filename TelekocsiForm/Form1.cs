using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TelekocsiForm
{
    public partial class frmfo : Form
    {
        private List<Auto> autok = new List<Auto>();
        private List<Igeny> igenyek = new List<Igeny>();
        public frmfo()
        {
            InitializeComponent();
            lbKimenet.Items.Clear();
        }

        private void btnBeolvasas_Click(object sender, EventArgs e)
        {
            try
            {
                StreamReader file = new StreamReader("autok.csv");
                file.ReadLine();
                while (!file.EndOfStream)
                {
                    autok.Add(new Auto(file.ReadLine()));
                }
                file.Close();

                StreamReader fajl = new StreamReader("igenyek.csv");
                fajl.ReadLine();
                while (!fajl.EndOfStream)
                {
                    igenyek.Add(new Igeny(fajl.ReadLine()));
                }
                fajl.Close();

                btnSecond.Enabled = true;
                btnBeolvasas.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSecond_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Add("2. feladat");
            lbKimenet.Items.Add($"    {autok.Count} autós hirdet fuvart.");

            btnThird.Enabled = true;
            btnSecond.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            lbKimenet.Items.Add("3. feladat");
            int ferohely = 0;
            for (int i = 0; i < autok.Count; i++)
            {
                if (autok[i].Utvonal == "Budapest-Miskolc")
                {
                    ferohely += autok[i].Ferohely;
                }
            }
            lbKimenet.Items.Add($"   Összesen {ferohely} férőhelyet hirdettek az autósok Budapestről Miskolcra.");
            btnSecond.Enabled = false;
            btnFourth.Enabled = true;
        }

        private void btnFourth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();

            int max = 0;
            string utv = "";

            var utvonalak = from a in autok
                            orderby a.Utvonal
                            group a by a.Utvonal into temp
                            select temp;

            foreach (var ut in utvonalak)
            {
                int fh = ut.Sum(x => x.Ferohely);
                if (max < fh)
                {
                    max = fh;
                    utv = ut.Key;
                }
            }

            lbKimenet.Items.Add("4. feladat");
            lbKimenet.Items.Add("A legtöbb férőhejet");
            lbKimenet.Items.Add($"{max}");
            lbKimenet.Items.Add("a");
            lbKimenet.Items.Add($"{utv} útvonalon");
            lbKimenet.Items.Add("ajánlották fel a hirdetők.");

            btnThird.Enabled = false;
            btnFifth.Enabled = true;
        }

        private void btnFifth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();
            btnFourth.Enabled = false;

            lbKimenet.Items.Add("5. feladat");
            foreach (var igeny in igenyek)
            {
                int i = igeny.VanAuto(autok);
                if (i > -1)
                {
                    lbKimenet.Items.Add($"{igeny.Azonosito} => {autok[i].Rendszam}");
                }
            }

            btnFifth.Enabled = false;
            btnSixth.Enabled = true;
        }

        private void btnSixth_Click(object sender, EventArgs e)
        {
            lbKimenet.Items.Clear();

            try
            {
                lbKimenet.Items.Clear();
                lbKimenet.Items.Add("6. feladat");
                StreamWriter file = new StreamWriter("utasuzenetek.txt");

                foreach (var igeny in igenyek)
                {
                    int i = igeny.VanAuto(autok);
                    if (i > -1)
                    {
                        file.WriteLine($"{igeny.Azonosito}: Rendszám: {autok[i].Rendszam}, Telefonszám: {autok[i].Telefon}");
                    }
                    else
                    {
                        file.WriteLine($"{igeny.Azonosito}: Sajnos nem sikerült autót találni");
                    }
                }
                file.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnkilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
