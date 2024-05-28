/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 19.03.24
/// Description : Programme qui execute le gestionnaire de mots de passe

using System;

namespace gestionnairePS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // instancier l'objet menu 
            Menu menu = new Menu();
            menu.Init();// appeller la méthode qui initialise le programme

            // pas fermer la console
            Console.ReadLine();
        }
    }
}
