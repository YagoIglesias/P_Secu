/// ETML
/// Auteur: Yago Iglesias Rodriguez
/// Date: 30.04.2024
/// Description : Classe qui va permettre de créér l'objet login.
///               Avec la méthode EncryptionLoginVigenere(bool isCrypted) on peut chiffrer ou dechiffrer le login.
///               Si le booléan est vrais alors le login est encrypte et si non decrypte.

using System;

namespace gestionnairePS
{
    internal class Login
    {
        /// <summary>
        /// consatante pour decaler le char 
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
        /// Constructeur pour le login
        /// </summary>
        /// <param name="login"> username / login </param>
        public Login(string login)
        {
            _userName = login;
        }

        /// <summary>
        /// méthode pour chiffrer le login avec la m´thode de césar
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
        /// méthode pour afficher le login en claire avec la méthode de césar 
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
        /// instancier l'objet clé pour encrypter le login avec 
        /// </summary>
        KeyPasse _key = new KeyPasse();

        /// <summary>
        /// tableau pour les caractère de la clé
        /// </summary>
        private char[] _keyChars;

        /// <summary>
        /// tableau des caractères de la clé pour remplacer les caractères du login
        /// </summary>
        private char[] _keyPasse;

        /// <summary>
        /// tableau de valeur en int des caractères de la clé 
        /// </summary>
        private int[] _keyValue;

        /// <summary>
        /// méthode pour encripter et decripter le login selon le chiffrement de vigènere 
        /// </summary>
        /// <param name="isCrypted"> determine si c'est a crypter ou decrypter </param>
        /// <returns> retourne le login chiffrer ou dechiffrer </returns>
        public string EncryptionLoginVigenere(bool isCrypted)
        {
            // stocker la cle
            string key = null;
            _key.CheckOrGenerateKey();// verifier la clé de l'utilisateur
            // stocker la clé decrypte
            key = _key.EncryptionKeyVigenere(false);

            // instancier le tableu de valeurs des caractères
            _keyValue = new int[_userName.Length];
            // instancier le tableau qui contien la valeur des caractères du login crypter ou decrypter 
            int[] charsValuesLogin = new int[_userName.Length];
            // instancier le tableu qui contients les valeurs des caractères du login d'origine
            _charValues = new int[_userName.Length];

            int valuesLogin;// variable qui stock la valeur du char du Login
            int valuesKey;// variable qui stock la valeur du char de la clé

            _chars = _userName.ToCharArray();// tableau des caractères du login
            _keyChars = key.ToCharArray();// tableau des caractères de la clé 
            _keyPasse = new char[_userName.Length];// tableau des caractères de la clé pour remplacer le login 
            int counter = 0;// compteur pour les caractères de la clé

            // var pour stocker le login
            string login = null;

            // repeter la cle selon la taille du Login
            for (int i = 0; i < _userName.Length; i++)
            {
                // si i est égale à la taille de la clé
                if (counter == key.Length)
                {
                    counter = 0; // compteur repasse à 0
                }
                _keyPasse[i] = _keyChars[counter];
                counter++;// incrementer le compteur
            }
            // transformer le caractère en int pour chaque cas du tableau
            for (int i = 0; i < _keyPasse.Length; i++)
            {
                valuesLogin = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesLogin;// stocker la valeur de chaque caractère

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque caractère

                if (isCrypted == true)
                {
                    // stocker le nouveau char qui sort de l'addition de la cle + le login et le modulo pour avoir la table ascii etendue
                    charsValuesLogin[i] = (_charValues[i] + _keyValue[i] + 256) % 256;
                }
                else
                {
                    // stocker le nouveau char qui sort de l'addition de la cle - le login + 256 et le modulo pour avoir la table ascii etendue
                    charsValuesLogin[i] = (_charValues[i] - _keyValue[i] + 256) % 256;
                }
                login += Convert.ToChar(charsValuesLogin[i]);// concatenation des caractère
            }
            // login chiffrer ou dechiffrer
            return login;
        }
    }
}
