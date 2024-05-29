/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 19.03.24
/// Description : Classe qui permet la creation du menu, au debut du programme la méthode Init() qui permet de verifier la clé 
///               si la clé est fausse alors le programme s'arrete, si non la méthode Start() est appeller pour executer le menu.
///               Ensuite il est possible d'appeller les options qui sont executés par les méthodes suivantes:
///               CheckPassWord() pour consulter un mot de passe.
///               AddPasseWord() pour ajouter un mot de passe.
///               ModifyPassword() pour modifier.
///               ChangeKey() pour changer la clé.
///               et DeletePasseword() qui permet de supprimer les fichiers.

using System;
using System.IO;

namespace gestionnairePS
{
    internal class Menu
    {
        /// <summary>
        /// variable pour la reponse de l'utilisateur
        /// </summary>
        private string _actionSelected = " ";

        /// <summary>
        /// compteur d'applications
        /// </summary>
        private int _counter = 1;

        /// <summary>
        /// instancier un objet fichier
        /// </summary>
        LogFile _logFile = new LogFile(" ");

        /// <summary>
        /// tableau pour parcourrir les fichiers d'un repertoire
        /// </summary>
        private string[] _files;

        /// <summary>
        /// intancier un objet qui permet de créer la clé a l'utilisateur
        /// </summary>
        KeyPasse _key = new KeyPasse();

        /// <summary>
        /// constructeur du menu
        /// </summary>
        public Menu()
        {
            

        }

        /// <summary>
        /// méthode pour vérifier les mots de passe 
        /// </summary>
        public void CheckPasseWord()
        {
            // verifier qu'il y ai des fichiers et si il y en a les stocker dans le tableau de fichiers
            ReadDirectory();
            // effacer la console
            Console.Clear();
            // si il n'y a pas de fichiers alors un message pour prevenir l'utilisateur s'affiche
            if (_files.Length == 0)
            {
                Console.WriteLine(" Aucun fichier existant dans le dossier Passwords, appuyer sur enter pour retourner au menu principal.");
                Console.ReadKey();
                // reaficher le menu
                Start();
            }
            else
            {
                // menu
                Console.WriteLine("******************************************************");
                Console.WriteLine(" Consulter un mot de passe:");
                Console.WriteLine(" 1. Retour au menu principal");

                // parcourir le tableau de fichiers
                for (int i = 0; i < _files.Length; i++)
                {
                    // a chaque fois qu'on parcour un element on increment de un 
                    _counter++;
                    // afficher le numero et le fichiers 
                    Console.WriteLine($" {_counter}. {_files[i]}");
                }
                // reinitialiser le compteur
                _counter = 1;

                Console.WriteLine("******************************************************\n");
                Console.Write(" Faites votre choix : ");
                _actionSelected = Console.ReadLine();
                // verifier le choix de l'utilisateur 
                switch (Convert.ToInt32(_actionSelected))
                {
                    // case 1 est le retour au menu principal(choix par default)
                    case 1:
                        // reaficher le menu
                        Start();
                    break;

                    // prendre en compte le reste des chiffres
                    default:
                  
                        string name = _files[Convert.ToInt32(_actionSelected) - 2];
                        _logFile.ReadFile(name);// lire le fichier selon le choix 
                        Console.WriteLine(" Appuyez sur Enter pour masquer le mot de passe et revenir au menu");
                        Console.ReadKey();
                        Start();
                    break;
                }
            }
        }

        /// <summary>
        /// méthode pour ajouter les mots de passe 
        /// </summary>
        public void AddPasseWord()
        {
            //var pour stocker une application
            string newApp = null;

            Console.Write(" Ajoutez le nom de votre application: " );
            newApp = Console.ReadLine();

            // si rien est rentrer comme nom d'application un messages'affiche pour l'utilisateur
            if (newApp != string.Empty)
            { 
                // instancier un fichier avec le nom de l'application qu'on veut
                LogFile logFile = new LogFile(name:newApp);
                logFile.FileGenerator(); // appeller la méthode qui créer le fichier dans le bon chemin

                // instancier une application 
                Application app = new Application(name: newApp, url: null, login: null, passeWord: null);

                // demander l'url jusqu'a ce que ce soit remplie 
                do 
                {
                    // url de la app
                    Console.Write(" URL : ");
                    app.Url = Console.ReadLine();

                } while (app.Url == string.Empty); // verifier que le champ url est remplie

                // demander le login jusqu'a ce que ce soit remplie
                do
                {
                    //Console.WriteLine();
                    // login de l'app
                    Console.Write(" Login : ");
                    app.Login = Console.ReadLine();

                    // verifier que le login n'est pas vide pour encripter lo login
                    if (app.Login != string.Empty)
                    {
                        Login login = new Login(app.Login);// instancier l'objet login pour pouvoir le chiffrer 
                        //app.Login = login.Encryption();// chiffrement césar du login
                        app.Login = login.EncryptionLoginVigenere(true);// chiffrement vigèner du login
                    }

                } while (app.Login == string.Empty); // verifier que le champ login est remplie 

                // demander le mot de passe jusqu'a ce que ce soit remplie
                do
                {
                    //Console.WriteLine();
                    // mot de passe de l'app
                    Console.Write(" Mot de passe : ");
                    app.PasseWord = _key.HiddeInput();

                    // verifier que le mot de passe n'est pas vide pour encripter le mot de passe
                    if (app.PasseWord != string.Empty)
                    {
                        PasseWord passWord = new PasseWord(app.PasseWord);// insatncier l'objet ps pour le chiffrer 
                        //app.PasseWord = passWord.Encryption();// chifrement césar du ps 
                        app.PasseWord = passWord.EncryptionPassewordVigenere(true);// chifrement vigènere du ps 
                    }

                } while (app.PasseWord == string.Empty); // verifier que le champ mot de passe est remplie

                logFile.WriteFile(app);// ecrire dans le fichier 
                Console.WriteLine(" Appuyez sur Enter pour masquer le mot de passe et revenir au menu");
                Console.ReadKey();
                // une fois apuyer sur enter on reafiche le menu 
                Start();
            }
            else 
            {
                Console.WriteLine(" Il faut saisir une application, appuyer sur enter pour revenir au menu");
                Console.ReadKey();
                Start();// reaficher le menu
            }
        }

        /// <summary>
        /// methode pour effacer un mot de passe 
        /// </summary>
        public void ModifyPassword()
        {
            // verifier qu'il y ai des fichiers et si il y en a les stocker dans le tableau de fichiers
            ReadDirectory();
            // effacer la console
            Console.Clear();
            // si il n'y a pas de fichiers alors un message pour prevenir l'utilisateur s'affiche
            if (_files.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine(" Aucun fichier existant dans le dossier Passwords, appuyer sur enter pour retourner au menu principal.");
                Console.ReadKey();
                // reaficher le menu
                Start();
            }
            else
            {
                // menu
                Console.WriteLine("******************************************************");
                Console.WriteLine(" Modifier un mot de passe:");
                Console.WriteLine(" 1. Retour au menu principal");

                // parcourir le tableau de fichiers
                for (int i = 0; i < _files.Length; i++)
                {
                    // a chaque fois qu'on parcour un element on increment de un 
                    _counter++;
                    // afficher le numero et le fichiers 
                    Console.WriteLine($" {_counter}. {_files[i]}");
                }
                // reinitialiser le compteur
                _counter = 1;
                Console.WriteLine("******************************************************\n");
                Console.Write(" Faites votre choix : ");
                _actionSelected = Console.ReadLine();
                switch (Convert.ToInt32(_actionSelected))
                {
                    case 1:
                        // reaficher le menu
                        Start();
                    break;

                    // prendre en compte le reste des chiffres 
                    default :
                        string name = _files[Convert.ToInt32(_actionSelected) - 2];
                        string appName = name.Substring(0,name.Length-4);// enlever l'extension du fichier
                        _logFile.FileName = appName;// donner le nom di fichier sur lequelle enregistrer les informations
                        // instancier une application 
                        Application app = new Application(name: appName, url: null, login: null, passeWord: null);
                        // demander l'url jusqu'a ce que ce soit remplie 
                        do
                        {
                            // url de la app
                            Console.Write(" URL : ");
                            app.Url = Console.ReadLine();
                        } while (app.Url == string.Empty); // verifier que le champ url est remplie
                        // demander le login jusqu'a ce que ce soit remplie
                        do
                        {
                            // login de l'app
                            Console.Write(" Login : ");
                            app.Login = Console.ReadLine();
                            // verifier que le login n'est pas vide pour encripter lo login
                            if (app.Login != string.Empty)
                            {
                                Login login = new Login(app.Login);// instancier l'objet login pour pouvoir le chiffrer 
                                //app.Login = login.Encryption();// chiffrement césar du login
                                app.Login = login.EncryptionLoginVigenere(true);// chiffrement vigèner du login
                            }

                        } while (app.Login == string.Empty); // verifier que le champ login est remplie 
                        // demander le mot de passe jusqu'a ce que ce soit remplie
                        do
                        {
                            // mot de passe de l'app
                            Console.Write(" Mot de passe : ");
                            app.PasseWord = _key.HiddeInput();
                            // verifier que le mot de passe n'est pas vide pour encripter le mot de passe
                            if (app.PasseWord != string.Empty)
                            {
                                PasseWord passWord = new PasseWord(app.PasseWord);// insatncier l'objet ps pour le chiffrer 
                                //app.PasseWord = passWord.Encryption();// chifrement césar du ps 
                                app.PasseWord = passWord.EncryptionPassewordVigenere(true);// chifrement vigènere du ps 
                            }

                        } while (app.PasseWord == string.Empty); // verifier que le champ mot de passe est remplie

                        _logFile.WriteFile(app);// ecrire dans le fichier

                        Console.WriteLine(" Appuyez sur Enter pour masquer le mot de passe et revenir au menu");
                        Console.ReadKey();
                        Start();// reaficher le menu
                    break;
                }
            }
        }

        /// <summary>
        /// methode pour fermer le programe
        /// </summary>
        public void Exit()
        {
            // fermer le programme
            Environment.Exit(0);            
        }

        /// <summary>
        /// methode pour afficher le menu 
        /// </summary>
        public void Start()
        {
            // effacer la console
            Console.Clear();
            // menu
            Console.WriteLine("******************************************************");
            Console.WriteLine(" Sélectionnez une action");
            Console.WriteLine(" 1. Consulter un mot de passe");
            Console.WriteLine(" 2. Ajouter un mot de passe");
            Console.WriteLine(" 3. Modifier un mot de passe");
            Console.WriteLine(" 4. Suprimer un mot de passe");
            Console.WriteLine(" 5. Changer de clé");
            Console.WriteLine(" 6. Quitter le programme");
            Console.WriteLine("******************************************************\n");
            Console.Write(" Faites votre choix : ");
            _actionSelected = Console.ReadLine();
            switch (_actionSelected)
            {
                case "1":
                    CheckPasseWord();
                break;

                case "2":
                    AddPasseWord();
                break;

                case "3":
                    ModifyPassword();
                break;

                case "4":
                    DeletePasseword();
                break;

                case "5":
                    ChangeKey();
                break;

                case "6":
                    Exit();                    
                break;

                default:
                break;

            }
        }

        /// <summary>
        /// méthode pour lire un repertoire et stocker ses fichiers d'uns un tableau 
        /// </summary>
        public void ReadDirectory()
        {
            // creation d'un tableau pour recuperer les fichiers du dossier  
            string[] filesToReturn = Directory.GetFiles(_logFile.Path, "*.txt");
            // parrcourir le tableau 
            for (int i = 0; i < filesToReturn.Length; i++)
            {
                // chaque case du tableau serra le fichier moins le nom du repertoire
                filesToReturn[i] = filesToReturn[i].Substring(_logFile.Path.Length);
            }
            _files = filesToReturn;// renvoyer les fichiers au tableau de fichiers
        }
        
        /// <summary>
        /// méthode pour initialiser le programme afin de verifier si il y a une clé et quelle est correcte 
        /// </summary>
        public void Init()
        {
            // verifier l'existance de la clé 
            _key.CheckOrGenerateKey();
            
            // tant que la cle n'est pas rentrer alors on demande
            do
            {
                Console.Write(" Ecrivez votre clé: ");
                _key.KeyToTest = _key.HiddeInput();// masquer la saisie
                // si la clé n'est pas vide 
                if (_key.KeyToTest != string.Empty)
                {
                    // verifier l'existance de la clé 
                    _key.CheckOrGenerateKey();
                    _key.KeyToTest = _key.EncryptKeyToTest();// cripter la clé inserer afin de verifier si elle correspond a celle enregistre 
                    // si la clé cripter qu'on test est la même que la clé enregistrer alors on demarre le programme
                    if (_key.KeyToTest == _key.Key)
                    {
                        Start();// lancer le programme
                    }
                    // si non message d'erreure
                    else
                    {
                        Console.WriteLine(" Votre clé est fausse");
                    }
                }

            } while (_key.Key == string.Empty);
        }
 
        /// <summary>
        /// méthode pour changer la clé 
        /// </summary>
        public void ChangeKey()
        { 
             // recuperer la cle precedante
            _key.CheckOrGenerateKey();
            //parcourir le repertoire des applications
            ReadDirectory();
            string infoFile = null; // stocker les informations des fichiers
            // parcourrir les fichiers et recuperer les informations afin de les decripter avec l'ancienne clé
            for (int i = 0; i < _files.Length; i++)
            {
                // recuperer les informations des fichiers decripters
                infoFile = _logFile.EncryptOrDecryptFileInfo(_files[i],false);
                // ecrire dans le fichier qui est dans le chemin indiquer en parametre
                StreamWriter appInfo = new StreamWriter(_logFile.Path + _files[i]);
                appInfo.WriteLine(infoFile);// informations decriptes
                // fin du processus
                appInfo.Close();
            }
            // demander la nouvelle cle
            _key.MasterPassword();
            _key.SaveKey();// enregistrer la nouvelle cle
            // reparcourir le dossier de passewords
            ReadDirectory();
            // parcourrir les fichiers 
            for (int i = 0; i < _files.Length; i++)
            {
                // strocker les informations encriptes
                infoFile = _logFile.EncryptOrDecryptFileInfo(_files[i], true);
                // ecrire dans le fichier qui est dans le chemin indiquer en parametre
                StreamWriter appInfo = new StreamWriter(_logFile.Path + _files[i]);
                appInfo.WriteLine(infoFile);// informations encriptes
                // fin du processus
                appInfo.Close();
            }
            // afficher le menu principal
            Start();
        }

        /// <summary>
        /// méthode pour supprimer un fichier qui contient un mot de passe
        /// </summary>
        public void DeletePasseword()
        {
            // verifier qu'il y ai des fichiers et si il y en a les stocker dans le tableau de fichiers
            ReadDirectory();
            // effacer la console
            Console.Clear();
            // si il n'y a pas de fichiers alors un message pour prevenir l'utilisateur s'affiche
            if (_files.Length == 0)
            {
                Console.WriteLine();
                Console.WriteLine(" Aucun fichier existant dans le dossier Passwords, appuyer sur enter pour retourner au menu principal.");
                Console.ReadKey();
                // reaficher le menu
                Start();
            }
            else
            {
                // menu
                Console.WriteLine("******************************************************");
                Console.WriteLine(" Suprimer un mot de passe:");
                Console.WriteLine(" 1. Retour au menu principal");

                // parcourir le tableau de fichiers
                for (int i = 0; i < _files.Length; i++)
                {
                    // a chaque fois qu'on parcour un element on increment de un 
                    _counter++;
                    // afficher le numero et le fichiers 
                    Console.WriteLine($" {_counter}. {_files[i]}");
                }
                // reinitialiser le compteur
                _counter = 1;
                Console.WriteLine("******************************************************\n");
                Console.Write(" Faites votre choix : ");
                _actionSelected = Console.ReadLine();
                switch (Convert.ToInt32(_actionSelected))
                {
                    case 1:
                        // reaficher le menu
                        Start();
                    break;

                    // prendre en compte le reste des chiffres 
                    default:
                        string name = _files[Convert.ToInt32(_actionSelected) - 2];
                        File.Delete(_logFile.Path + name);
                        Console.WriteLine(" Fichier supprimer appuyez sur enter pour retourner au menu principal");
                        Console.ReadKey();
                        Start();
                    break; 
                }
            }
        }
    }
}
