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
using SGTC.ViewModels;
using SGTC.Models;
namespace SGTC.Views
{
    /// <summary>
    /// Interaction logic for ResultGraph.xaml
    /// </summary>
    public partial class ResultGraph : Window
    {
        public ResultGraph(ResultGraphViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
