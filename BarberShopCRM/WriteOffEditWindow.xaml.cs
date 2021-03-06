﻿using BarberShopCRM.model;
using BarberShopCRM.viewmodel;
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
using System.Windows.Shapes;

namespace BarberShopCRM {
    /// <summary>
    /// Логика взаимодействия для WriteOffEditWindow.xaml
    /// </summary>
    public partial class WriteOffEditWindow : Window {
        private WriteOffEditViewModel viewModel;
        public WriteOffEditWindow (WriteOff writeOff) {
            InitializeComponent ();
            DataContext = viewModel = new WriteOffEditViewModel (this, writeOff);
        }
    }
}
