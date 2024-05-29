/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 14.05.24
/// Description : Classe dedier a la creation d'un master passeword choisi par l'utilisateur.
///               Pour ceci la méthode CheckOrGenerateKey() est appeller pour crée le fichier si il n'existe pas
///               et appelle la méthode MasterPasseWord(), qui permet la saissie de la clé, la saisie est masquer par des chars '*'
///               avec la méthode HideInput(). La clé est chiffrer par la méthode EncryptionKeyVigenere(bool isCrypted) si le boolean est vrais
///               et dechiffrer si le boolean est faux. 
///               Ensuite la clé est enregistrer dans le fichier créér précédement avec la méthode SaveKey().
///               En revanche si le fichier exite le fichier est lu et la clé chiffrer est stocker.
///               Pour finir il y a la méthode EncryptKeyToTest() qui permet de cripter la clé entre par l'utilisateur.
///               Ceci permet de verifier si la clé encrypter correspond á la clé inserer et continuer le programme ou l'interrompre

using System;
using System.IO;

namespace gestionnairePS
{
    internal class KeyPasse
    {
        /// <summary>
        /// variable pour stocker la cle de l'utilisateur
        /// </summary>
        private string _key;

        /// <summary>
        /// variable pour stocker le chemin pour la creation du fichier
        /// </summary>
        private string _keyPath = ".\\Key\\";

        /// <summary>
        /// variable pour creer le nom du fichier qui contiens la cle 
        /// </summary>
        private string _keyFileName = "Clé";

        /// <summary>
        /// tableau pour stocker les characteres du PS
        /// </summary>
        private char[] _chars;

        /// <summary>
        /// tableau pour stocker les valeurs des characteres
        /// </summary>
        private int[] _charValues;

        /// <summary>
        /// variable pour la cle encripter pour stocker dans le fichier
        /// </summary>
        private string _keyCrypted;

        /// <summary>
        /// variable pour tester que la clé est correcte pour lancer le programme
        /// </summary>
        private string _keyToTest;

        /// <summary>
        /// recuperer ou mettre a jour la cle a verifier
        /// </summary>
        public string KeyToTest { get { return _keyToTest; } set { _keyToTest = value; } }

        /// <summary>
        /// recuperer ou mettre a jour la valeur de la cle 
        /// </summary>
        public string Key { get { return _key; } set { _key = value; } }

        /// <summary>
        /// recuperer ou metre a jour le chemin 
        /// </summary>
        public string KeyPath { get { return _keyPath; } private set { _keyPath = value; } }

        /// <summary>
        /// recuperer ou mettre a jour le nom du fichier qui contient la clé 
        /// </summary>
        public string KeyFileName { get { return _keyFileName; } private set { _keyFileName = value; } }

        /// <summary>
        /// constructeur 
        /// </summary>
        public KeyPasse()
        {

        }

        /// <summary>
        /// méthode pour verifier ou créer la cle
        /// </summary>
        public void CheckOrGenerateKey()
        {
            // si le fichier exist pas on le cree et on demande de choisir la cle
            if (!File.Exists(_keyPath + _keyFileName + ".txt"))
            {
                // cration du fichier dans le chemin indique avec le nom et l'extension
                StreamWriter keyFile = File.CreateText(_keyPath + _keyFileName + ".txt");
                keyFile.Close();// fermer le procesus une fois le fichier créé
                MasterPassword();// demander de choisir une cle
                SaveKey();// enregistrer la clé dans le fichier qui à été créé
            }
            // si le fichier existe 
            else
            {
                // lecture du fichier 
                StreamReader streamReader = new StreamReader(_keyPath + _keyFileName + ".txt");
                string line = streamReader.ReadLine();// contenu de la ligne 
                if (!string.IsNullOrEmpty(line))// si la ligne n'est pas null ni vide 
                {
                    _key = line;// la valeur de la ligne et stocke dans la variable de la cle 
                }
                streamReader.Close();// fin du processus
            }
        }

        /// <summary>
        /// méthode qui demande le master passeword a l'utilisateur et chiffre la clé
        /// </summary>
        public void MasterPassword()
        {
            // tant que la cle n'est pas rentrer alors on demande
            do
            {
                //Console.WriteLine();
                Console.Write(" Inserez votre masterpasseword qui ferra office de clé: ");
                _key = HiddeInput();// clé choisi par l'utilisateur
                // si la cle est inserer alors elle est cripte pour la stocker
                if (_key != string.Empty)
                {
                    //_keyCrypted = EncryptionKeyVigenere();// chiffrement vigenere de la clé
                    _keyCrypted = EncryptionKeyVigenere(true);// chiffrement vigenere de la clé
                }

            } while (_key == string.Empty);
        }

        /// <summary>
        /// méthode pour enregistrer la clé dans le fichier  
        /// </summary>
        public void SaveKey()
        {
            // ecrire dans le fichier qui est dans le chemin indiquer en parametre
            StreamWriter saveKey = new StreamWriter(_keyPath + _keyFileName + ".txt");
            string key = _keyCrypted;// clé chiffrer
            saveKey.WriteLine(key);// écrire la clé chiffrer dans le fichier 
            // fin du processus
            saveKey.Close();
        }        

        /// <summary>
        /// méthode pour encripter la clé a tester afin de verifier la clé encripter du fichier
        /// </summary>
        /// <returns> clé chiffrer </returns>
        public string EncryptKeyToTest()
        {
            // instancier le tableu qui contients les valeurs des chars de la clé
            _charValues = new int[_key.Length];
            int valuesKey;// variable qui stock la valeur du char de la clé
            int[] cryptedKey = new int[_key.Length];// stocker les nouveau chars de la clé cripte
            _chars = _keyToTest.Trim().ToCharArray();// tableau des chars de la cle

            // var pour stocker la clé encripte
            string encryptedKey = null;

            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _keyToTest.Length; i++)
            {
                valuesKey = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de l'addition de la cle + la clé et le modulo pour avoir la table ascii etendue
                cryptedKey[i] = (_charValues[i] + _charValues[i]) % 256;

                encryptedKey += Convert.ToChar(cryptedKey[i]);// concatenation des chars
            }
            // clé chiffrer
            return encryptedKey;
        }

        /// <summary>
        /// méthode pour masquer lentre de characteres 
        /// </summary>
        /// <returns> retourne la mot de passe en claire </returns>
        public string HiddeInput()
        {
            string input = null;// stocker la valeur de la touche
            ConsoleKeyInfo keyPressed = new ConsoleKeyInfo();// touche presse 
            // repeter tant que enter n'est pas presse
            do
            {
                // stocker la touche presse
                keyPressed = Console.ReadKey(true);// true n'affiche pas le symbole

                if (keyPressed.Key != ConsoleKey.Backspace && keyPressed.Key != ConsoleKey.Enter)
                {
                    input += keyPressed.KeyChar;// stocke le caracter de la touche
                    Console.Write("*");
                    // si alt ou alt gr sont prsses alors on efface leur valeur afin de pas avoir d'espace vide
                    if ((keyPressed.Modifiers & ConsoleModifiers.Alt) != 0)
                    {
                        input.Substring(0, input.Length - 1);
                    }
                }
                // si la touche presse et le backspace et que l'input n'est pas null 
                if (keyPressed.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Remove(input.Length - 1);// effacer le char
                    Console.Write("\b \b");// effacer le caractère précédent sans ajouter d’espace vide 
                    
                }

            } while (keyPressed.Key != ConsoleKey.Enter);
            Console.WriteLine();
            // returne le mot de passe en claire
            return input;
        }

        /// <summary>
        /// méthode qui permet de chiffrer ou dechiffrer la cl´de l'utilisateur
        /// </summary>
        /// <param name="isCrypted"> determine si il faut crypter ou decrypter </param>
        /// <returns> clé crypter ou decrypter </returns>
        public string EncryptionKeyVigenere(bool isCrypted)
        {
            // instancier le tableu qui contients les valeurs des chars de la clé
            _charValues = new int[_key.Length];
            int valuesKey;// variable qui stock la valeur du char de la clé
            int[] cryptedKey = new int[_key.Length];// stocker les nouveau chars de la clé cripte
            _chars = _key.Trim().ToCharArray();// tableau des chars de la cle

            // var pour stocker la clé encripte
            string key = null;

            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _key.Length; i++)
            {
                valuesKey = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesKey;// stocker la valeur de chaque char

                // si c'est vrais il faut crypter la clé sin la decrypter
                if (isCrypted == true)
                {
                    // stocker le nouveau char qui sort de l'addition de la cle + la clé et le modulo pour avoir la table ascii etendue
                    cryptedKey[i] = (_charValues[i] + _charValues[i]) % 256;
                }
                else
                {
                    // stocker le nouveau char qui sort de la / du char par 2 
                    cryptedKey[i] = _charValues[i] / 2;
                }   
                // concatenation des chars
                key += Convert.ToChar(cryptedKey[i]);
            }
            // clé chiffrer ou dechiffrer 
            return key;
        }
    }
}
