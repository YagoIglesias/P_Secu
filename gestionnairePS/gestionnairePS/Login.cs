/// ETML
/// Auteur: Yago Iglesias Rodriguez
/// Date: 30.04.2024
/// Description : Classe qui va permettre de créér le login chiffrer et dechifrer avec les méthodes EncryptionVigenere() et DecryptionVigenere()

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace gestionnairePS
{
    internal class Login
    {
        /// <summary>
        /// consatante pour decaler 
        /// </summary>
        private const int _DECALAGE = 3;

        /// <summary>
        /// variable pour stocker le login
        /// </summary>
        private string _userName;

        /// <summary>
        /// tableau pour stocker les characteres du login
        /// </summary>
        private char[] _chars;

        /// <summary>
        /// tableau pour stocker les valeurs des characteres
        /// </summary>
        private int[] _charValues;

        /// <summary>
        /// Recuperer ou mettre a jour le login 
        /// </summary>
        public string UserName { get { return _userName; } set { _userName = value; } }  

        /// <summary>
        /// Recuperer ou mettre a jour le tableau de valeurs
        /// </summary>
        public int[] CharValues { get { return _charValues; } set { _charValues = value; } }

        /// <summary>
        /// Constructeur pour le login
        /// </summary>
        /// <param name="login"> username </param>
        public Login(string login)
        {
            _userName = login;
        }

        /// <summary>
        /// méthode pour chiffrer le login
        /// </summary>
        public string Encryption()
        {
            // instancier le tableau de valeurs
            _charValues = new int[_userName.Length];
            // variable pour stocker la valeur du char
            int value;
            _chars = _userName.ToCharArray();// separer le ps en char et les stocker dans le tableau 
            // tableau pour stocker les nouveau characteres
            char[] newArray = new char[_userName.Length];
            // variable pour stocker la concatenation des characteres modifies 
            string encriptedLogin = null;

            // parcourrir le tableau de char
            for (int i = 0; i < _chars.Length; i++)
            {
                value = Convert.ToInt32(_chars[i]);// stocker la valeur des chars
                _charValues[i] = (value + _DECALAGE);// stocker la valeur du char + le decalage dans le tableau 
                encriptedLogin += Convert.ToChar(_charValues[i]);// stocker le ps chifrre
            }
            // returner le ps encripte
            return encriptedLogin;
        }

        /// <summary>
        /// méthode pour afficher le login en claire
        /// </summary>
        public string Decryption()
        {
            // instancier le tableu de valeurs
            _charValues = new int[_userName.Trim().Length];
            int values;// variable pour stocker les valeurs 
            _chars = _userName.Trim().ToCharArray();// separer le ps en char et les stocker
            string dencriptedLogin = null;// stocker le ps decripte 
            // parcourrir le tableau de char
            for (int i = 0; i < _chars.Length; i++)
            {
                values = Convert.ToInt32(_chars[i]);// stocker la valeur 
                _charValues[i] = (values - _DECALAGE);// stocker la v aleur moind le decalage
                dencriptedLogin += Convert.ToChar(_charValues[i]); // stocker les chars decriptes
            }
            // retourner le ps decripter
            return dencriptedLogin;
        }

        //****/ chiffrement vigenere /***//

        /// <summary>
        /// instancier l'objet pour la clé
        /// </summary>
        KeyPasse _key = new KeyPasse();

        /// <summary>
        /// tableau pour les chars de la clé
        /// </summary>
        private char[] _keyChars;

        /// <summary>
        /// tableau des chars de la cle pour remplacer les chars du login
        /// </summary>
        private char[] _keyPasse;

        /// <summary>
        /// tableau de valeur en int des chars de la clé 
        /// </summary>
        private int[] _keyValue;

        /// <summary>
        /// méthode pour chiffrer le login avec le chiffrement vigenere
        /// </summary>
        /// <returns> le mot de passe chiffrer </returns>
        public string EncryptionVigenere()
        {
            // stocker la cle
            string key = null;
            _key.CheckOrGenerateKey();// verifier la cle de l'utilisateur
            // stocker la cle decrypte
            key = _key.DecryptionKeyVigenere();

            // instancier le tableu de valeurs des chars
            _keyValue = new int[_userName.Length];
            // instancier le tableau de chars qui contient les chars de la clé
            int[] cryptedLogin = new int[_userName.Length];
            // instancier le tableu qui contients les valeurs des chars du login d'origine
            _charValues = new int[_userName.Length];

            int valuesLogin;// variable qui stock la valeur du char du Login
            int valuesKey;// variable qui stock la valeur du char de la clé

            _chars = _userName.ToCharArray();// tableau des chars du login
            _keyChars = key.ToCharArray();// tableau des chars de la cle 
            _keyPasse = new char[_userName.Length];// tableau des chars de la cle pour remplacer le login 
            int cptr = 0;// compteur pour les chars de la cle

            // var pour stocker le psencripte
            string encriptedLogin = null;

            // repeter la cle selon la taille du Login
            for (int i = 0; i < _userName.Length; i++)
            {
                // si i est égale à la taille de la clé
                if (cptr == key.Length)
                {
                    cptr = 0; // compteur repasse à 0
                }
                _keyPasse[i] = _keyChars[cptr];
                cptr++;// incrementer le compteur
            }
            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _keyPasse.Length; i++)
            {
                valuesLogin = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesLogin;// stocker la valeur de chaque char

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de l'addition de la cle + le login et le modulo pour avoir la table ascii etendue
                cryptedLogin[i] = (_charValues[i] + _keyValue[i]) % 256;

                encriptedLogin += Convert.ToChar(cryptedLogin[i]);// concatenation des chars
            }
            // login chiffrer
            return encriptedLogin;
        }

        /// <summary>
        /// méthode pour dechiffrer le login avec le chiffrement vigenere
        /// </summary>
        /// <returns> login dechiffre </returns>
        public string DencryptionVigenere()
        {
            // stocker la cle
            string key = null;
            _key.CheckOrGenerateKey();// verifier la cle de l'utilisateur

            // stocker la cle decrypte
            key = _key.DecryptionKeyVigenere();

            // instancier le tableu de valeurs des chars
            _keyValue = new int[_userName.Length];
            // instancier le tableau de chars qui contient les chars de la clé
            int[] decryptedLoginArray = new int[_userName.Length];
            // instancier le tableu qui contients les valeurs valeurs des chars du login d'origine
            _charValues = new int[_userName.Length];

            int valuesLogin;// variable qui stock la valeur du char du Login
            int valuesKey;// variable qui stock la valeur du char de la clé

            _chars = _userName.ToCharArray();// tableau des chars du login
            _keyChars = key.ToCharArray();// tableau des chars de la cle 
            _keyPasse = new char[_userName.Length];// tableau des chars de la cle pour remplacer le login 
            int cptr = 0;// compteur pour les chars de la cle

            // var pour stocker le login encripte
            string decriptedLogin = null;

            // repeter la cle selon la taille du Login
            for (int i = 0; i < _userName.Length; i++)
            {
                // si i est égale à la taille de la clé
                if (cptr == key.Length)
                {
                    cptr = 0; // compteur repasse à 0
                }
                _keyPasse[i] = _keyChars[cptr];
                cptr++;// incrementer le compteur
            }
            // transformer le char en int pour chaque cas du tableau
            for (int i = 0; i < _keyPasse.Length; i++)
            {
                valuesLogin = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesLogin;// stocker la valeur de chaque char

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de la sustraction de la cle - le login et le modulo pour avoir la table ascii etendue
                decryptedLoginArray[i] = (_charValues[i] - _keyValue[i] + 256) % 256;

                decriptedLogin += Convert.ToChar(decryptedLoginArray[i]);// concatenation des chars
            }
            // login dechiffre
            return decriptedLogin;
        }
    }
}
