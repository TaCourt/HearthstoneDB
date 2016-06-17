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

        private User _authenticatedUser;

        public User AuthenticatedUser
        {
            get { return _authenticatedUser; }
            set
            {
                _authenticatedUser = value;
                NotifyPropertyChanged("AuthenticatedUser");
            }
        }

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

        public ListCardViewModel()
        {
            CardList = new ObservableCollection<Card>()
            {
                new Spell
                {
                    
                    Name = "Pyroblast",
                    Rarity = "Epic",
                    Cost = 10,
                    Effect = "Deals 10 damage",
                    Story = "Take the time for an evil laugh after you draw this card.",
                    ImagePath = "..\\Images\\Cards\\pyro.png"
                    
                },
                new Minion
                {
                    Name = "Leeroy Jenkins",
                    ImagePath = "..\\Images\\Cards\\leeroy.png",
                    Strength = 6,
                    HealthPoints = 2,
                    Cost = 5,
                    Effect = "Charge. Battlecry : Summons two 1/1 Whelps for your opponent.",
                    Rarity = "Legendary",
                    Story = "At least he has angry chicken"  
                },
                new Minion
                {
                    Name = "Alextraza",
                    ImagePath = "..\\Images\\Cards\\Alex.png",
                    Strength = 8,
                    HealthPoints = 8,
                    Cost = 9,
                    Effect = "Battlecry : Set a hero's remaining Health to 15.",
                    Rarity = "Legendary",
                    Story = "Alexstrasza the Life-Binder brings life and hope to everyone. Except Deathwing. And Malygos. And Nekros."
                },
                new Minion
                {
                    Name = "Doomsayer",
                    ImagePath = "..\\Images\\Cards\\Doomsayer.png",
                    Strength = 0,
                    HealthPoints = 7,
                    Cost = 2,
                    Effect = "At the start of your turn, destroy ALL minions.",
                    Rarity = "Epic",
                    Story = "He's almost been right so many times. He was sure it was coming during the Cataclysm."
                },
                new Minion
                {
                    Name = "Flamewaker",
                    ImagePath = "..\\Images\\Cards\\Flamewaker.png",
                    Strength = 2,
                    HealthPoints = 4,
                    Cost = 3,
                    Effect = "After you cast a spell, deal 2 damage randomly split among all enemies.",
                    Rarity = "Rare",
                    Story = "Flamewakers HATE being confused for Flamewalkers. They just wake up fire, they don’t walk on it. Walking on fire is CRAZY."
                },
                new Spell
                {
                    Name = "Equality",
                    ImagePath = "..\\Images\\Cards\\Equality.png",
                    Cost = 2,
                    Effect = "Change the Health of ALL minions to 1.",
                    Rarity = "Rare",
                    Story = "We are all special unique snowflakes... with 1 Health."
                },
                new Minion
                {
                    Name = "Acolyte of pain",
                    ImagePath = "..\\Images\\Cards\\AcolyteofPain.png",
                    Strength = 1,
                    HealthPoints = 3,
                    Cost = 3,
                    Effect = "Whenever this minion takes damage, draw a card.",
                    Rarity = "Common",
                    Story = "He trained when he was younger to be an acolyte of joy, but things didn’t work out like he thought they would."
                },
                new Spell
                {
                    Name = "Ice barrier",
                    ImagePath = "..\\Images\\Cards\\IceBarrier.png",
                    Cost = 3,
                    Effect = "Secret: When your hero is attacked, gain 8 Armor.",
                    Rarity = "Common",
                    Story = "This is Rank 1. Rank 2 is Chocolate Milk Barrier."
                }


            };

            CardListToShow = CardList;
            IsLayoutVisible = false;
            IsGoldenChecked = false;
            OnAddCommand = new DelegateCommand(AddAction, CanAddCommand);
            OnEditCommand = new DelegateCommand(EditAction, CanEditCommand);
            OnDeleteCommand = new DelegateCommand(DeleteAction, CanDeleteCommand);
            OnSearchCommand = new DelegateCommand(SearchAction, CanSearchCommand);
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

            NotifyPropertyChanged("CardList");
         }


         private void DeleteAction(Object o)
         {

             MessageBoxResult message = MessageBox.Show("Are you sure you want to delete this card?", "Delete confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(message == MessageBoxResult.Yes){
                CardList.Remove(Card);
                NotifyPropertyChanged("CardList");
            }

         }

        private void SearchAction(Object o)
        { 
            CardListToShow = new ObservableCollection<Card>(CardList.Where(c => c.Name == SearchBar));
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

    }
}