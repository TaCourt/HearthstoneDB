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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

        private string _passwordValue;
        public string PasswordValue
        {
            set
            {
                _passwordValue = value;
                NotifyPropertyChanged("PasswordValue");
                OnLoginCommand.RaiseCanExecuteChanged();
            }
            get
            {
                return _passwordValue;
            }
        }

        public LoginViewModel()
        {
            OnCreateUserCommand = new DelegateCommand(CreateUserAction, CanCreateUserCommand);
            OnLoginCommand = new DelegateCommand(LoginAction, CanLoginCommand);
            LoadUsers();
            
        }

        private void LoginAction(Object o)
        {
            User user;
            user = usersList.Login(UsernameValue,PasswordValue);

            MainWindow main = new MainWindow(user);
            main.Name = "HearthstoneDB";
            main.ShowDialog();
            
        }

        private void CreateUserAction(Object o)
        {

            ButtonPressedEvent.GetEvent().Handler += CloseCreateView;
            createUser = new CreateUserView();
            createUser.Name = "Adduser";
            createUser.ShowDialog();

            if (createUser.ViewModel.IsAdd)
            {

                User userAdd = new User(createUser.ViewModel.UserToAdd.Username, createUser.ViewModel.UserToAdd.Password);
                usersList.UsersList.Add(userAdd);
                SaveUsers();
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
            return !string.IsNullOrEmpty(UsernameValue) && !string.IsNullOrEmpty(PasswordValue);
        }

        public void SaveUsers()
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("..\\..\\Data\\UsersList.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, usersList);
            stream.Close();

        }

        public void LoadUsers()
        {

                Users FromFile = null;
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream("..\\..\\Data\\UsersList.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                FromFile = (Users)formatter.Deserialize(stream);
                usersList = FromFile;
                stream.Close();
        }
    }
}
