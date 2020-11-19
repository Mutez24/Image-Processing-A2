using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pb_info_semestre_2
{
    class Pixel
    {
        private int rouge;
        private int bleu;
        private int vert;

        public Pixel (int rouge, int vert, int bleu)
        {
            this.bleu = bleu;
            this.vert = vert;
            this.rouge = rouge;
        }

        // permet d'avoir accès et de modifier les valeurs de rouge, vert et bleu.
        public int Rouge
        {
            get
            {
                return this.rouge;
            }
            set
            {
                this.rouge = value;
            }
        }

        public int Vert
        {
            get
            {
                return this.vert;
            }
            set
            {
                this.vert = value;
            }
        }

        public int Bleu
        {
            get
            {
                return this.bleu;
            }
            set
            {
                this.bleu = value;
            }
        }
    }
}
