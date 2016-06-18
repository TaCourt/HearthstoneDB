using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Library;
using System.Collections.ObjectModel;
using HearthstoneDB.Models;
using System.Windows;
using TD1.Events;
using System.Windows.Media;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace HearthstoneDB.ViewModel
{
    public class ListCardViewModel : NotifyPropertyChangedBase
    {
        
        public DelegateCommand OnAddCommand { get; set; }
        public DelegateCommand OnEditCommand { get; set; }
        public DelegateCommand OnDeleteCommand { get; set; }
        public DelegateCommand OnSearchCommand { get; set; }

        #region Font

        public FontFamily FontFamily
        {
            get
            {
                return new FontFamily("Georgia");
            }
        }

        public double FontSize { get; set; } = 15;
        #endregion

        User User;
        public bool _isGoldenNotChecked;
        public bool IsGoldenNotChecked
        {
            get
            {
                return _isGoldenNotChecked;
            }
            set
            {
                _isGoldenNotChecked = value;
                NotifyPropertyChanged("IsGoldenNotChecked");
            }
        }

        public bool _isGoldenChecked;
        public bool IsGoldenChecked
        {
            get
            {
                return _isGoldenChecked;
            }
            set
            {
                _isGoldenChecked = value;
                IsGoldenNotChecked = !value;
                NotifyPropertyChanged("IsGoldenChecked");
            }
        }
        
        private String _searchBar;
        public String SearchBar
        {
            get
            {
                return _searchBar;
            }
            set
            {
                _searchBar = value;
                NotifyPropertyChanged("SearchBar");
                OnSearchCommand.RaiseCanExecuteChanged();
                if (_searchBar == "")
                    CardListToShow = CardList;
            }
        }

        private bool _isLayoutNotVisible;
        public bool IsLayoutNotVisible
        {
            get
            {
                return _isLayoutNotVisible;
            }
            set
            {
                _isLayoutNotVisible = value;
                NotifyPropertyChanged("IsLayoutNotVisible");
            }
        }


        private bool _isLayoutVisible;
        public bool IsLayoutVisible
        {
            get
            {
                return _isLayoutVisible;
            }
            set
            {
                _isLayoutVisible = value;
                IsLayoutNotVisible = !value;
                NotifyPropertyChanged("IsLayoutVisible");
            }
        }

        #region EtatsFiltres
        public bool _isCommonChecked;
        public bool IsCommonChecked
        {
            get
            {
                return _isCommonChecked;
            }
            set
            {
                _isCommonChecked = value;
                filter(IsCommonChecked, IsRareChecked, IsEpicChecked, IsLegendaryChecked);
            }
        }

        public bool _isRareChecked;
        public bool IsRareChecked
        {
            get
            {
                return _isRareChecked;
            }
            set
            {
                _isRareChecked = value;
                filter(IsCommonChecked, IsRareChecked, IsEpicChecked, IsLegendaryChecked);
            }
        }

        public bool _isEpicChecked;
        public bool IsEpicChecked
        {
            get
            {
                return _isEpicChecked;
            }
            set
            {
                _isEpicChecked = value;
                filter(IsCommonChecked, IsRareChecked, IsEpicChecked, IsLegendaryChecked);
            }
        }

        public bool _isLegendaryChecked;
        public bool IsLegendaryChecked
        {
            get
            {
                return _isLegendaryChecked;
            }
            set
            {
                _isLegendaryChecked = value;
                filter(IsCommonChecked, IsRareChecked, IsEpicChecked, IsLegendaryChecked);
            }
        }
        #endregion

        public AddView Add { get; set; }
        private AddView _addView { get; set; }


        private Card _card;
        public Card Card
        {
            get
            {
                return _card;
            }
            set
            {
                _card = value;
                IsLayoutVisible = true;
                NotifyPropertyChanged("Card");
                NotifyPropertyChanged("CardList");
                OnEditCommand.RaiseCanExecuteChanged();
                OnDeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private ObservableCollection<Card> _cardListToShow;

        public ObservableCollection<Card> CardListToShow
        {
            get
            {
                return _cardListToShow;
            }
            set
            {
                _cardListToShow = value;
                NotifyPropertyChanged("CardListToShow");
            }
        }

        private ObservableCollection<Card> _cardList;

        public ObservableCollection<Card> CardList
        {
            get
            {
                return _cardList;
            }
            set
            {
                _cardList = value;
                NotifyPropertyChanged("CardList");
            }
        }

        public ListCardViewModel(User u)
        {
            User = u;
            IsLayoutVisible = false;
            IsGoldenChecked = false;
            OnAddCommand = new DelegateCommand(AddAction, CanAddCommand);
            OnEditCommand = new DelegateCommand(EditAction, CanEditCommand);
            OnDeleteCommand = new DelegateCommand(DeleteAction, CanDeleteCommand);
            OnSearchCommand = new DelegateCommand(SearchAction, CanSearchCommand);
            Load(u);
            CardListToShow = CardList;
        }



         private void AddAction(Object o)
         {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;

            Add = new AddView();
            Add.Name = "Add";
            Add.ShowDialog();


            if (Add.ViewModel.IsAdd)
            {
                CardList.Add(Add.ViewModel.CardToAdd);
                Save(User);
            }


            NotifyPropertyChanged("CardList");
         }

        private void CloseAddView(object sender, EventArgs e)
        {
            
            Add.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseAddView;
        }


        private void EditAction(Object o)
         {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;
            Add = new AddView(Card);
            Add.Name = "Edit";
            Add.ShowDialog();

            if (Add.ViewModel.IsAdd)
            {
                CardList.Remove(Card);
                CardList.Add(Add.ViewModel.CardToAdd);
                Card = CardList.First(c => c.Name == Add.ViewModel.CardToAdd.Name);
                Save(User);
            }

            NotifyPropertyChanged("CardList");
         }


         private void DeleteAction(Object o)
         {
             MessageBoxResult message = MessageBox.Show("Are you sure you want to delete this card?", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(message == MessageBoxResult.Yes){
                CardList.Remove(Card);
                NotifyPropertyChanged("CardList");
                IsLayoutVisible = false;
                Save(User);
            }


         }

        private void SearchAction(Object o)
        { 

            CardListToShow = new ObservableCollection<Card>(CardList.Where(c => c.Name.ToLower().Contains(SearchBar.ToLower())));
            if (CardListToShow.Count == 1)
            {
                Card = CardListToShow.First();
                NotifyPropertyChanged("Card");
            }
            NotifyPropertyChanged("CardListToShow");
           

        }



        private void filter(bool IsCommonChecked, bool IsRareChecked, bool IsEpicChecked, bool IslegendaryChecked)
        {

            ObservableCollection<Card> CommonList = new ObservableCollection<Card>();
            ObservableCollection<Card> RareList = new ObservableCollection<Card>();
            ObservableCollection<Card> EpicList = new ObservableCollection<Card>();
            ObservableCollection<Card> LegendaryList = new ObservableCollection<Card>();

            ObservableCollection<Card> BigList;
            
            if (IsCommonChecked)
                CommonList = new ObservableCollection<Card>(CardList.Where(c => c.Rarity == "Common"));
            if(IsRareChecked)
                RareList = new ObservableCollection<Card>(CardList.Where(c => c.Rarity == "Rare"));
            if(IsEpicChecked)
                EpicList = new ObservableCollection<Card>(CardList.Where(c => c.Rarity == "Epic"));
            if(IsLegendaryChecked)
                LegendaryList = new ObservableCollection<Card>(CardList.Where(c => c.Rarity == "Legendary"));

            if (IsCommonChecked || IsEpicChecked || IsRareChecked || IsLegendaryChecked)
            {
                BigList = new ObservableCollection<Card>(CommonList.Union(RareList));
                BigList = new ObservableCollection<Card>(BigList.Union(EpicList));
                BigList = new ObservableCollection<Card>(BigList.Union(LegendaryList));
                BigList = new ObservableCollection<Card>(BigList.OrderBy(c => c.Cost));
                CardListToShow = BigList;
            }
            else
            {
                CardListToShow = CardList;
            }

        }


        private bool CanAddCommand(Object o)
        {
            return true;
        }

        private bool CanEditCommand(Object o)
        {
            return Card != null;
        }

        private bool CanDeleteCommand(Object o)
        {
            return Card != null;
        }

        private bool CanSearchCommand(Object o)
        {
            return !String.IsNullOrEmpty(SearchBar);
        }


        public void Save(User user)
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(user.SaveFile, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, CardList);
            stream.Close();
            
        }

        public void Load(User user)
        {

            ObservableCollection<Card> FromFile = null;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(user.SaveFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            if (new FileInfo(user.SaveFile).Length != 0)
            {
                FromFile = (ObservableCollection<Card>)formatter.Deserialize(stream);
                CardList = FromFile;
            }
            else
            {
                CardList = new ObservableCollection<Card>();
            }
            stream.Close();
        }


    }
}