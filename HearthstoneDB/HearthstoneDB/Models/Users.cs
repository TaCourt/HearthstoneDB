using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthstoneDB.Models
{
    [Serializable]
    public class Users
    {
        public List<User> UsersList
        {
            get;
            private set;
        }

        public Users()
        {
            UsersList = new List<User>();
        }

        /// <summary>
        /// Ajoute un utilisateur à la liste
        /// </summary>
        /// <param name="u"></param>

        private void AddUser(User u)
        {
            UsersList.Add(u);
            Directory.CreateDirectory("..\\..\\Data\\" + u.Username);
        }

        /// <summary>
        /// Supprime un utilisateur de la liste
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>

        private bool DeleteUser(User u)
        {
            return UsersList.Remove(u);
        }

        /// <summary>
        /// Trouve un utilisateur à partir de son nom
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        private User FindUser(String username)
        {
            User userFound = UsersList.Find(user => user.Username == username);

            return userFound;
        }

        /// <summary>
        /// Connecte un utilisateur
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public User Login(String username, String password)
        {
            // Regarder si l'utilisateur existe
            User userFound = FindUser(username);

            // Si l'utilisateur n'a pas été trouvé
            if (userFound == null)
            {
                // Lancer une exception
                throw new Exception("Username or password incorrect.");
            }

            // Si le mot de passe est incorrect
            if (!userFound.CheckPassword(password))
            {
                // Lancer une exception
                throw new Exception("Password incorrect.");
            }

            // Connexion reussie, retourner l'utilisateur connecté
            return userFound;
        }

        /// <summary>
        /// Créé un utilisateur
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="repeatPassword"></param>

        public void CreateUser(String username, String password, String repeatPassword)
        {
            // Regarder si l'utilisateur existe
            User userFound = FindUser(username);

            // Si l'utilisateur existe déjà
            if (userFound != null)
            {
                // Lancer une exception
                throw new Exception("User already exists.");
            }

            // Si le mot de passe et la confirmation du mot de passe ne correspondent pas
            if (password != repeatPassword)
            {
                // Lancer une exception
                throw new Exception("Passwords do not match.");
            }

            // Créer l'utilisateur
            User newUser = new User(username, password);

            // Ajouter l'utilisateur à la liste
            AddUser(newUser);
        }

    }
}
