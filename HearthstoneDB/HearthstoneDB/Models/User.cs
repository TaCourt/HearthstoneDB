using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneDB.Models
{
    [Serializable]
    public class User : IEquatable<User>
    {

        public string SaveFile { get; set; }

        public String Username
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public ObservableCollection<Card> cardList;
        
        public User()
        {

        }

        public User(string username)
        {
            ValidateUsername(username);
            this.Username = username;
        }


        public User(string username, string password) : this(username)
        {
            ValidatePassword(password);
            this.Password = password;
            SaveFile = "..\\..\\Data\\" + Username + ".bin";
            File.Create(SaveFile);
        }

        /// <summary>
        /// Vérifie le mot de passe
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>

        public bool CheckPassword(string password)
        {
            if (Password != password)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Changer mot de passe
        /// </summary>
        /// <param name="password"></param>
        /// <param name="newPassword"></param>
        /// <param name="repeatNewPassword"></param>

        public void ChangePassword(string password, string newPassword, string repeatNewPassword)
        {

            // Si le mot de passe initial est incorrect
            if (!CheckPassword(password))
            {
                // Lancer une exception
                throw new Exception("Incorrect password.");
            }

            // Si les nouveaux mot de passes ne correspondent pas
            if (newPassword != repeatNewPassword)
            {
                //Lancer une exception
                throw new Exception("New passwords do not match.");
            }

            // Change le mot de passe
            Password = newPassword;
        }

        public bool Equals(User other)
        {
            return this.Username == other.Username;
        }

        /// <summary>
        /// Vérifie le nom d'utilisateur
        /// </summary>
        /// <param name="username"></param>

        private void ValidateUsername(string username)
        {
            if (username.Length == 0)
            {
                throw new Exception("Username cannot be empty.");
            }

            if (username.Length < 3)
            {
                throw new Exception("Username is too short.");
            }

            if (username.Length > 30)
            {
                throw new Exception("Username is too long.");
            }
        }

        /// <summary>
        /// Valide le mot de passe
        /// </summary>
        /// <param name="password"></param>

        private void ValidatePassword(string password)
        {
            if (password.Length == 0)
            {
                throw new Exception("Password cannot be empty.");
            }

            if (password.Length < 6)
            {
                throw new Exception("Password is too short.");
            }

            if (password.Length > 150)
            {
                throw new Exception("Password is too long.");
            }
        }
    }
}
