﻿using System.Windows.Controls;
using SGTC.ViewModels;

namespace SGTC.Views
{
    public partial class TopLoad : UserControl
    {
        public TopLoad()
        {
            InitializeComponent();
        }

        public TopLoad(TopLoadViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

    }
}
