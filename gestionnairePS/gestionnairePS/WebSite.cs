/// ETML
/// Auteur: Yago Iglesias Rodriguez
/// Date: 26.03.2024
/// Description: Claase qui permet de creer un site internet 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionnairePS
{
    internal class WebSite
    {
        // variable pour le nom du site
        private string _nameWebSite = null;
        // variable pour l'url du site
        private string _url = null;
        // variable pour le login
        private string _login = null;
        // variable pour le mot de passe
        private string _passeWord = null;

        /// <summary>
        /// recuperer ou mettre a jour le nom du site
        /// </summary>
        public string NameWebSite { get { return _nameWebSite; } set { _nameWebSite = value; } }

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

        // creation d'une liste pour stocker les sites
        private List<WebSite> webSites = new List<WebSite>();

        /// <summary>
        /// recuperer ou mettre a jour la liste des sites
        /// </summary>
        public List<WebSite> WebSites { get { return webSites; } set { webSites = value; } }

        /// <summary>
        /// contructeur de l'application
        /// </summary>
        /// <param name="name"> inserer le nom du site</param>
        public WebSite(string name, string url, string login, string passeWord)
        {
            _nameWebSite = name;
            _url = url;
            _login = login;
            _passeWord = passeWord;

        }

        /// <summary>
        /// methode pour afficher les informations du site
        /// </summary>
        /// <returns></returns>
        public string GetWebSiteInfo()
        {
            // var pour stocker les infos du site
            string webSiteInfo = null;

            // var stocker l'url
            string url = $"URL : {_url}\n";
            // var stocker le login
            string login = $"Login : {_login}\n";
            // var stocker le mot de passe
            string passeWord = $"Mot de Passe : {_passeWord}";

            webSiteInfo = url + login + passeWord;

            return webSiteInfo;


        }




    }
}
