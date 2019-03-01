using DeathBringer.Terminal.Entities;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathBringer.Wpf.ViewModels
{
    public class UtenteViewModel: ViewModelBase
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string NomeCompleto { get; set; }

        public bool IsFromMilano { get; set; }

        public UtenteViewModel()
        {
            //Se a design mode
            if (IsInDesignMode)
            {
                UserName = "mario.rossi";
                Email = "mario.rossi@icubed.it";
                NomeCompleto = "Mario Rossi";
                IsFromMilano = true;
            }
        }

        public UtenteViewModel(Utente model)
        {
            //Riempio le proprietà
            UserName = model.Username;
            Email = model.Email;
            NomeCompleto = $"{model.Nome} {model.Cognome}";
            IsFromMilano = model.Citta == "Milano";
        }
    }
}
