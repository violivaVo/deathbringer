using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace DeathBringer.Wpf.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        public IList<UtenteViewModel> Utenti { get; set; }

        public ICommand RefreshCommand { get; set; }

        public MainViewModel()
        {
            //Inizializzazione della lista di elementi
            Utenti = new List<UtenteViewModel>(); 

            //in design mode, lista di dati finti
            if (IsInDesignMode)
            {
                
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
            else
            {
                //Generazione di 50 elementi random
                Random random = new Random();
                for (var i = 0; i < 50; i++)
                {
                    Utenti.Add(new UtenteViewModel
                    {
                        IsExpanded = false,
                        IsFromMilano = random.Next() % 2 == 1,
                        Email = random.Next() + "@icubed.it", 
                        NomeCompleto = "Nome" + random.Next(),
                        UserName = "Username" + random.Next(),
                    });
                }
            }
        }
    }
}
