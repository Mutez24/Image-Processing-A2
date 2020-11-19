using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Pb_info_semestre_2
{
    class Blanche
    {
        private int hauteur;
        private int largeur;
        private byte[] tab;
        private int tailletab;
        private int tailleoffset = 54;

        /// <summary>
        /// permet de créer une image blanche pour créer la fractale
        /// </summary>
        /// <param name="hauteur">
        /// hauteur voulue de l'image blanche
        /// </param>
        /// <param name="largeur">
        /// largeur voulue de l'image blanche
        /// </param>
        /// <param name="filename">
        /// nom du fichier de l'image blanche
        /// </param>
        public Blanche(int hauteur, int largeur, string filename)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            tailletab = (hauteur * largeur * 3) + 54;
            tab = new byte[tailletab];
            tab[0] = 66;
            tab[1] = 77;
            tab[14] = 40;
            tab[26] = 1;
            tab[28] = 24;
            int tailleimage = hauteur * largeur * 3;
            byte[] tabtailleimage = Convertir_int_to_endian(tailleimage);
            int compteur = 0;
            for (int i = 34; i < 38; i++)
            {
                tab[i] = tabtailleimage[compteur];
                compteur++;
            }
            byte[] tabtaillefichier = Convertir_int_to_endian(tailletab); //convertit la taille du fichier
            int compteurA = 0;
            for (int i = 2; i < 6; i++)
            {
                tab[i] = tabtaillefichier[compteurA];
                compteurA++;
            }
            byte[] tabtailleoffset = Convertir_int_to_endian(tailleoffset); //convertit la taille offset du fichier
            int compteurB = 0;
            for (int i = 10; i < 14; i++)
            {
                tab[i] = tabtailleoffset[compteurB];
                compteurB++;
            }
            byte[] tabtaillelargeur = Convertir_int_to_endian(largeur); //convertit la largeur du fichier
            int compteurC = 0;
            for (int i = 18; i < 22; i++)
            {
                tab[i] = tabtaillelargeur[compteurC];
                compteurC++;
            }
            byte[] tabtaillehauteur = Convertir_int_to_endian(hauteur); //convertit la hauteur du fichier
            int compteurD = 0;
            for (int i = 22; i < 26; i++)
            {
                tab[i] = tabtaillehauteur[compteurD];
                compteurD++;
            }
            for(int i = 54; i < tailletab-2; i+=3) //permet de créer une image blanche
            {
                tab[i] = 255;
                tab[i+ 1] = 255;
                tab[i+ 2] = 255;
            }
            File.WriteAllBytes(filename, tab);
        }

        /// <summary>
        /// permet de faire la conversion de int to endian
        /// </summary>
        /// <param name="val"></param>
        /// val est la valeur que l'on veut convertir en endian
        /// <returns></returns>
        public byte[] Convertir_int_to_endian(int val)
        {
            byte[] tab = new byte[4]; // créer le tableau qui va recevoir la conversion
            byte[] tabtemp = new byte[4]; // créeer un tableau temporaire
            if (val <= 255) // 256^1 - 1
            {
                tabtemp[0] = Convert.ToByte(val);
                for (int i = 1; i < tabtemp.Length; i++)
                {
                    tabtemp[i] = 0;
                }
            }
            if (val <= 65535) //65 535 = 256^2 - 1
            {
                double quotient = val / 256;
                double quotientb = Math.Truncate(quotient);
                double reste = val - (quotientb * 256);
                tabtemp[0] = Convert.ToByte(reste);
                tabtemp[1] = Convert.ToByte(quotientb);
                for (int i = 3; i < 4; i++)
                {
                    tabtemp[i] = 0;
                }
            }
            if (val <= 16777215) //16 777 215 = 256^3 - 1
            {
                double quotient1 = val / (256 * 256);
                double quotient1b = Math.Truncate(quotient1);
                double reste1 = val - (quotient1b * 256 * 256);
                double quotient2 = reste1 / 256;
                double quotient2b = Math.Truncate(quotient2);
                double reste2 = reste1 - (quotient2b * 256);
                tabtemp[0] = Convert.ToByte(reste2);
                tabtemp[1] = Convert.ToByte(quotient2b);
                tabtemp[2] = Convert.ToByte(quotient1b);
                tabtemp[3] = 0;
            }
            else
            {
                double quotient1 = val / (256 * 256 * 256);
                double quotient1b = Math.Truncate(quotient1);
                double reste1 = val - (quotient1b * 256 * 256 * 256);
                double quotient2 = reste1 / (256 * 256);
                double quotient2b = Math.Truncate(quotient2);
                double reste2 = reste1 - (quotient2b * 256 * 256);
                double quotient3 = reste2 / 256;
                double quotient3b = Math.Truncate(quotient2);
                double reste3 = reste2 - (quotient3b * 256);
                tabtemp[0] = Convert.ToByte(reste3);
                tabtemp[1] = Convert.ToByte(quotient3b);
                tabtemp[2] = Convert.ToByte(quotient2b);
                tabtemp[3] = Convert.ToByte(quotient3b);
            }
            int k = 0;
            for (int i = 0; i < tabtemp.Length; i++) //parcourt le tableau de la fin jusqu'au debut
            {
                tab[k] = tabtemp[i]; //permet de créer le nouveau tableau
                k++;
            }
            return tab;
        }

        /// <summary>
        /// permet de faire la conversion de endian to int
        /// </summary>
        /// <param name="tab"></param>
        /// tab est le tableau en byte du nombre en endian
        /// <returns></returns>
        public int Convertir_Endian_to_Int(byte[] tab)
        {
            int valeurint = 0;
            int puissance = 1;
            for (int i = 0; i < 4; i++) // permet de parcourir le tableau d'endian
            {
                valeurint += (tab[i] * puissance); //pemret de faire le calcul du int
                puissance = puissance * 256; // augmente la puissance a chque case du tableau d'endian
            }
            return valeurint;
        }

    }
}
