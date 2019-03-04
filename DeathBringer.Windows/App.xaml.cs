using DeathBringer.Wpf.ViewModels;
using DeathBringer.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeathBringer.Windows
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
