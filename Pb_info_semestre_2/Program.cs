using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;


namespace Pb_info_semestre_2
{
    class Program
    {
        //objectif du TD1 : -comprendre le bitmap afin de lire et écrire une image
        //                  -comprendre le litlle endian, le pixel, structure fichier bitmap
        //finalement je pense avoir commencé a comprendre les difficultés mais je n'ai pas eu le temps de faire grand chose -> faire ce que j ai compris chez moi avant la prochaine séance et approfondir mes recherhces pour mieux comprendre

        //objectif du TD2 : mettre en place ce que j ai compris et vérifier que cela fonctionne
        //finalement : - j'ai créer les 3 fonctions MyImage, et les 2 fonction Convert, et j'ai bien compris comment fonctionne le bitmap
        //             - j'ai vérifier que mes fonctions fonctionnent
        //pour la prochaine fois : faire la fonction From_Image_To_File et faire les fonctions du td2 (j'ai deja commencé a les regarder, je dois les mettres en forme)
        
        //il y aun pb au niveau de ma conversion, il suffit juste que j'inverse le tableau de sortie
        //j ai finis la fonction from image to file
        //objectif^pour la prochaine fois: finir toutes les fonctions que j'ai de retard

        //j ai corrigé le probleme au niveau de la conversion
        //j'ai rencontré un pb avec mon ordinateur, je n'arrive plus a générer mon programme, j'essaye donc d'avancer mais je ne peux pas tester ce que je fais
        //je fais donc mes fonctions au brouillon
        //mon ordi a remarcher 2h plus tard apres une reinitiaisation
        //j ai corrigé mes fonctions from image to file et mon constructeur
        //j ai pu les tester et elles fonctionnent avec Exotest3
        //je recopie mes fonctions faites au brouillon
        //je verifie si elles fonctionnent avec Exotest4
        //il me manque les fonctions reduire et agrandir, je les ai faites au brouillon mais je n ai pas eu le temps de les tester
        //objectif pour le prochain td, rattrapper mon retard

        /// <summary>
        /// permet de choisir quel exo on veut effectuer, on retire les // pour effectuer l'exercice souhaité
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Programme();
            //Exotest1();
            //Exotest2();
            //Exotest3();
            //Exotest4();
            //Exotest5();
            //Exotest6();
            //Exotest7();

            Console.ReadKey();
        }

        /// <summary>
        /// permet de faire le programme avec la photo coco
        /// </summary>
        static void Coco()
        {
            MyImage image = new MyImage("coco.bmp");
            Console.WriteLine("Que souhaitez vous faire avec votre photo ?");
            Console.WriteLine("Tapez 1 pour faire une nuance de gris ");
            Console.WriteLine("Tapez 2 pour mettre la photo en noir et blanc");
            Console.WriteLine("Tapez 3 pour faire une rotation");
            Console.WriteLine("Tapez 4 pour faire un effet miroir");
            Console.WriteLine("Tapez 5 pour agrandir la photo");
            Console.WriteLine("Tapez 6 pour retrecir la photo");
            Console.WriteLine("Tapez 7 pour appliquer le filtre detection de contour");
            Console.WriteLine("Tapez 8 pour appliquer le filtre renforcement des bords");
            Console.WriteLine("Tapez 9 pour appliquer le filtre repoussage");
            Console.WriteLine("Tapez 10 pour appliquer le filtre flou");
            Console.WriteLine("Tapez 11 pour faire l'histogramme de la photo");
            string choix = Console.ReadLine();
            if (choix == "1")
            {
                image.Nuance_de_Gris();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "2")
            {
                image.Noir_et_blanc();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "3")
            {
                Console.WriteLine("Tapez 1 pour faire une rotation de 90, 2 pour 180° et 3 pour 270° ");
                int n = Convert.ToInt32(Console.ReadLine());
                image.Rotation(n);
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "4")
            {
                image.Miroir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "5")
            {
                image.Agrandir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "6")
            {
                image.Retrecir();
                image.FonctionPourRetrecir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "7")
            {
                image.DetectionDeContour();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "8")
            {
                image.RenforcementDesBords();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "9")
            {
                image.Repoussage();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "10")
            {
                image.Flou();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "11")
            {
                image.Histogramme();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }

        }
        /// <summary>
        /// permet de faire le programme avec la photo lac
        /// </summary>
        static void Lac()
        {
            MyImage image = new MyImage("lac.bmp");
            Console.WriteLine("Que souhaitez vous faire avec votre photo ?");
            Console.WriteLine("Tapez 1 pour faire une nuance de gris ");
            Console.WriteLine("Tapez 2 pour mettre la photo en noir et blanc");
            Console.WriteLine("Tapez 3 pour faire une rotation");
            Console.WriteLine("Tapez 4 pour faire un effet miroir");
            Console.WriteLine("Tapez 5 pour agrandir la photo");
            Console.WriteLine("Tapez 6 pour retrecir la photo");
            Console.WriteLine("Tapez 7 pour appliquer le filtre detection de contour");
            Console.WriteLine("Tapez 8 pour appliquer le filtre renforcement des bords");
            Console.WriteLine("Tapez 9 pour appliquer le filtre repoussage");
            Console.WriteLine("Tapez 10 pour appliquer le filtre flou");
            Console.WriteLine("Tapez 11 pour faire l'histogramme de la photo");
            string choix = Console.ReadLine();
            if (choix == "1")
            {
                image.Nuance_de_Gris();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "2")
            {
                image.Noir_et_blanc();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "3")
            {
                Console.WriteLine("Tapez 1 pour faire une rotation de 90, 2 pour 180° et 3 pour 270° ");
                int n = Convert.ToInt32(Console.ReadLine());
                image.Rotation(n);
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "4")
            {
                image.Miroir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "5")
            {
                image.Agrandir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "6")
            {
                image.Retrecir();
                image.FonctionPourRetrecir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "7")
            {
                image.DetectionDeContour();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "8")
            {
                image.RenforcementDesBords();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "9")
            {
                image.Repoussage();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "10")
            {
                image.Flou();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "11")
            {
                image.Histogramme();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
        }
        /// <summary>
        /// permet de faire le programme avec la photo lena
        /// </summary>
        static void Lena()
        {
            MyImage image = new MyImage("lena.bmp");
            Console.WriteLine("Que souhaitez vous faire avec votre photo ?");
            Console.WriteLine("Tapez 1 pour faire une nuance de gris ");
            Console.WriteLine("Tapez 2 pour mettre la photo en noir et blanc");
            Console.WriteLine("Tapez 3 pour faire une rotation");
            Console.WriteLine("Tapez 4 pour faire un effet miroir");
            Console.WriteLine("Tapez 5 pour agrandir la photo");
            Console.WriteLine("Tapez 6 pour retrecir la photo");
            Console.WriteLine("Tapez 7 pour appliquer le filtre detection de contour");
            Console.WriteLine("Tapez 8 pour appliquer le filtre renforcement des bords");
            Console.WriteLine("Tapez 9 pour appliquer le filtre repoussage");
            Console.WriteLine("Tapez 10 pour appliquer le filtre flou");
            Console.WriteLine("Tapez 11 pour faire l'histogramme de la photo");
            string choix = Console.ReadLine();
            if (choix == "1")
            {
                image.Nuance_de_Gris();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "2")
            {
                image.Noir_et_blanc();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "3")
            {
                Console.WriteLine("Tapez 1 pour faire une rotation de 90, 2 pour 180° et 3 pour 270° ");
                int n = Convert.ToInt32(Console.ReadLine());
                image.Rotation(n);
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "4")
            {
                image.Miroir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "5")
            {
                image.Agrandir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "6")
            {
                image.Retrecir();
                image.FonctionPourRetrecir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "7")
            {
                image.DetectionDeContour();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "8")
            {
                image.RenforcementDesBords();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "9")
            {
                image.Repoussage();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "10")
            {
                image.Flou();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "11")
            {
                image.Histogramme();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
        }
        /// <summary>
        /// permet de faire le programme avec la photo test
        /// </summary>
        static void Test()
        {
            MyImage image = new MyImage("Test.bmp");
            Console.WriteLine("Que souhaitez vous faire avec votre photo ?");
            Console.WriteLine("Tapez 1 pour faire une nuance de gris ");
            Console.WriteLine("Tapez 2 pour mettre la photo en noir et blanc");
            Console.WriteLine("Tapez 3 pour faire une rotation");
            Console.WriteLine("Tapez 4 pour faire un effet miroir");
            Console.WriteLine("Tapez 5 pour agrandir la photo");
            Console.WriteLine("Tapez 6 pour retrecir la photo");
            Console.WriteLine("Tapez 7 pour appliquer le filtre detection de contour");
            Console.WriteLine("Tapez 8 pour appliquer le filtre renforcement des bords");
            Console.WriteLine("Tapez 9 pour appliquer le filtre repoussage");
            Console.WriteLine("Tapez 10 pour appliquer le filtre flou");
            Console.WriteLine("Tapez 11 pour faire l'histogramme de la photo");
            string choix = Console.ReadLine();
            if (choix == "1")
            {
                image.Nuance_de_Gris();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "2")
            {
                image.Noir_et_blanc();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "3")
            {
                Console.WriteLine("Tapez 1 pour faire une rotation de 90, 2 pour 180° et 3 pour 270° ");
                int n = Convert.ToInt32(Console.ReadLine());
                image.Rotation(n);
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "4")
            {
                image.Miroir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "5")
            {
                image.Agrandir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "6")
            {
                image.Retrecir();
                image.FonctionPourRetrecir();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "7")
            {
                image.DetectionDeContour();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "8")
            {
                image.RenforcementDesBords();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "9")
            {
                image.Repoussage();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "10")
            {
                image.Flou();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
            if (choix == "11")
            {
                image.Histogramme();
                image.From_Image_To_File("salut.bmp");
                Process.Start("salut.bmp");
            }
        }
        /// <summary>
        /// permet de faire le programme pour créer un fractale
        /// </summary>
        static void Fractale()
        {
            Blanche frac1 = new Blanche(270, 240, "fractale.bmp");// permet de créer une image de taille 270*240
            MyImage image = new MyImage("fractale.bmp"); //permet de se placer dans la classe MyImage pour povoir être traité
            image.Fractale();//effectue la fractale
            image.From_Image_To_File("test3.bmp");
            Process.Start("test3.bmp"); //affiche la fractale
        }
        /// <summary>
        /// permet de faire le programme pour l'utilisateur
        /// </summary>
        static void Programme()
        {
            Console.WriteLine("Bienvenue, que souhaitez vous faire ?");
            string refaire = "oui";
            while (refaire == "oui")
            {
                Console.WriteLine("Tapez 1 pour utiliser coco, 2 pour le lac, 3 pour lena, 4 pour test et tapez 5 si vous voulez faire un fractale->");
                string numerophoto = Console.ReadLine();
                if (numerophoto == "1")
                {
                    Coco();
                }
                if (numerophoto == "2")
                {
                    Lac();
                }
                if (numerophoto == "3")
                {
                    Lena();
                }
                if (numerophoto == "4")
                {
                    Test();
                }
                if (numerophoto == "5")
                {
                    Fractale();
                }
                Console.WriteLine("Voulez vous continuer ? Tapez oui ou non");
                refaire = Console.ReadLine();
            }
        }

        //les fonctions suivante m'ont permis (et permettent) de tester toutes mes fonctions, pour voir si elles fonctionnaient(fonctionnnent) correctement

        /// <summary>
        /// permet de tester la fonction donnée au premier TD
        /// </summary>
        static void Exotest1()
        {
            byte[] myfile = File.ReadAllBytes("Test.bmp");
            Process.Start("Test.bmp");
            Console.WriteLine("Header \n");
            for (int i = 0; i < 14; i++)
                Console.Write(myfile[i] + " ");
            Console.WriteLine("\n HEADER INFO \n\n");
            for (int i = 14; i < 54; i++)
                Console.Write(myfile[i] + " ");
            Console.WriteLine("\n\n IMAGE \n");
            for (int i = 54; i < myfile.Length; i++)
            {
                Console.Write(myfile[i] + "\t");
            }

            File.WriteAllBytes("Sortie.bmp", myfile);
            Process.Start("Sortie.bmp");
            Console.ReadLine();
        }

        /// <summary>
        /// permet de tester la fonction
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        static int Convertir_Endian_to_Int(byte[] tab)
        {
            int valeurint = 0;
            int puissance = 1;
            for (int i = 0; i < 4; i++)
            {
                valeurint += (tab[i] * puissance);
                puissance = puissance * 256;
            }
            return valeurint;
        }
        /// <summary>
        /// permet de tester la fonction
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        static byte[] Convertir_int_to_endian(int val)
        {
            byte[] tab = new byte[4];
            byte[] tabtemp = new byte[4];
            if (val <= 255)
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
                double quotient3b = Math.Truncate(quotient3);
                double reste3 = reste2 - (quotient3b * 256);
                tabtemp[0] = Convert.ToByte(reste3);
                tabtemp[1] = Convert.ToByte(quotient3b);
                tabtemp[2] = Convert.ToByte(quotient2b);
                tabtemp[3] = Convert.ToByte(quotient3b);
            }
            /*int k = 0;
            for (int i = tabtemp.Length - 1; i >= 0; i--) //parcourt le tableau de la fin jusqu'au debut
            {
                tab[k] = tabtemp[i]; //permet de créer le nouveau tableau
                k++;
            }*/
            int k = 0;
            for (int i = 0; i < tabtemp.Length; i++)
            {
                tab[k] = tabtemp[i]; //permet de créer le nouveau tableau
                k++;
            }
            return tab;
        }
        /// <summary>
        /// permet de tester mes fonctions de conversions
        /// </summary>
        static void Exotest2()
        {
            byte[] tab1 = {20, 0, 0, 0 }; // je rentre un tableau dont je connais la conversion
            int val1 = Convertir_Endian_to_Int(tab1);
            Console.WriteLine(val1);

            int val2 = 1200; //je rentre un int dont je connais la conversion
            byte[] tab2 = Convertir_int_to_endian(val2);
            for (int i = 0; i < tab2.Length; i++) // permet d'afficher le tableau d'endian
            {
                Console.Write(tab2[i] + " ");
            }
            Console.WriteLine();

            int val3 = 20; //ici je test directement les 2 conversions
            byte[] tab3 = Convertir_int_to_endian(val3); //en effet je rentre un int, j'effectue les deux conversions, et je vérifie que le int qui ressort est le même quand entrée
            int val4 = Convertir_Endian_to_Int(tab3);
            Console.WriteLine(val4);
        }

        /// <summary>
        /// permet de tester le constructeur de MyImage, et aussi la fonction from-image-to-file
        /// </summary>
        static void Exotest3()
        {
            MyImage image = new MyImage("coco.bmp");
            image.From_Image_To_File("test2.bmp");
            Console.WriteLine("Taille du fichier : " + image.Taillefichier);
            Console.WriteLine("Taille de la Largeur : " + image.Largeur);
            Console.WriteLine("Taille de la Hauteur : " + image.Hauteur);
            Process.Start("test2.bmp");
        }

        /// <summary>
        /// permet de tester les fonctions du TD2, il suffit d'enlever les // pour tester chaque fonction
        /// </summary>
        static void Exotest4()
        {
            MyImage image = new MyImage("coco.bmp");
            Console.WriteLine("Taille du fichier : " + image.Taillefichier);
            Console.WriteLine("Taille de la Largeur : " + image.Largeur);
            Console.WriteLine("Taille de la Hauteur : " + image.Hauteur);
            Console.WriteLine("Tailledu offset : " + image.Offset);
            //image.Nuance_de_Gris();
            //image.Noir_et_blanc();
            //image.Rotation(1);
            //image.Rotation(2);
            //image.Rotation(3);
            //image.Miroir();
            //image.Agrandir();
            //image.Retrecir();
            //image.FonctionPourRetrecir();
            image.From_Image_To_File("test2.bmp");
            //Console.WriteLine("Taille du fichier : " + image.Taillefichier);
            //Console.WriteLine("Taille de la Largeur : " + image.Largeur);
            //Console.WriteLine("Taille de la Hauteur : " + image.Hauteur);
            Process.Start("test2.bmp");
        }

        /// <summary>
        /// permet de tester les fonctions du TD3, en l'occurence les filtres. De même il suffit d'enlever les // pour tester chaque fonction
        /// </summary>
        static void Exotest5()
        {
            MyImage image = new MyImage("coco.bmp");
            //image.DetectionDeContour();
            //image.RenforcementDesBords();
            //image.Repoussage();
            image.Flou();
            image.From_Image_To_File("test3.bmp");
            Process.Start("test3.bmp");
        }

        /// <summary>
        /// permet de tester la fonction fractale
        /// </summary>
        static void Exotest6()
        {
            Blanche frac1 = new Blanche(270, 240, "fractale.bmp");// permet de créer une image de taille 270*240
            MyImage image = new MyImage("fractale.bmp"); //permet de se placer dans la classe MyImage pour povoir être traité
            image.Fractale();//effectue la fractale
            image.From_Image_To_File("test3.bmp"); 
            Process.Start("test3.bmp"); //affiche la fractale

        }

        /// <summary>
        /// permet de tester ma fonction histogramme
        /// </summary>
        static void Exotest7()
        {
            MyImage image = new MyImage("coco.bmp");
            Console.WriteLine("la hauteur est : " + image.Hauteur);
            image.Histogramme();
            image.From_Image_To_File("test3.bmp");
            Process.Start("test3.bmp");
        }
    }
}
