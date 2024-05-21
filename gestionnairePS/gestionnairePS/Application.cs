/// ETML
/// Auteur: Yago Iglesias Rodriguez
/// Date: 26.03.2024
/// Description: Claase qui permet de creer une application affin de recuperer les informations de connexion de l'application.
///              Pour ceci la méthode GetAppInfo() est utiliser affin de stocker les informations et les retourner pour les stocker dans le fichier correspondent 
             

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionnairePS
{
    internal class Application
    {
        /// <summary>
        /// variable pour le nom de l'application
        /// </summary>
        private string _nameApp = null;

        /// <summary>
        /// variable pour l'url de l'application
        /// </summary>
        private string _url = null;

        /// <summary>
        /// variable pour le login
        /// </summary>
        private string _login = null;

        /// <summary>
        /// variable pour le mot de passe
        /// </summary>
        private string _passeWord = null;

        /// <summary>
        /// recuperer ou mettre a jour le nom de l'application
        /// </summary>
        public string NameApp { get { return _nameApp; }  set { _nameApp = value; } }

        /// <summary>
        /// variable pour recuperer ou mettre a jour l'url
        /// </summary>
        public string Url { get { return _url; } set { _url = value; } }

        /// <summary>
        /// recuperer ou mettre a jour le login
        /// </summary>
        public string Login { get { return _login; } set { _login = value; } }

        /// <summary>
        /// recuperer ou mettre a jour un mot de passe 
        /// </summary>
        public string PasseWord { get { return _passeWord; } set { _passeWord = value; } }

        /// <summary>
        /// contructeur de l'application
        /// </summary>
        /// <param name="name">inserer le nom de l'application</param>
        /// <param name="url">lien</param>
        /// <param name="login">login</param>
        /// <param name="passeWord">mot de passe</param>
        public Application(string name,string url, string login, string passeWord)
        {
            _nameApp = name;
            _url = url;
            _login = login;
            _passeWord = passeWord;
        }

        /// <summary>
        /// methode pour afficher les informations de l'application
        /// </summary>
        /// <returns> les informations de conexion de l'application </returns>
        public string GetAppInfo()
        {
            // var pour stocker les infos de l'application
            string appInfo = null;

            // var stocker l'url
            string url = $"{_url}\n"; 
            // var stocker le login
            string login = $"{_login}\n";
            // var stocker le mot de passe
            string passeWord = $"{_passeWord}"; 
            appInfo = url + login + passeWord;// concatenation

            // returner les informations de l'application
            return appInfo;
        }
    }
}
