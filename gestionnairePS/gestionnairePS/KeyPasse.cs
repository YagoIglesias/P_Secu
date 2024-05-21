/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 14.05.24
/// Description : Classe dedier a la creation d'un master passeword choisi par l'utilisateur.
///               Pour ceci la méthode CheckOrGenerateKey() est appeller qui crée le fichier si il n'existe pas
///               et appelle la méthode MasterPasseWord() qui permet la saissie de la clé, la saisie est masquer par des chars '*'
///               avec la méthode HideInput() la clé est chiffrer par la méthode DecryptionKeyVigenere 
///               et la clé est enregistrer dans le fichier créér précédement avec la méthode SaveKey().
///               En revanche si le fichier exite le fichier est lu et la clé est decripter.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

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
        /// recuperer ou mettre a jour le nom du fichier qui contient la clé 
        /// </summary>
        public string KeyFileName { get { return _keyFileName; } set { _keyFileName = value; } }

        /// <summary>
        /// consatante pour decaler 
        /// </summary>
        private const int _DECALAGE = 3;

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
        /// stocker ou mettre a jour la valeur de la cle encrypter 
        /// </summary>
        public string KeyCrypted { get { return _keyCrypted; } set { _keyCrypted = value; } }

        /// <summary>
        /// variable pour tester que la clé est correcte pour lancer le programme
        /// </summary>
        private string _keyToTest;

        /// <summary>
        /// variable pour stocker la clé decripter recuperer du fichier 
        /// </summary>
        private string _keyDecripted = null;

        /// <summary>
        /// recuperer ou mettre a jour la clé decripter 
        /// </summary>
        public string KeyDecripted { get { return _keyDecripted; } set { _keyDecripted = value; } }

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
        public string KeyPath { get { return _keyPath; } set { _keyPath = value; } }

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
                    _keyDecripted = DecryptionKeyVigenere();// decripter la clé et la stocker
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
                _key = HideInput();// clé choisi par l'utilisateur
                // si la cle est inserer alors elle est cripte pour la stocker
                if (_key != string.Empty)
                {
                    //_keyPasse = EncryptionKey();// chiffrement césar de la clé
                    _keyCrypted = EncryptionKeyVigenere();// chiffrement vigenere de la clé
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
        /// méthode pour encripter la cle
        /// </summary>
        /// <returns> retourne la cle encripte </returns>
        public string EncryptionKey()
        {
            // instancier le tableau de valeurs
            _charValues = new int[_key.Length];

            // var pour stocker la valeur du char
            int value;

            // separer le ps en char et stock les chars dans un tableau
            _chars = _key.ToCharArray();

            // tableau pour stocker les nouveau characteres
            char[] newArray = new char[_key.Length];

            // variable pour stocker la concatenation des characteres modifies 
            string encriptedKey;

            // parcourrir le tableau de char
            for (int i = 0; i < _chars.Length; i++)
            {
                // stocker la valeur numerique des chars
                value = Convert.ToInt32(_chars[i]);
                _charValues[i] = (value + _DECALAGE);// stocker la valeur du char dans le tableau 
                newArray[i] = Convert.ToChar(_charValues[i]);// stocker la valeur reconverti en char dans le nouveau tableau
                newArray.ToString();// convertir le tableau en string 
            }
            // stocker le ps encripte avec le string du tableau
            encriptedKey = new string(newArray);
            // returner le ps encripte
            return encriptedKey;

        }

        /// <summary>
        /// méthode pour afficher le mot de passe en claire
        /// </summary>
        public string DecryptionKey()
        {
            // instancier le tableau des valeurs du char
            _charValues = new int[_key.Length];
            int values;// stocker les valeurs du char
            _chars = _key.ToCharArray();// separer le ps en char et les stocker dans le tableau 
            string dencriptedPs = null;// stocker le ps decripte
            // parcourrir le tablea de char
            for (int i = 0; i < _chars.Length; i++)
            {
                // stocker la valeur
                values = Convert.ToInt32(_chars[i]);
                _charValues[i] = (values - _DECALAGE);// faire la valeur moins 3 pour stocker la valeur du vrais char
                dencriptedPs += Convert.ToChar(_charValues[i]);// concatener le char et le stocker dans decriptedPs
            }
            // returner le ps decripter
            return dencriptedPs;
        }

        //****/ chiffrement vigenere /***//
 
        /// <summary>
        /// méthode pour chiffrer la clé avec le chiffrement de vigenere
        /// </summary>
        /// <returns> clé chiffrer </returns>
        public string EncryptionKeyVigenere()
        {
            // instancier le tableu qui contients les valeurs des chars de la clé
            _charValues = new int[_key.Length]; 
            int valuesKey;// variable qui stock la valeur du char de la clé
            int[] cryptedKey = new int[_key.Length];// stocker les nouveau chars de la clé cripte
            _chars = _key.Trim().ToCharArray();// tableau des chars de la cle

            // var pour stocker la clé encripte
            string encryptedKey = null;

            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _key.Length; i++)
            {
                valuesKey = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de l'addition de la cle + la clé et le modulo pour avoir la table ascii etendue
                cryptedKey[i] = _charValues[i] + _charValues[i] % 256;

                encryptedKey += Convert.ToChar(cryptedKey[i]);// concatenation des chars
            }
            // clé chiffrer
            return encryptedKey;
        }

        /// <summary>
        /// méthode pour dechiffrer la clé selon le chiffrement de vigenere
        /// </summary>
        /// <returns> clé decripte </returns>
        public string DecryptionKeyVigenere()
        {
            // instancier le tableu qui contients les valeurs des chars de la clé
            _charValues = new int[_key.Length];
            int valuesKey;// variable qui stock la valeur du char de la clé
            int[] decryptedKey = new int[_key.Length];// stocker les nouveau chars de la clé decripte
            _chars = _key.ToCharArray();// tableau des chars de la cle

            // var pour stocker la clé decripte
            string decriptedKey = null;

            // transformer le char en int pour chaque case du tableau
            for (int i = 0; i < _key.Length; i++)
            {
                valuesKey = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de la division du character par deux 
                decryptedKey[i] = _charValues[i] / 2; //_charValues[i] % 256;

                decriptedKey += Convert.ToChar(decryptedKey[i]);// concatenation des chars
            }
            // clé dechiffre
            return decriptedKey;
        }

        /// <summary>
        /// méthode pour encripter la clé a tester afin de verifier la clé encripter du fichier
        /// </summary>
        /// <returns> clé chiffrer </returns>
        public string TestKey()
        {
            // instancier le tableu qui contients les valeurs des chars de la clé
            _charValues = new int[_keyToTest.Length];
            int valuesKey;// variable qui stock la valeur du char de la clé
            int[] cryptedKey = new int[_keyToTest.Length];// stocker les nouveau chars de la clé cripte
            _chars = _keyToTest.Trim().ToCharArray();// tableau des chars de la cle

            // var pour stocker la clé encripte
            string encryptedKey = null;

            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _keyToTest.Length; i++)
            {
                valuesKey = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de l'addition de la cle + la clé et le modulo pour avoir la table ascii etendue
                cryptedKey[i] = _charValues[i] + _charValues[i] % 256;

                encryptedKey += Convert.ToChar(cryptedKey[i]);// concatenation des chars
            }
            // clé chiffrer
            return encryptedKey;
        }

        /// <summary>
        /// méthode pour masquer lentre de characteres 
        /// </summary>
        /// <returns> retourne la mot de passe en claire </returns>
        public string HideInput()
        {
            string input = " ";// stocker la valeur de la touche
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
                }
                // si la touche presse et le backspace et que l'input n'est pas null 
                else if (keyPressed.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Remove(input.Length - 1);// effacer le char
                    Console.Write("\b \b");// effacer le caractère précédent sans ajouter d’espace vide 
                }
            } while (keyPressed.Key != ConsoleKey.Enter);
            Console.WriteLine();
            // returne le mot de passe en claire
            return input.Trim();
        }
    }
}
