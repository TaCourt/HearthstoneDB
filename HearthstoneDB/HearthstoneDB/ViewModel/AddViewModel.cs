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

        public string Title { get; set; }

        private bool _willItBeASpell;
        public bool WillItBeASpell
        {
            get
            {
                return _willItBeASpell;
            }
            set
            {
                _willItBeASpell = value;
                NotifyPropertyChanged("CardToAdd");
                NotifyPropertyChanged("WillitBeASpell");
            }
        }

        private bool _willItBeAMinion;
        public bool WillItBeAMinion
        {
            get
            {
                return _willItBeAMinion;
            }
            set
            {
                _willItBeAMinion = value;
                WillItBeASpell = !value;
                if (value == true)
                    CardToAdd = new Minion();
                else
                    CardToAdd = new Spell();
                NotifyPropertyChanged("CardToAdd");
                NotifyPropertyChanged("WillitBeAMinion");
            }
        }
        
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
                NotifyPropertyChanged("CardToAdd");
            }
        }


        public bool IsAdd { get; set; } = false;

        public AddViewModel()
        {
            OnBrowseCommand = new DelegateCommand(BrowseAction, CanBrowseCommand);
            OnSaveCommand = new DelegateCommand(SaveAction, CanSaveCommand);
            OnCancelCommand = new DelegateCommand(CancelAction, CanCancelCommand);
            WillItBeAMinion = true;
            CardToAdd.ImagePath = "..\\Images\\Cards\\Default.png";
            Title = "Add a card";

        }


        public AddViewModel( Card c)
        {
            OnBrowseCommand = new DelegateCommand(BrowseAction, CanBrowseCommand);
            OnSaveCommand = new DelegateCommand(SaveAction, CanSaveCommand);
            OnCancelCommand = new DelegateCommand(CancelAction, CanCancelCommand);
            if (c is Minion)
                WillItBeAMinion = true;
            else
                WillItBeAMinion = false;
            CardToAdd = c;
            Title = "Edit a card";
        }

        private void BrowseAction(Object o)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Add card picture";
            fileDialog.ShowDialog();

            string fullPath = fileDialog.FileName;
            CardToAdd.ImagePath = fullPath;
            NotifyPropertyChanged("CardToAdd");

        }



        private void SaveAction(Object o)
        {
            IsAdd = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void CancelAction(Object o)
        {
            IsAdd = false;
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
