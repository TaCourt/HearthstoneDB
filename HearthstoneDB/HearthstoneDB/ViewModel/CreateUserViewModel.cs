using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthstoneDB.Models;
using Library;
using TD1.Events;

namespace HearthstoneDB.ViewModel
{
    public class CreateUserViewModel : NotifyPropertyChangedBase
    {

        public DelegateCommand OnSaveUserCommand { get; set; }
        public DelegateCommand OnCancelCommand { get; set; }

        public bool IsAdd { get; set; } = false;
        private User _userToAdd;
        public User UserToAdd
        {
            get
            {
                return _userToAdd;
            }
            set
            {
                _userToAdd = value;
                NotifyPropertyChanged("User");
            }
        }

        public CreateUserViewModel()
        {
            OnSaveUserCommand = new DelegateCommand(SaveUserAction, CanSaveUserCommand);
            OnCancelCommand = new DelegateCommand(CancelAction, CanCancelCommand);

            UserToAdd = new User();
        }

        private void SaveUserAction(Object o)
        {
            IsAdd = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void CancelAction(Object o)
        {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }



        private bool CanSaveUserCommand(Object o)
        {
            return true;
        }


        private bool CanCancelCommand(Object o)
        {
            return true;
        }
    }
}
