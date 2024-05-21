/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 23.04.24
/// Description : Classe qui permet la creation de fichier ou nous allons stocker les informations, les fichiers seront créé pour chaque application.
///               Pour ceci les fichiers sont créés avec la méthode FileGenerator() qui prends le nom en paramettre du constructeur.
///               WriteFile(Application application) est executer pour écrire dans le fichier les informations de l'application en parametres.
///               Ensuite il y a les méthodes : 
///               ReadFile(string fileName) lit les fichiers qui ont le nom reçu en parametre et affiche les informations dechiffres
///               DecryptFileIfKeyChange(string fileName) decript les fichiers qui ont le nom reçu en parametres avec l'ancienne clé si celle si est changé 
///               EncryptFileIfKeyChange(string fileName) encrypter les fichiers qui ont le nom reçu en parametre avec la nouvele clé si celle si est changé 
        

using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gestionnairePS
{
    internal class LogFile
    {
        /// <summary>
        /// variable pour stocker le chemin pour la creation du fichier
        /// </summary>
        private string _path = ".\\Passwords\\";

        /// <summary>
        /// variable pour stocker le nom du fichier
        /// </summary>
        private string _fileName;

        /// <summary>
        /// recuperer ou mettre a jour le chemin
        /// </summary>
        public string Path {  get { return _path; } private set { _path = value; } }

        /// <summary>
        /// recuperer ou mettre a jour le nom du fichier 
        /// </summary>
        public string FileName { get { return _fileName; } set { _fileName = value; } }

        /// <summary>
        /// tableau pour stocker les informations du fichier selectioner
        /// </summary>
        private string[] _infoFile = new string[4];

        /// <summary>
        /// recuperer ou mettre a jour le tableu 
        /// </summary>
        public string[] InfoFile { get { return _infoFile; } set { _infoFile = value; } }

        /// <summary>
        /// instancier l'objet login 
        /// </summary>
        Login _login = new Login(login: " ");

        /// <summary>
        /// instancier l'objet passe word
        /// </summary>
        PasseWord _ps = new PasseWord(ps: " ");

        /// <summary>
        /// Constructeur du fichier avec le nom qu'n dessire(app)
        /// </summary>
        /// <param name="name"></param>
        public LogFile(string name)
        {
            _fileName = name;
        }

        /// <summary>
        /// méthode pour créér le fichier 
        /// </summary>
        public void FileGenerator()
        {
            // si le fichier n'existe pas alors on le créé
            if (!File.Exists(path: _path + _fileName + ".txt"))
            {
                StreamWriter logFile = File.CreateText(_path + _fileName + ".txt");
                logFile.Close();// fermer le procesus une fois le fichier créé
            }

        }

        /// <summary>
        /// méthode pour écrire dans le fichier 
        /// </summary>
        /// <param name="application"> objet application afin d'avoir les informations </param>
        public void WriteFile(Application application)
        {
            // informations de l'app
            string info = application.GetAppInfo();
            // ecrire dans le fichier qui est dans le chemin indiquer en parametre
            StreamWriter appInfo = new StreamWriter(_path + _fileName + ".txt");
            appInfo.WriteLine(_fileName);// nom de l'app
            appInfo.WriteLine(info);// informations
            // fin du processus
            appInfo.Close();
        }

        /// <summary>
        /// méthode pour lire le fichier
        /// </summary>
        /// <param name="fileName"> nom du fichier </param>
        public void ReadFile(string fileName)
        {
            string[] fileArray = new string[4];//céer un const pour le nombre d'elements contenues dans les fichiers
            // recuperer le nom du fichier
            _fileName = fileName;
            string line;// variable pour lire les lignes
            int lineNumber = 0;// compteur de lignes

            // preciser le nom du fichier
            StreamReader readAppInfo = new StreamReader($"{_path}{_fileName}");
            // lire les lignes du fichier
            line = readAppInfo.ReadLine();

            // lire les lignes tant qu'elles sont pas vides
            while (line != null)
            {
                // si la premier ligne commence a 0
                if (lineNumber == 0)
                {
                    string appName = line;
                    // ecrire le nom de l'app
                    Console.WriteLine($" Nom de l'application: {appName}");//line
                    fileArray[lineNumber] = appName;
                }
                else if (lineNumber == 1)
                {
                    string url = line;
                    // afficher l'URL
                    Console.WriteLine($" URL: {url}");//line
                    fileArray[lineNumber] = url;
                }
                else if (lineNumber == 2)
                {
                    string login;// stocker le login decripter
                    // stocker la ligne comme le login
                    _login.UserName = line.Trim();
                    //login = _login.Decryption();// decriptage césar du login
                    login = _login.DencryptionVigenere();// decriptage vigènere du login
                    // afficher le login decripter
                    Console.WriteLine($" Login: {login}");
                    fileArray[lineNumber] = login;
                }
                else if (lineNumber == 3)
                {
                    string ps;// stocker le passeWord decripter
                    // stocker la ligne comme le ps
                    _ps.Ps = line;
                    //ps = _ps.Decryption();// dechiffrement césar du passeWord
                    ps = _ps.DecryptionVigenere();// dechiffrement vigènere du passeWord

                    // afficher le ps decripter
                    Console.WriteLine($" PasseWord: {ps}");
                    fileArray[lineNumber] = ps;
                }
                // incrementer le compteur 
                lineNumber++;
                line = readAppInfo.ReadLine();// remetre line par defaut  
            }
            _infoFile = fileArray;
            // fin du processus
            readAppInfo.Close();
        }

        /// <summary>
        /// méthode pour decrypter les informations du fichier si on change la clé 
        /// </summary>
        /// <param name="fileName"> nom du fichier </param>
        /// <returns> retourne les informations decriptes </returns>
        public string DecryptFileIfKeyChange(string fileName)
        {
            string name = null;// stocker le nom de l'app
            string url = null;// stocker l'url
            string login = null;// stocker le login
            string password = null;// stocker le mot de passe
            string fullInfo = null;// stocker les informations completes de l'app

            // recuperer le nom du fichier
            _fileName = fileName;
            string line;// variable pour lire les lignes
            int lineNumber = 0;// compteur de lignes

            // preciser le nom du fichier
            StreamReader readAppInfo = new StreamReader($"{_path}{_fileName}");
            // lire les lignes du fichier
            line = readAppInfo.ReadLine();

            // lire les lignes tant qu'elles sont pas vides
            while (line != null)
            {
                // si la premier ligne commence a 0
                if (lineNumber == 0)
                {
                    string appName = line;
                    // stocker le nom de l'app
                    name = $"{appName}\n";
                }
                else if (lineNumber == 1)
                {
                    string appUrl = line;
                    // stocker l'URL
                    url = $"{appUrl}\n";
                }
                else if (lineNumber == 2)
                {
                    string appLogin;// stocker le login decripter
                    // stocker la ligne comme le login
                    _login.UserName = line.Trim();
                    //login = _login.Decryption();// decriptage césar du login
                    appLogin = _login.DencryptionVigenere();// decriptage vigènere du login
                    // stocker le login decrypte
                    login = $"{appLogin}\n";
                }
                else if (lineNumber == 3)
                {
                    string appPs;// stocker le passeWord decripter
                    // stocker la ligne comme le ps
                    _ps.Ps = line;
                    //ps = _ps.Decryption();// dechiffrement césar du passeWord
                    appPs = _ps.DecryptionVigenere();// dechiffrement vigènere du passeWord
                    // stocker le mot de passe decripte
                    password = $"{appPs}";
                }
                // incrementer le compteur 
                lineNumber++;
                line = readAppInfo.ReadLine();// remetre line par defaut
            }
            // fin du processus
            readAppInfo.Close();
            // stocker les info au complet
            fullInfo = name + url + login + password;
            // retourner les infos decryptes
            return fullInfo;
        }

        /// <summary>
        /// méthode pour encrypter les fichiers
        /// </summary>
        /// <param name="fileName"> nom du fichier </param>
        /// <returns> informations au complete de l'application </returns>
        public string EncryptFileIfKeyChange(string fileName)
        {
            string name = null;// stocker le nom de l'app
            string url = null;// stocker l'url
            string login = null;// stocker le login
            string password = null;// stocker le mot de passe
            string fullInfo = null;// stocker les informations completes de l'app

            // recuperer le nom du fichier
            _fileName = fileName;
            string line;// variable pour lire les lignes
            int lineNumber = 0;// compteur de lignes

            // preciser le nom du fichier
            StreamReader readAppInfo = new StreamReader($"{_path}{_fileName}");
            // lire les lignes du fichier
            line = readAppInfo.ReadLine();

            // lire les lignes tant qu'elles sont pas vides
            while (line != null)
            {
                // si la premier ligne commence a 0
                if (lineNumber == 0)
                {
                    string appName = line;
                    // stocker le nom de l'app
                    name = $"{appName}\n";
                }
                else if (lineNumber == 1)
                {
                    string appUrl = line;
                    // stocker l'URL
                    url = $"{appUrl}\n";
                }
                else if (lineNumber == 2)
                {
                    string appLogin;// stocker le login decripter
                    // stocker la ligne comme le login
                    _login.UserName = line.Trim();
                    //login = _login.Decryption();// decriptage césar du login
                    appLogin = _login.EncryptionVigenere();// encryptage vigènere du login
                    // stocker le login encrypte
                    login = $"{appLogin}\n";
                }
                else if (lineNumber == 3)
                {
                    string ps;// stocker le passeWord decripter
                    // stocker la ligne comme le ps
                    _ps.Ps = line;
                    //ps = _ps.Decryption();// dechiffrement césar du passeWord
                    ps = _ps.EncryptionVigenere();// encrypte vigènere du passeWord
                    // stocker le mot de passe encrypte
                    password = $"{ps}";
                }
                // incrementer le compteur 
                lineNumber++;
                line = readAppInfo.ReadLine();// remetre line par defaut
            }
            // fin du processus
            readAppInfo.Close();
            // stocker les infos completes
            fullInfo = name + url + login + password;
            // informations encryptes 
            return fullInfo;
        }
    }
}