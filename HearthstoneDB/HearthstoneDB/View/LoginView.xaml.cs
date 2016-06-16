﻿using System;
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
using System.Windows.Shapes;
using HearthstoneDB.ViewModel;
using HearthstoneDB.Models;

namespace HearthstoneDB.View
{
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {

        private LoginViewModel ViewModel { get; set; }

        public LoginView()
        {
            InitializeComponent();

            ViewModel = new LoginViewModel();
            DataContext = ViewModel;
        }
    }
}
