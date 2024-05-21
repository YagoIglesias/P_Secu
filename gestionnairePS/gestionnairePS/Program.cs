/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 19.03.24
/// Description : Programme qui execute le gestionnaire de mots de passe

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gestionnairePS
{
    internal class Program
    {

        static void Main(string[] args)
        {
            /*********** tester le chiffrement et dechifrement ****************/
            /*string test = "HOla";
            PasseWord ps = new PasseWord(ps:test);
            ps.Chiffrement();
            ps.Afficher();*/
            /*char test = 'y';
            int y = Convert.ToInt32(test);
            int modulo = y % 256;
            int result = y + modulo;
            Console.WriteLine(Convert.ToChar(result));
            char todecript = Convert.ToChar(result);
            int Val = Convert.ToInt32(todecript);
            int Modulo2 = todecript % 256;
            int Res = Val - Modulo2;
            Console.WriteLine(Res);
            int test2 = modulo - Modulo2;
            Console.WriteLine(Convert.ToChar(Modulo2 - modulo));*/
            /*********** tester le chiffrement et dechifrement ****************/

            // instancier l'objet menu 
            Menu menu = new Menu();
            menu.Init();// appeller la méthode qui initialise le programme



            // pas fermer la console
            Console.ReadLine();
        }

    }
}
