using HearthstoneDB.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HearthstoneDB.ViewModel;

namespace HearthstoneDB
{
    /// <summary>
    /// Logique d'interaction pour AddView.xaml
    /// </summary>
    public partial class AddView : Window
    {

        public AddViewModel ViewModel { get; set; }

        public AddView()
        {

            InitializeComponent();
            ViewModel = new AddViewModel();
            DataContext = ViewModel;
        }

        public AddView(Card c)
        {
            InitializeComponent();
            ViewModel = new AddViewModel(c);
            DataContext = ViewModel;
        }       
    }
}
