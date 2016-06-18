using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HearthstoneDB.ViewModel;
using HearthstoneDB.Models;


namespace HearthstoneDB
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ListCardViewModel _viewModel;

        public MainWindow(User u)
        {
            InitializeComponent();
            _viewModel = new ListCardViewModel(u);
            DataContext = _viewModel;

        }
    }
}
