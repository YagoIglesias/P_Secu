/// ETML
/// Auteur : Yago Iglesias Rodriguez
/// Date : 19.03.24
/// Description : Classe qui permet la creation de mots de passe, les encrypter et les decrypter avec les méthodes EncryptionVigenere et DecryptionVigenere()

using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Cryptography;
using System.IO;

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
        private string _ps;

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
        public string Ps { get { return _ps; } set { _ps = value; } }

        /// <summary>
        /// Recuperer ou mettre a jour le tableau de valeurs
        /// </summary>
        public int[] CharValues { get { return _charValues; } set { _charValues = value; } }

        /// <summary>
        /// Constructeur du ps 
        /// </summary>
        /// <param name="ps"> mot de passe </param>
        public PasseWord(string ps)
        {
            _ps = ps;
        }

        /// <summary>
        /// méthode pour chiffrer le ps
        /// </summary>
        public string Encryption()
        {
            // instancier le tableau de valeurs
            _charValues = new int[_ps.Length];

            // var pour stocker la valeur du char
            int value;

            // separer le ps en char et stock les chars dans un tableau
            _chars = _ps.ToCharArray();
                                       
            // tableau pour stocker les nouveau characteres
            char[] newArray = new char[_ps.Length];

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
        /// méthode pour afficher le mot de passe en claire
        /// </summary>
        public string Decryption()
        {
            // instancier le tableau des valeurs du char
            _charValues = new int[_ps.Length];
            int values;// stocker les valeurs du char
            _chars = _ps.ToCharArray();// separer le ps en char et les stocker dans le tableau 
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
        /// instancier l'objet pour la clé
        /// </summary>
        KeyPasse _key = new KeyPasse();

        /// <summary>
        /// tableau pour les chars de la clé
        /// </summary>
        private char[] _keyChars;

        /// <summary>
        /// tableau des chars de la cle pour remplacer le ps
        /// </summary>
        private char[] _keyPasse;

        /// <summary>
        /// tableau de valeur en int des chars de la clé 
        /// </summary>
        private int[] _keyValue;

        /// <summary>
        /// méthode pour chiffrer le mot de passe avec le chiffrement vigenere
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
            _keyValue = new int[_ps.Length];
            // instancier le tableau de chars qui contient les chars de la clé
            int[] cryptedPs = new int[_ps.Length];
            // instancier le tableu qui contients les valeurs valeurs des chars du ps d'origine
            _charValues = new int[_ps.Length];

            int valuesPs;// variable qui stock la valeur du char du ps
            int valuesKey;// variable qui stock la valeur du char de la clé

            _chars = _ps.ToCharArray();// tableau des chars du ps
            _keyChars = key.ToCharArray();// tableau des chars de la cle 
            _keyPasse = new char[_ps.Length];// tableau des chars de la cle pour remplacer le ps 
            int cptr = 0;// compteur pour les chars de la cle

            // var pour stocker le psencripte
            string encriptedPs = null;

            // repeter la cle selon la taille du passeWord
            for (int i = 0; i < _ps.Length; i++)
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
                valuesPs = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesPs;// stocker la valeur de chaque char

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de l'addition de la cle + le passeWord et le modulo pour avoir la table ascii etendue
                cryptedPs[i] = (_charValues[i] + _keyValue[i]) % 256;

                encriptedPs += Convert.ToChar(cryptedPs[i]);// concatenation des chars
            }
            // ps chiffrer
            return encriptedPs;
        }

        /// <summary>
        /// méthode pour dechiffrer le passeWord avec le dechiffrement vigenere
        /// </summary>
        /// <returns> mot de passe dechiffrer </returns>
        public string DecryptionVigenere()
        {
            // stocker la cle
            string key = null;
            _key.CheckOrGenerateKey();// verifier la cle de l'utilisateur
            // stocker la cle decrypte
            key = _key.DecryptionKeyVigenere();

            // instancier le tableu de valeurs des chars
            _keyValue = new int[_ps.Length];
            // instancier le tableau de chars qui contient les chars de la clé
            int[] decryptedPs = new int[_ps.Length];
            // instancier le tableu qui contients les valeurs des chars du ps d'origine
            _charValues = new int[_ps.Length];

            int valuesPs;// variable qui stock la valeur du char du ps
            int valuesKey;// variable qui stock la valeur du char de la clé

            _chars = _ps.ToCharArray();// tableau des chars du ps
            _keyChars = key.ToCharArray();// tableau des chars de la cle 
            _keyPasse = new char[_ps.Length];// tableau des chars de la cle pour remplacer le ps 
            int cptr = 0;// compteur pour les chars de la cle

            string dencriptedPs = null;// stocker le ps decripte

            // repeter la cle selon la taille du passeWord
            for (int i = 0; i < _ps.Length; i++)
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
                valuesPs = Convert.ToInt32(_chars[i]);
                _charValues[i] = valuesPs;// stocker la valeur de chaque char

                valuesKey = Convert.ToInt32(_keyPasse[i]);
                _keyValue[i] = valuesKey;// stocker la valeur de chaque char

                // stocker le nouveau char qui sort de la sustraction de la cle - le passeWord et le modulo pour avoir la table ascii etendue
                decryptedPs[i] = (_charValues[i] - _keyValue[i] + 256) % 256;

                dencriptedPs += Convert.ToChar(decryptedPs[i]);// concatenation des chars
            }
            // ps decripte
            return dencriptedPs;
        }
    }
}
