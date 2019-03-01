using DeathBringer.Core.ServiceLayers;
using DeathBringer.Wpf.Messaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace DeathBringer.Wpf.ViewModels
{
    public class SignInViewModel: ViewModelBase
    {
        private string _UserName;
        private string _Password;
        private bool _IsBusy;

        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; RaisePropertyChanged(() => IsBusy); }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged(() => UserName); }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(() => Password); }
        }

        public RelayCommand SignInCommand { get; set; }

        public SignInViewModel()
        {
            //Inizializzo il comando
            SignInCommand = new RelayCommand(ExecuteSignIn, CanExecuteSignIn);

            //Se siamo a design time
            if (IsInDesignMode)
            {
                //Inizializza i valori di sample
                IsBusy = true;
                UserName = "mario.rossi";
                Password = "iwef hwieufh o3wf oqwiffj qowidj oq2idj oqi";
            }
            else //Se siamo a RUNTIME
            {
                //Non occupato di defalt
                IsBusy = false;

                //Aggancio l'evento di cambio delle proprietà
                PropertyChanged += OnPropertyChanged;
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine($"Cambiata proprietà {e.PropertyName}");
            SignInCommand.RaiseCanExecuteChanged();
        }

        private void ExecuteSignIn()
        {
            //Inizio a lavorare: non rompermi le palle!
            IsBusy = true;

            ApplicationServiceLayer layer = new ApplicationServiceLayer();
            var result = layer.SignIn(UserName, Password);

            //Mostro ok o fallimento
            if (result == null)
            {
                MessageBox.Show($"Credenziali errate!");
            }
            else
            {
                MessageBox.Show($"Autenticato con successo!");

                //Creo il messaggio per il Messenger
                Messenger.Default.Send(new CloseSignInViewMessage());

                //Chiudo la finestra corrente


                //Apro la finestra principale
            }

            //Ho finito: adesso puoi rompere...
            IsBusy = false;
        }

        private bool CanExecuteSignIn()
        {
            var condition =
                !string.IsNullOrEmpty(UserName) &&
                !string.IsNullOrEmpty(Password);
            Debug.WriteLine($"Condizione attivazione pulsante : {condition}");
            return condition;

        }
    }
}
