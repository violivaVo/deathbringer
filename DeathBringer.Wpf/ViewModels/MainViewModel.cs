using DeathBringer.Core.ServiceLayers;
using DeathBringer.Terminal.Entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace DeathBringer.Wpf.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public IList<UtenteViewModel> Utenti { get; set; }

        public ICommand RefreshCommand { get; set; }

        public MainViewModel()
        {
            //in design mode, lista di dati finti
            if (IsInDesignMode)
            {
                Utenti = new List<UtenteViewModel>();
                Utenti.Add(new UtenteViewModel
                {
                    UserName = "mario",
                    Email = "mario@icubed.it", 
                    IsFromMilano = true, 
                    NomeCompleto = "Mario Rossi"
                });
                Utenti.Add(new UtenteViewModel
                {
                    UserName = "giuseppe",
                    Email = "giuseppe@icubed.it", 
                    IsFromMilano = false, 
                    NomeCompleto = "Giuseppe Verdi"
                });
                Utenti.Add(new UtenteViewModel
                {
                    UserName = "antonio",
                    Email = "antonio@icubed.it", 
                    NomeCompleto = "Antonio Bianchi", 
                    IsFromMilano = true
                });
            }
        }
    }
}
