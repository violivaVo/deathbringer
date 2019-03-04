using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace DeathBringer.Wpf.ViewModels
{
    public class UtenteViewModel: ViewModelBase
    {
        private string _UserName;
        private string _Email;
        private string _NomeCompleto;
        private bool _IsFromMilano;
        private bool _IsExpanded;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(() => UserName); }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; RaisePropertyChanged(() => Email); }
        }

        public string NomeCompleto
        {
            get { return _NomeCompleto; }
            set { _NomeCompleto = value; RaisePropertyChanged(() => NomeCompleto); }
        }

        public bool IsFromMilano
        {
            get { return _IsFromMilano; }
            set { _IsFromMilano = value; RaisePropertyChanged(() => IsFromMilano); }
        }

        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set { _IsExpanded = value; RaisePropertyChanged(() => IsExpanded); }
        }

        public RelayCommand ToggleCommand { get; set; }

        public UtenteViewModel()
        {
            ToggleCommand = new RelayCommand(() => IsExpanded = !IsExpanded);

            //Se a design mode
            if (IsInDesignMode)
            {
                UserName = "mario.rossi";
                Email = "mario.rossi@icubed.it";
                NomeCompleto = "Mario Rossi";
                IsFromMilano = true;
                IsExpanded = true;                
            }
        }

        public UtenteViewModel(object model)
        {
            //Riempio le proprietà
            //UserName = model.Username;
            //Email = model.Email;
            //NomeCompleto = $"{model.Nome} {model.Cognome}";
            //IsFromMilano = model.Citta == "Milano";
        }
    }
}
