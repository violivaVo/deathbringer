﻿using System.Windows;
using DeathBringer.Wpf.ViewModels;
using DeathBringer.Wpf.Views;

namespace DeathBringer.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Inizializzazione base
            base.OnStartup(e);

            //Creo istanza della view principale
            SignInView view = new SignInView();

            //Creo istanza del suo viewmodel
            SignInViewModel viewModel = new SignInViewModel();

            //Creo il "matrimonio"
            view.DataContext = viewModel;

            //Visualizza la view
            view.Show();
        }
    }
}
