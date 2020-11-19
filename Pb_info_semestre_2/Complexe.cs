using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pb_info_semestre_2
{
    class Complexe
    {
        private double A;
        private double B;

        public Complexe(double partieréel, double partieimaginaire)
        {
            this.A = partieréel;
            this.B = partieimaginaire;
        }

        public double a
        {
            get
            {
                return this.A;
            }
            set
            {
                this.A = value;
            }
        }
        public double b
        {
            get
            {
                return this.B;
            }
            set
            {
                this.B = value;
            }
        }
        /// <summary>
        /// permet de mettre un nombre complexe au carré
        /// </summary>
        public void Aucarre() 
        {
            double temp = A * A - B * B;
            B = 2 * A * B;
            A = temp;
        }
    }
}
