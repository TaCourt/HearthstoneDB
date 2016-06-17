using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using HearthstoneDB.Models;
using HearthstoneDB.View;
using System.IO;
using System.Xml.Serialization;
using TD1.Events;
using System.Windows.Controls;

namespace HearthstoneDB.ViewModel
{
    public class LoginViewModel : NotifyPropertyChangedBase
    {
        private Users usersList;
        private CreateUserView createUser;
        public DelegateCommand OnLoginCommand { get; set; }
        public DelegateCommand OnCreateUserCommand { get; set; }
        private string _usernameValue;
        public string UsernameValue
        {
            set
            {
                _usernameValue = value;
                NotifyPropertyChanged("UsernameValue");
                OnLoginCommand.RaiseCanExecuteChanged();
            }
            get
            {
                return _usernameValue ;
            }
        }
        public String password;

        public LoginViewModel()
        {
            OnCreateUserCommand = new DelegateCommand(CreateUserAction, CanCreateUserCommand);
            OnLoginCommand = new DelegateCommand(LoginAction, CanLoginCommand);

            // Si le fichier users.xml existe

            if (File.Exists("users.xml"))
            {
                // On charge la liste des utilisateurs à partir du fichier

                XmlSerializer xs = new XmlSerializer(typeof(Users));
                using (StreamReader sr = new StreamReader("users.xml"))
                {
                    usersList = xs.Deserialize(sr) as Users;
                }
            }
            else
            {
                // Sinon on créé une nouvelle liste
                usersList = new Users();
            }
        }

        private void LoginAction(Object o)
        {
            usersList.Login(UsernameValue,password);
        }

        private void CreateUserAction(Object o)
        {

            ButtonPressedEvent.GetEvent().Handler += CloseCreateView;
            createUser = new CreateUserView();
            createUser.Name = "Adduser";
            createUser.ShowDialog();

            if (createUser.ViewModel.IsAdd)
            {
                usersList.UsersList.Add(createUser.ViewModel.UserToAdd);
            }

            NotifyPropertyChanged("UsersList");
        }


        private void CloseCreateView(object sender, EventArgs e)
        {
            createUser.Close();

            ButtonPressedEvent.GetEvent().Handler -= CloseCreateView;
        }


        private bool CanCreateUserCommand(Object o)
        {
            return true;
        }

        private bool CanLoginCommand(Object o)
        {
            return !string.IsNullOrEmpty(UsernameValue);
        }
    }
}
