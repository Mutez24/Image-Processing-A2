using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Pb_info_semestre_2
{
    class MyImage
    {
        //private string typeImage;
        private int taillefichier;
        private int tailleOffset;
        private int largeur;
        private int hauteur;
        //private int nbBitParCouleur = 24; //car le fichier est code sur 24 bits
        private Pixel[,] image;


        public MyImage(string filename)
        {
            /*if (typeImage == "csv")
            {

            }
            if (typeImage == "bmp")*/
            {
                byte[] myfile = File.ReadAllBytes(filename); //lit le fichier
                byte[] tabtaillefichier = { myfile[2], myfile[3], myfile[4], myfile[5] }; //crééer le tableau où les informations de taille sont dans le header
                taillefichier = Convertir_Endian_to_Int(tabtaillefichier); //transforme le tableau de byte en valeur int
               
                byte[] tabtaillefichierOffset = { myfile[10], myfile[11], myfile[12], myfile[13] }; //crééer le tableau où les informations d'offset sont dans le header
                tailleOffset = Convertir_Endian_to_Int(tabtaillefichierOffset); //transforme le tableau de byte en valeur int

                byte[] tablargeur = { myfile[18], myfile[19], myfile[20], myfile[21] }; //crééer le tableau où les informations de largeur sont dans le header
                largeur = Convertir_Endian_to_Int(tablargeur); //transforme le tableau de byte en valeur int

                byte[] tabhauteur = { myfile[22], myfile[23], myfile[24], myfile[25] }; //crééer le tableau où les informations de hauteur sont dans le header
                hauteur = Convertir_Endian_to_Int(tabhauteur); //transforme le tableau de byte en valeur int

                this.image = new Pixel[hauteur, largeur]; //créer la matrice de pixel
                int k = 54; //pour pouvoir commencer la boucle a 54 (cases ou commence l'image)
                for (int i = hauteur - 1; i >= 0; i--) //parcours la matrice, dans les lignes, de haut en bas
                {
                    for (int j = 0 ; j < largeur; j++) //parcours la matrice, dans les colonnes, de droite a gauche
                    {
                        image[i, j] = new Pixel(myfile[k], myfile[k + 1], myfile[k + 2]); // on commence a la case 54, car c'est a cette case que commence l'image
                        k += 3 ;                                                          // on prend 3 cases pour créer un tableau, on rajoute 3 a cas pour passer au pixel suivant
                    }
                }
            }
        }

        //les propriétés suivantes me permettent d'avoir accès à la taille du fichier, la hauteur et la largeur
        public int Taillefichier
        {
            get
            {
                return taillefichier;
            }
        }
        public int Largeur
        {
            get
            {
                return largeur;
            }
        }
        public int Hauteur
        {
            get
            {
                return hauteur;
            }
        }
        public int Offset
        {
            get
            {
                return tailleOffset;
            }
        }


        /// <summary>
        /// permet de faire la conversion de endian to int
        /// </summary>
        /// <param name="tab"></param>
        /// tab est le tableau en byte du nombre en endian
        /// <returns></returns>
        public int Convertir_Endian_to_Int (byte[] tab)
        {
            int valeurint = 0;
            int puissance = 1;
            for (int i = 0; i < 4; i++) // permet de parcourir le tableau d'endian
            {
                valeurint += (tab[i] * puissance); //pemret de faire le calcul du int
                puissance = puissance * 256; // augmente la puissance a chaque case du tableau d'endian
            }
            return valeurint;
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
                    tabtemp[i] = 0; //car les valeurs du tableau a partir de la case 1 sont nuls car le nombre est inférieur a 255
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
                    tabtemp[i] = 0;//car les valeurs du tableau a partir de la case 3 sont nuls car le nombre est inférieur a 65 535
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
                double quotient1 = val / (256 * 256* 256);
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
        /// permet de traduire une image en tableau de byte
        /// </summary>
        /// <param name="file">
        /// file est le nom du fichier que l'on veut traduire
        /// </param>
        public void From_Image_To_File(string file)
        {
            byte[] tab = new byte[taillefichier]; //créer le tableau de byte
            tab[0] = 66; // condition identique pour toutes les fichiers que l'on traite ici
            tab[1] = 77;
            tab[14] = 40; // taille du header info
            tab[26] = 1;
            tab[28] = 24;
            int tailleimage = hauteur * largeur * 3; //on multipli par 3, car chaque pixel possède 3 couleurs
            byte[] tabtailleimage = Convertir_int_to_endian(tailleimage); //convertit la taille de l'image en tableau de byte
            int compteur = 0;
            for (int i = 34; i < 38; i++) //car le tableau qui contient l'informationde la taille se trouve dans cet intervalle
            {
                tab[i] = tabtailleimage[compteur];
                compteur++;// le compteur permet d'avoir un autre "indicateur" que i
            }
            byte[] tabtaillefichier = Convertir_int_to_endian(taillefichier); //convertit la taille du fichier
            int compteurA = 0;
            for (int i = 2; i < 6; i++)
            {
                tab[i] = tabtaillefichier[compteurA]; 
                compteurA++;// le compteur permet d'avoir un autre "indicateur" que i
            }
            byte[] tabtailleoffset = Convertir_int_to_endian(tailleOffset); //convertit la taille offset du fichier
            int compteurB = 0;
            for (int i = 10; i < 14; i++)
            {
                tab[i] = tabtailleoffset[compteurB];
                compteurB++;// le compteur permet d'avoir un autre "indicateur" que i
            }
            byte[] tabtaillelargeur = Convertir_int_to_endian(largeur); //convertit la largeur du fichier
            int compteurC = 0;
            for (int i = 18; i < 22; i++)
            {
                tab[i] = tabtaillelargeur[compteurC];
                compteurC++;// le compteur permet d'avoir un autre "indicateur" que i
            }
            byte[] tabtaillehauteur = Convertir_int_to_endian(hauteur); //convertit la hauteur du fichier
            int compteurD = 0;
            for (int i = 22; i < 26; i++)
            {
                tab[i] = tabtaillehauteur[compteurD];
                compteurD++;// le compteur permet d'avoir un autre "indicateur" que i
            }
            int compteurE = 54; //creér le tableau a partir de la matrice image, permet de parcourir le tableau de la case 54 jusqu'a la fin
            for(int i = image.GetLength(0)-1; i >= 0; i--) //parcours la matrice dans le même ordre que le constructeur
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    tab[compteurE] = Convert.ToByte(image[i, j].Rouge);
                    tab[compteurE + 1] = Convert.ToByte(image[i, j].Vert);
                    tab[compteurE + 2] = Convert.ToByte(image[i, j].Bleu);
                    compteurE += 3; // permet de passer au pixel suivant
                }
            }
            File.WriteAllBytes(file, tab);
        }

        /// <summary>
        /// permet de faire une nuance de gris de l'image
        /// </summary>
        public void Nuance_de_Gris()
        {
            for (int i = 0; i < image.GetLength(0); i++) //parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int somme = image[i, j].Rouge + image[i, j].Bleu + image[i, j].Vert; //permet de faire la somme des 3 couleurs pour pouvoir faire la nuance de gris
                    int resultat = somme / 3; //permet de faire la moyenne des 3 couleurs pour pouvoir faire la nuance de gris
                    image[i, j].Vert = resultat; //associe chaque couleurs a la nuance de gris
                    image[i, j].Rouge = resultat;
                    image[i, j].Bleu = resultat;
                }
            }
        }

        /// <summary>
        /// permet de changer la photo en noir et blanc
        /// </summary>
        public void Noir_et_blanc()
        {
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    int somme = image[i, j].Rouge + image[i, j].Bleu + image[i, j].Vert;
                    int resultat = somme / 3; //comme la fonctin nuance de gris, permet de faire la moyenne des 3 couleurs
                    if(resultat >= 128) //associe la couleur noirs si la moyenne des 3 couleurs est au dessus de 128
                    {
                        image[i, j].Vert = 255;
                        image[i, j].Rouge = 255;
                        image[i, j].Bleu = 255;
                    }
                    else //associe la couleur blanche si la moyenne des 3 couleurs est en dessous de 128
                    {
                        image[i, j].Vert = 0;
                        image[i, j].Rouge = 0;
                        image[i, j].Bleu = 0;
                    }
                }
            }
        }

        /// <summary>
        /// permet d'effectuer une ou plusieurs rotation de 90°
        /// </summary>
        /// <param name="valeur">
        /// la valeur 1 fait une rotation a 90°, la rotation en fait 2 donc 180°, la rotation 3 fait 270°
        /// </param>
        public void Rotation(int valeur)
        {
            if (valeur == 1)// 90°
            {
                int l = 0;
                Pixel[,] imagetemp = new Pixel[largeur, hauteur]; //creer une matrice temporaire pour pouvoir faire la rotation
                for (int j = imagetemp.GetLength(1) - 1; j >= 0; j--)
                {
                    int k = 0;
                    for (int i = 0; i < imagetemp.GetLength(0); i++)
                    {
                        imagetemp[i, j] = image[l, k];// permet de stocker l'image "rotationné" dans la matrice temporaire (donc sans modifier la matrice image)
                        k++;
                    }
                    l++;
                }
                this.image = new Pixel[largeur, hauteur]; //permet d'intervertir la largeur et la hauteur, car il y a une rotation a 90°
                for(int i = 0; i < imagetemp.GetLength(0); i++) //percours la matrice temporaire
                {
                    for (int j = 0; j < imagetemp.GetLength(1); j++)
                    {
                        image[i, j] = imagetemp[i, j]; //permet d'égaliser la matrice temporaire et la matrice image
                    }
                }
                largeur = imagetemp.GetLength(1); // permet de modifier la largeur et la hauteur de la matrice image, qui sont modifiés par la rotation
                hauteur = imagetemp.GetLength(0);
            }
            if (valeur == 2)// 180°,refait la meme chose que rotatino a 90° 2 fois
            {
                int l = 0;
                Pixel[,] imagetemp = new Pixel[largeur, hauteur];
                for (int j = imagetemp.GetLength(1) - 1; j >= 0; j--)
                {
                    int k = 0;
                    for (int i = 0; i < imagetemp.GetLength(0); i++)
                    {
                        imagetemp[i, j] = image[l, k];
                        k++;
                    }
                    l++;
                }
                int n = 0;
                Pixel[,] imagetempb = new Pixel[hauteur, largeur];
                for (int j = imagetempb.GetLength(1) - 1; j >= 0; j--)
                {
                    int m = 0;
                    for (int i = 0; i < imagetempb.GetLength(0) ; i++)
                    {
                        imagetempb[i, j] = imagetemp[n, m];
                        m++;
                    }
                    n++;
                }
                this.image = new Pixel[hauteur, largeur];
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        image[i, j] = imagetempb[i, j];
                    }
                }
            }
            if (valeur == 3)//270°, fait la meme chose que rotation a 90° mais dans l'autre sens
            {
                int l = 0;
                Pixel[,] imagetemp = new Pixel[largeur, hauteur];
                for (int j = 0; j < imagetemp.GetLength(1); j++)
                {
                    int k = 0;
                    for (int i = 0; i < imagetemp.GetLength(0); i++)
                    {
                        imagetemp[i, j] = image[l, k];
                        k++;
                    }
                    l++;
                }
                this.image = new Pixel[largeur, hauteur];
                for (int i = 0; i < image.GetLength(0); i++)
                {
                    for (int j = 0; j < image.GetLength(1); j++)
                    {
                        image[i, j] = imagetemp[i, j];
                    }
                }
                largeur = imagetemp.GetLength(1);
                hauteur = imagetemp.GetLength(0);
            }
        }

        /// <summary>
        /// permet d'effectuer la fonction miroir
        /// </summary>
        public void Miroir()
        {
            Pixel[,] imagetemp = new Pixel[hauteur, largeur]; // creer la matrice temporaire pour pouvoir faire le miroir
            for (int i = 0; i < imagetemp.GetLength(0); i++) //permet de parourir la matrice temporaire
            {
                int k = 0;
                for (int j = imagetemp.GetLength(1) -1 ; j >= 0; j--) //permet de faire l'effet miroir
                {
                    imagetemp[i, j] = image[i, k];
                    k++;
                }
            }
            this.image = new Pixel[hauteur, largeur];
            for (int i = 0; i < image.GetLength(0); i++) //permet d'éagaliser la matrice temporaire qui contient le miroir a la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = imagetemp[i, j];
                }
            }
        }

        /// <summary>
        /// permet d'agrandir l'image par 2
        /// </summary>
        public void Agrandir()
        {
            Pixel[,] imagetemp = new Pixel[hauteur * 2, largeur * 2]; //créer une matrice temporaire de taille 2 fois plus grande que la matrice image
            int l = 0;
            for (int i = 0; i < image.GetLength(0); i++)
            {
                int k = 0; //revient a 0 car il parcours les collonnes
                for (int j = 0; j < image.GetLength(1); j++) //parcours une case de image, pour remplir 4 cases de imagetemp
                {
                    imagetemp[l, k] = image[i, j]; //ces 4 lignes permettent de remplir les 4 cases
                    imagetemp[l, k + 1] = image[i, j];
                    imagetemp[l + 1, k] = image[i, j];
                    imagetemp[l + 1, k + 1] = image[i, j];
                    k += 2; // +2 car quand j augmente de 1, k doit augmenter de 2 pour remplir imagetemp 
                }
                l += 2; // +2 car quand i augmente de 1, l doit augmenter de 2 pour remplir imagetemp

            }
            this.image = new Pixel[hauteur * 2, largeur * 2]; //boucle qui va associer imagetemp a image
            for (int i = 0; i < imagetemp.GetLength(0); i++)
            {
                for (int j = 0; j < imagetemp.GetLength(1); j++)
                {
                    image[i, j] = imagetemp[i, j];
                }
            }
            largeur = imagetemp.GetLength(1); //modifie la taille de la largeur car celle ci augmente
            hauteur = imagetemp.GetLength(0); //modifie la taille de la hauteur car celle ci augmente
            this.taillefichier = (hauteur * largeur * 3) + 54; //modifie la taille du fichier car celle ci augmente
        }

        /// <summary>
        /// permet de retrecir l'image de 2
        /// </summary>
        public void Retrecir()
        {
            double Hau = Math.Truncate(Convert.ToDouble(hauteur / 2)); //permet de modifier la taille de imagetemp, j utilise math.truncate pour prendre ne compte le cas où la taille du fichier est impaire
            double Lar = Math.Truncate(Convert.ToDouble(largeur / 2));
            int Haute = Convert.ToInt32(Hau); //permet de revenir en int
            int Large = Convert.ToInt32(Lar);
            Pixel[,] imagetemp = new Pixel[Haute, Large]; //creer la matrice temporaire que je vais remplir
            int l = 0;
            for (int i = 0; i < imagetemp.GetLength(0); i++)
            {
                int k = 0; //revient a 0 car il parcours les collonnes
                for (int j = 0; j < imagetemp.GetLength(1); j++) //parcours une case de imagetemp, et va prendre une case "parmis 4" de image
                {
                    imagetemp[i, j] = image[l, k];
                    k += 2; // +2 car quand j augmente de 1, k doit augmenter de 2 pour remplir imagetemp 
                }
                l += 2; // +2 car quand i augmente de 1, l doit augmenter de 2 pour remplir imagetemp

            }
            this.image = new Pixel[Haute, Large]; //boucle qui va associer image a imagetemp
            for (int i = 0; i < imagetemp.GetLength(0); i++)
            {
                for (int j = 0; j < imagetemp.GetLength(1); j++)
                {
                    image[i, j] = imagetemp[i, j];
                }
            }
            largeur = imagetemp.GetLength(1); //modifie la taille de la largeur car celle ci augmente
            hauteur = imagetemp.GetLength(0); //modifie la taille de la hauteur car celle ci augmente
            this.taillefichier = (hauteur * largeur * 3) + 54; //modifie la taille du fichier car celle ci augmente
        }

        /// <summary>
        /// fonction qui permet de traiter le cas où, une fois l'image retrécit, l'image n'a pas une taille qui est un multiple de 4
        /// la fonction permet de faire devenir l'image, avec une taille multiple de 4,  en rajoutant des cases rempli avec des zeros
        /// </summary>
        public void FonctionPourRetrecir()
        {
            int reste = largeur % 4; //teste si la largeur est multiple de 4
            if(reste != 0)// si la largeur n'est pas un multiple de 4
            {
                int compteur = 0;
                while (reste != 0)//tant que la largeur n'est pas un multiple de 4
                {
                    largeur++;
                    reste = largeur % 4;
                    compteur++;//le compteur permet de savoir combein de colonne il faudra rajouter
                }
                Pixel[,] temp = new Pixel[hauteur, largeur]; //prend en compte la nouvelle largeur
                for (int a = 0; a < temp.GetLength(0); a++) // permet de l'initialiser a 0
                {
                    for (int b = 0; b < temp.GetLength(1); b++)
                    {
                        temp[a, b] = new Pixel(0, 0, 0);// permet remplir toutes les colonnes avec des pixels noirs
                    }
                }
                for (int i = 0; i < hauteur; i++)//permet de remplir la matrice temp, avec les pixels de la matrice image
                {
                    for (int j = 0; j < largeur - compteur; j++)
                    {
                        temp[i, j] = image[i, j];// donc les colonnes non remplit par la matrice images seront remplis de noir (grace a la boucle précédente)
                    }
                }
                this.image = new Pixel[hauteur, largeur];
                for (int i = 0; i < temp.GetLength(0); i++)//permet d'égaliser la matrice image avec la matrice temps
                {
                    for (int j = 0; j < temp.GetLength(1); j++)
                    {
                        image[i, j] = temp[i, j];
                    }
                }
                largeur = temp.GetLength(1); //modifie la taille de la largeur car celle ci augmente
                hauteur = temp.GetLength(0); //modifie la taille de la hauteur car celle ci augmente
                this.taillefichier = (hauteur * largeur * 3) + 54; //modifie la taille du fichier car celle ci augmente
            }
        }

        /// <summary>
        /// fonction utile pour les matrice de convolution, en l'occurence, cette fonction permet d'effectuer la multiplication entre les 2 matrices rentrés en parametre
        /// </summary>
        /// <param name="mat1">
        /// mat1 est la matrice de pixel qui contient le contour du pixel sur lequel on veut applique le filtre
        /// </param>
        /// <param name="mat2">
        /// mat2 est la matrice de convolution
        /// </param>
        /// <param name="filtre">
        /// filtre permet juste de différencier flou des autres filtres, car on doit effectuer une division par 9 pouir le filtre flou
        /// </param>
        /// <returns>
        /// retourne le pixel "flitré"
        /// </returns>
        public Pixel MultiplicationMatriceConv(Pixel[,] mat1, int[,] mat2, string filtre) 
        {
            int sommeRouge = 0; //j'initialise mes variables
            int sommeBleu = 0;
            int sommeVert = 0;
            for (int i = 0; i < mat2.GetLength(0); i++)
            {
                for(int j = 0; j < mat2.GetLength(1); j++) //permet de parcourir les deux matrices (car de même taille)
                {
                    sommeRouge += mat1[i, j].Rouge * mat2[i, j]; //permet de faire la multiplication des deux matrices 
                    sommeBleu += mat1[i, j].Bleu * mat2[i, j]; //on stock le résultat dans une somme
                    sommeVert += mat1[i, j].Vert * mat2[i, j];
                }
            }
            if (filtre == "flou") //car c'est différent pour le flou, on doit diviser chaque somme par 9
            {
                sommeRouge = sommeRouge / 9;
                sommeBleu = sommeBleu / 9;
                sommeVert = sommeVert / 9;
            }
            if (sommeRouge < 0) // cela permet d'avoir un pixel qui est compris entre 0 et 255, dans le cas contraire il ne pourra pas être lu
            {
                sommeRouge = 0;
            }
            if (sommeBleu < 0)
            {
                sommeBleu = 0;
            }
            if (sommeVert < 0)
            {
                sommeVert = 0;
            }
            if (sommeRouge > 255)
            {
                sommeRouge = 255;
            }
            if (sommeBleu > 255)
            {
                sommeBleu = 255;
            }
            if (sommeVert > 255)
            {
                sommeVert = 255;
            }
            Pixel resultat = new Pixel(sommeRouge, sommeVert, sommeBleu); //permet de créer le pixel après avoir fait la multiplication
            return resultat; //retourne le pixel qui va etre utilisé dans la matrice "filtré"
        }

        /// <summary>
        /// permet de créer la matrice qui comporte le pixel sur lequel on applique le filtre
        /// </summary>
        /// <param name="i">
        /// est le rang i du pixel que l'on veut "filtré", et donc trouver sa matrice contour
        /// </param>
        /// <param name="j">
        /// est le rang j du pixel que l'on veut "filtré", et donc trouver sa matrice contour
        /// </param>
        /// <returns>
        /// retourne la matrice contour du pixel rang [i,j] que l'on veut "filtré"
        /// </returns>
        public Pixel[,] CreerLaMatriceContour(int i, int j) 
        {
            Pixel[,] resultat = new Pixel[3, 3]; //permet d'initialsier la matrice contour
            for(int a = 0; a < resultat.GetLength(0); a++) // permet de l'initialiser a 0
            {
                for (int b = 0; b < resultat.GetLength(1); b++)
                {
                    resultat[a, b] = new Pixel(0, 0, 0);
                }
            }
            // le if qui suit prend en comtpte tous les cas qui sont une "exception" (les cotés et les bords), dans ces cas la ont devra faire apparaitre des zéros pour créer la matrice contour
            if (((i == 0) && (j == 0)) || ((i == 0) && (j != 0) && (j != image.GetLength(1) - 1)) || ((i == image.GetLength(0) - 1) && j == 0) || ((i == image.GetLength(0) - 1) && (j != 0) && (j != image.GetLength(1) - 1)) || ((i == image.GetLength(0) - 1) && (j == image.GetLength(1) - 1)) || ((j == image.GetLength(1) - 1) && (i != 0) && (i != image.GetLength(0) - 1)) || ((j == 0) && (i != 0) && (i != image.GetLength(0) - 1)) || ((i == 0) && (j == image.GetLength(1) - 1)))
            {
                if (i == 0 && j == 0) //prend le cas où le pixel est en haut a gauche
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcouri la matrice resultat
                        {
                            if (c == 0 || d == 0) { }
                            else
                            {
                                resultat[c, d].Rouge = image[c - 1, d - 1].Rouge; //formule qui remplit la matrice resultat avec la matrice image dans ce cas
                                resultat[c, d].Vert = image[c - 1, d - 1].Vert;
                                resultat[c, d].Bleu = image[c - 1, d - 1].Bleu;
                            }
                        }
                    }
                }

                if ((i == 0) && (j != 0) && (j != image.GetLength(1) - 1)) //prend le cas où le pixel est en haut mais par sur les cotés
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcouri la matrice resultat
                        {
                            if (c == 0) { }
                            else
                            {
                                resultat[c, d].Rouge = image[c - 1, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[c - 1, j - 1 + d].Vert;
                                resultat[c, d].Bleu = image[c - 1, j - 1 + d].Bleu;
                            }
                        }
                    }
                }

                if ((i == image.GetLength(0) - 1) && j == 0) //prend le cas où le pixel est en bas à gauche
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (c == 2 || d == 0) { }
                            else
                            {
                                resultat[c, d].Rouge = image[i - 1 + c, d - 1].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[i - 1 + c, d - 1].Vert;
                                resultat[c, d].Bleu = image[i - 1 + c, d - 1].Bleu;
                            }
                        }
                    }
                }

                if ((i == image.GetLength(0) - 1) && (j != 0) && (j != image.GetLength(1) - 1))// prend le cas où le pixel est sur le bas mais pas sur les cotés
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (c == 2) { }
                            else
                            {
                                resultat[c, d].Rouge = image[i - 1 + c, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[i - 1 + c, j - 1 + d].Vert;
                                resultat[c, d].Bleu = image[i - 1 + c, j - 1 + d].Bleu;
                            }
                        }
                    }
                }

                if ((i == image.GetLength(0) - 1) && (j == image.GetLength(1) - 1)) //prend le cas où le pixel est en bas a droite
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (c == 2 || d == 2) { }
                            else
                            {
                                resultat[c, d].Rouge = image[i - 1 + c, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[i - 1 + c, j - 1 + d].Vert;
                                resultat[c, d].Bleu = image[i - 1 + c, j - 1 + d].Bleu;
                            }
                        }
                    }
                }

                if ((j == image.GetLength(1) - 1) && (i != 0) && (i != image.GetLength(0) - 1))// prend le cas où le pixel est sur le coté droit mais pas sur les cotés
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (d == 2) { }
                            else
                            {
                                resultat[c, d].Rouge = image[i - 1 + c, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[i - 1 + c, j - 1 + d].Vert;
                                resultat[c, d].Bleu = image[i - 1 + c, j - 1 + d].Bleu;
                            }
                        }
                    }
                }

                if ((j == 0) && (i != 0) && (i != image.GetLength(0) - 1))// prend le cas où le pixel est sur le coté gauche mais pas sur les cotés
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (d == 0) { }
                            else
                            {
                                resultat[c, d].Rouge = image[i - 1 + c, d - 1].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[i - 1 + c, d - 1].Vert;
                                resultat[c, d].Bleu = image[i - 1 + c, d - 1].Bleu;
                            }
                        }
                    }
                }

                if (i == 0 && (j == image.GetLength(1) - 1)) //prend le cas où le pixel est en haut a droite
                {
                    for (int c = 0; c < resultat.GetLength(0); c++)
                    {
                        for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                        {
                            if (c == 0 || d == 2) { }
                            else
                            {
                                resultat[c, d].Rouge = image[c - 1, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                                resultat[c, d].Vert = image[c - 1, j - 1 + d].Vert;
                                resultat[c, d].Bleu = image[c - 1, j - 1 + d].Bleu;
                            }
                        }
                    }
                }
            }
            else // prend le cas où il n'y a pas besoin de rejouter des 0
            {
                for (int c = 0; c < resultat.GetLength(0); c++)
                {
                    for (int d = 0; d < resultat.GetLength(1); d++) //permet de parcourir la matrice resultat
                    {
                        resultat[c, d].Rouge = image[i - 1 + c, j - 1 + d].Rouge; //formule qui remplit la matrice resultat avecla matrice image dans ce cas
                        resultat[c, d].Vert = image[i - 1 + c, j - 1 + d].Vert;
                        resultat[c, d].Bleu = image[i - 1 + c, j - 1 + d].Bleu;
                    }
                }
            }
            return resultat; 
        }

        /// <summary>
        /// permet d'effectuer le filtre contour, à l'aide des fonctions MultiplicationMatriceConv et CréerLaMatriceContour
        /// </summary>
        public void DetectionDeContour()
        {
            int[,] mat2 = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } }; // matrice de convolution
            Pixel[,] temp = new Pixel[image.GetLength(0), image.GetLength(1)]; // créer une matrice de pixel temporaire qui servira à être rempli par les pixels modifiés par le filtre
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice temporaire
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Pixel[,] mat1 = CreerLaMatriceContour(i, j); // créer la matrice de contour du pixel
                    Pixel PxFiltrer = MultiplicationMatriceConv(mat1, mat2, "DetectionDeContour"); // multiplie la matrice contour par la matrtice de convolution et ressort un pixel
                    temp[i,j] = PxFiltrer; // égalise le pixel obtenu a la case correspondante
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = temp[i, j]; // permet d'égaliser la matrice image a celle temporaire qui stockait l'image filtré
                }
            }
        }

        /// <summary>
        /// permet d'effectuer le filtre renforcement des bords, avec la même méthode que que contour
        /// </summary>
        public void RenforcementDesBords()
        {
            int[,] mat2 = { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } }; // même méthode que précédement mais avec une matrice de convolution différrente
            Pixel[,] temp = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice temporaire
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Pixel[,] mat1 = CreerLaMatriceContour(i, j);
                    Pixel PxFiltrer = MultiplicationMatriceConv(mat1, mat2, "renforcementdesbords");
                    temp[i, j] = PxFiltrer;
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = temp[i, j];
                }
            }
        }

        /// <summary>
        /// permet d'effectuer le filtre repoussage, avec la même méthode que contour
        /// </summary>
        public void Repoussage()
        {
            int[,] mat2 = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } }; // même méthode que précédement mais avec une matrice de convolution différrente
            Pixel[,] temp = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice temporaire
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Pixel[,] mat1 = CreerLaMatriceContour(i, j);
                    Pixel PxFiltrer = MultiplicationMatriceConv(mat1, mat2, "repoussage");
                    temp[i, j] = PxFiltrer;
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = temp[i, j];
                }
            }
        }

        /// <summary>
        /// permet d'effectuer le filtre flou, avec la même méthode que contour
        /// </summary>
        public void Flou()
        {
            int[,] mat2 = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } }; // même méthode que précédement mais avec une matrice de convolution différrente
            Pixel[,] temp = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice temporaire
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Pixel[,] mat1 = CreerLaMatriceContour(i, j);
                    Pixel PxFiltrer = MultiplicationMatriceConv(mat1, mat2, "flou");
                    temp[i, j] = PxFiltrer;
                }
            }
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = temp[i, j];
                }
            }
        }

        /// <summary>
        /// permet de créer la fractale (à partir de l'image blanche créer dans blanche)
        /// </summary>
        public void Fractale()
        {
            int iterationmax = 1000;
            double x1 = -2.1; // l ensemble de mandelbrot est compris entre -2,1 et 0,6 sur l'axe des abscisse
            double x2 = 0.6;
            double y1 = -1.2; //l'ensemble de mandelbrot sur l'axe des ordonné
            double y2 = 1.2;
            double zoomx = image.GetLength(0) / (x2 - x1);
            double zoomy = image.GetLength(1) / (y2 - y1);
            for(int i = 0; i <  image.GetLength(0); i++) //parours la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    double cr = i / zoomx + x1; //permet de créer la partie réel initiale 
                    double ci = j / zoomy + y1; //permet de créer la partie imaginaire initiale
                    double zr = 0; 
                    double zi = 0;
                    int iteration = 0;
                    while ((zr*zr + zi*zi < 4) && (iteration < iterationmax)) //donnné de l'algorithme de mandelbrot
                    {
                        double temp = zr; // permet de stocker la valeur
                        zr = (zr * zr) - (zi * zi) + cr; // permet de faire Z^2 + c pour la partie réel
                        zi = (2 * zi * temp) + ci; // permet de faire Z^2 + c pour la partie imaginaire
                        iteration++;
                    }
                    if (iteration == iterationmax)
                    {
                        image[i, j].Rouge = 0; //permet de créer la fractale de couleur noir
                        image[i, j].Vert = 0;
                        image[i, j].Bleu = 0;
                    }
                }
            }
        }

        /// <summary>
        /// fonction qui permet de faire l'histogramme
        /// c'est a dire pour chaque colonne, regarder combien de rouge, vert et bleu contient chaque pixel de la colonne
        /// ensuite je fais la somme des valeurs de rouge vert et bleu
        /// et selon leur pourcentage, je créer une nouvelle matrice où la repartition des couleurs correspond à leur pourcentage
        /// </summary>
        public void Histogramme()
        {
            Pixel[,] temp = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < image.GetLength(0); i++)//parcour la matrice temporaire
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    temp[i,j] = new Pixel(0, 0, 0);// permet d'initialiser la matrice temporaire
                }
            }
            for (int j = 0; j < image.GetLength(1); j++)//parcour les colonnes
            {
                double sommerouge = 0; //initialise les vairable a 0 a chaque changement de colonne
                double sommebleu = 0;
                double sommevert = 0;
                for (int i = 0; i < image.GetLength(0); i++)//parcours les lignes de chaque colonne
                {
                    sommerouge = sommerouge + image[i, j].Rouge;//permet d'aditionner les "valuer" de rouge de chaque pixel
                    sommebleu = sommebleu + image[i, j].Bleu;
                    sommevert = sommevert + image[i, j].Vert;
                }
                double sommetotal = sommerouge + sommevert + sommebleu + 1;// permet de faire la somme de chacune des sommmes, afin de faire des pourcentages par la suite, on rajout 1 pour ne jamais diviser par 0
                sommerouge = (sommerouge * 100) / sommetotal ; //permet d'obtenir le pourcentage de rouge parmis la somme des rouge, vert et bleu
                sommebleu = (sommebleu * 100) / sommetotal;
                sommevert = (sommevert * 100) / sommetotal;
                sommerouge = Math.Truncate(sommerouge);//permet d'obtenir un nombre entier
                sommebleu = Math.Truncate(sommebleu);
                sommevert = Math.Truncate(sommevert);
                double bornerouge = (sommerouge * hauteur) / 100;// permet d'obtenir le nombre de pixel rouge que je dois remplir dans la colonne, à partir du pourcentage calculé précédement
                double bornevert = (sommevert * hauteur) / 100;
                double bornebleu = (sommebleu * hauteur) / 100;
                bornerouge = Math.Truncate(bornerouge);//permet d'obtenir un nombre entier
                bornebleu = Math.Truncate(bornebleu);
                bornevert = Math.Truncate(bornevert);
                int borneRouge = Convert.ToInt32(bornerouge); //permet de convertir les nombre de pixel en int afin de pouvoir les utiliser dans les boucles
                int borneVert = Convert.ToInt32(bornevert);
                int borneBleu = Convert.ToInt32(bornebleu);
                for (int l = 0; l < borneRouge; l++) //boulce qui permet de remplir le rouge
                {
                    temp[l, j].Rouge = 255;
                    temp[l, j].Vert = 0;
                    temp[l, j].Bleu = 0;
                }
                for (int m = borneRouge; m < borneRouge + borneVert; m++) //boucle qui permet de remplir le vert
                {
                    temp[m, j].Rouge = 0;
                    temp[m, j].Vert = 255;
                    temp[m, j].Bleu = 0;
                }
                for (int n = borneVert + borneRouge; n < temp.GetLength(0); n++)// boucle qui permet de remplir (par élimination) le bleu
                {
                    temp[n, j].Rouge = 0;
                    temp[n, j].Vert = 0;
                    temp[n, j].Bleu = 255;
                }
            }
            for(int i = 0; i < image.GetLength(0); i++)//parcour la matrice image
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    image[i, j] = temp[i, j]; //égalise les deux matrices
                }
            }
        }
    }
}
