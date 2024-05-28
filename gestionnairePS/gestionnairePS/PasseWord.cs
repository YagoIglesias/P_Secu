/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 19.03.24
/// Description : Classe qui permet la creation de l'objet mot de passe.
///               Avec la méthode EncryptionPassewordVigenere(bool isCrypted) on peut chiffrer ou dechiffrer le mot de pas.
///               Si le booléan est vrais alors le mot de passe est encrypte et si non decrypte.

using System;

namespace gestionnairePS
{
    internal class PasseWord
    {
        /// <summary>
        /// consatante pour decaler 
        /// </summary>
        private const int _DECALAGE = 3;

        /// <summary>
        /// variable pour le mot de passe
        /// </summary>
        private string _passeword;

        /// <summary>
        /// tableau pour stocker les characteres du PS
        /// </summary>
        private char[] _chars;

        /// <summary>
        /// tableau pour stocker les valeurs des characteres
        /// </summary>
        private int[] _charValues;

        /// <summary>
        /// Recuperer ou mettre a jour le mot de passe 
        /// </summary>
        public string Passeword { get { return _passeword; } set { _passeword = value; } }

        /// <summary>
        /// Constructeur du ps 
        /// </summary>
        /// <param name="passeword"> mot de passe </param>
        public PasseWord(string passeword)
        {
            _passeword = passeword;
        }

        /// <summary>
        /// méthode pour chiffrer le mot de passe avec césar
        /// </summary>
        public string Encryption()
        {
            // instancier le tableau de valeurs
            _charValues = new int[_passeword.Length];

            // var pour stocker la valeur du char
            int value;

            // separer le ps en char et stock les chars dans un tableau
            _chars = _passeword.ToCharArray();
                                       
            // tableau pour stocker les nouveau characteres
            char[] newArray = new char[_passeword.Length];

            // variable pour stocker la concatenation des characteres modifies 
            string encriptedPs;

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
            encriptedPs = new string(newArray);
            // returner le ps encripte
            return encriptedPs;
        }

        /// <summary>
        /// méthode pour afficher le mot de passe avec césar
        /// </summary>
        public string Decryption()
        {
            // instancier le tableau des valeurs du char
            _charValues = new int[_passeword.Length];
            int values;// stocker les valeurs du char
            _chars = _passeword.ToCharArray();// separer le ps en char et les stocker dans le tableau 
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
        /// instancier l'objet clé pour encrypter le mot de passe avec 
        /// </summary>
        KeyPasse _key = new KeyPasse();

        /// <summary>
        /// tableau pour les caractères de la clé
        /// </summary>
        private char[] _keyChars;

        /// <summary>
        /// tableau des caractères de la cle pour remplacer le ps
        /// </summary>
        private char[] _keyPasse;

        /// <summary>
        /// tableau de valeur en int des caractères de la clé 
        /// </summary>
        private int[] _keyValue;

        /// <summary>
        /// méthode pour chiffrer ou dechiffrer le mot de passe avec le chiffrement vigenere
        /// </summary>
        /// <param name="isCrypted"> verifier si il faut crypter ou decrypter </param>
        /// <returns> passeword crypter ou decrypter </returns>
        public string EncryptionPassewordVigenere(bool isCrypted)
        {
            // stocker la cle
            string key = null;
            _key.CheckOrGenerateKey();// verifier la clé de l'utilisateur
            // stocker la cle decrypte
            key = _key.EncryptionKeyVigenere(false);

            // instancier le tableu de valeurs des caractères
            _keyValue = new int[_passeword.Length];
            // instancier le tableau qui contien la valeur des caractères du mot de passe crypter ou decrypter 
            int[] charsValuesPasseword = new int[_passeword.Length];
            // instancier le tableu qui contients les valeurs des caractères du mot de passe d'origine
            _charValues = new int[_passeword.Length];

            int valuesPs;// variable qui stock la valeur du caractère du ps
            int valuesKey;// variable qui stock la valeur du caractère de la clé

            _chars = _passeword.ToCharArray();// tableau des caractères du ps
            _keyChars = key.ToCharArray();// tableau des caractères de la cle 
            _keyPasse = new char[_passeword.Length];// tableau des caractères de la cle pour remplacer le ps 
            int counter = 0;// compteur pour les chars de la cle

            // var pour stocker le mot de passe chiffrer ou dechiffrer
            string password = null;

            // repeter la cle selon la taille du passeWord
            for (int i = 0; i < _passeword.Length; i++)
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
                valuesPs = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesPs;// stocker la valeur de chaque caractère

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque caractère

                if (isCrypted == true)
                {
                    // stocker le nouveau char qui sort de l'addition de la cle + le passeWord et le modulo pour avoir la table ascii etendue
                    charsValuesPasseword[i] = (_charValues[i] + _keyValue[i] + 256) % 256;
                }
                else
                {
                    // stocker le nouveau char qui sort de la sustraction de la cle - le passeWord et le modulo pour avoir la table ascii etendue
                    charsValuesPasseword[i] = (_charValues[i] - _keyValue[i] + 256) % 256;
                }
                password += Convert.ToChar(charsValuesPasseword[i]);// concatenation des chars
            }
            // mot de passe  chiffrer ou dechiffrer 
            return password.Trim();
        }
    }
}
