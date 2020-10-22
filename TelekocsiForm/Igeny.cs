using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelekocsiForm
{
    class Igeny
    {
        public string Azonosito { get; private set; }
        public string Indulas { get; private set; }
        public string Cel { get; private set; }
        public int Szemelyek { get; private set; }

        public string Utvonal { get; private set; }

        public Igeny(string szoveg)
        {
            string[] adat = szoveg.Split(';');
            Azonosito = adat[0];
            Indulas = adat[1];
            Cel = adat[2];
            Szemelyek = int.Parse(adat[3]);
            Utvonal = Indulas + "-" + Cel;
        }

        public int VanAuto(List<Auto> autok)
        {
            int i = 0;
            while (i < autok.Count &&
                !(Utvonal == autok[i].Utvonal &&
                Szemelyek <= autok[i].Ferohely))
            {
                i++;
            }

            if (i < autok.Count)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
    }
}
