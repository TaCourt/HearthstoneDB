using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HearthstoneDB.Models;
using Library;
using Microsoft.Win32;
using TD1.Events;

namespace HearthstoneDB.ViewModel
{
    public class AddViewModel : NotifyPropertyChangedBase
    {
        public DelegateCommand OnBrowseCommand { get; set; }
        public DelegateCommand OnSaveCommand { get; set; }
        public DelegateCommand OnCancelCommand { get; set; }

        private Card _card;
        public Card CardToAdd
        {
            get
            {
                return _card ;
            }
            set
            {
                _card = value;
                NotifyPropertyChanged("Card");
            }
        }

        public bool IsAdd { get; set; } = false;

        public AddViewModel()
        {
            OnBrowseCommand = new DelegateCommand(BrowseAction, CanBrowseCommand);
            OnSaveCommand = new DelegateCommand(SaveAction, CanSaveCommand);
            OnCancelCommand = new DelegateCommand(CancelAction, CanCancelCommand);

            CardToAdd = new Card();
        }


        public AddViewModel( Card c)
        {
            OnBrowseCommand = new DelegateCommand(BrowseAction, CanBrowseCommand);
            OnSaveCommand = new DelegateCommand(SaveAction, CanSaveCommand);
            OnCancelCommand = new DelegateCommand(CancelAction, CanCancelCommand);

            CardToAdd = c;
        }
        private void BrowseAction(Object o)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Add card picture";
            fileDialog.ShowDialog();

            string fullPath = fileDialog.FileName;
            CardToAdd.ImagePath = fullPath;

        }



        private void SaveAction(Object o)
        {
            IsAdd = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void CancelAction(Object o)
        {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);

        }



        private bool CanBrowseCommand(Object o)
        {
            return true;
        }

        private bool CanSaveCommand(Object o)
        {
            return true;
        }

        private bool CanCancelCommand(Object o)
        {
            return true;
        }

    }
}
