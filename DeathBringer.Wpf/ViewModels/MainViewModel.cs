using DeathBringer.Core.ServiceLayers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace DeathBringer.Wpf.ViewModels
{
    public class MainViewModel: ViewModelBase
    {
        private string _UserName;
        private string _Password;

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

        public MainViewModel()
        {
            //Inizializzo il comando
            SignInCommand = new RelayCommand(ExecuteSignIn, CanExecuteSignIn);

            //Se siamo a design time
            if (IsInDesignMode)
            {
                //Inizializza i valori di sample
                UserName = "mario.rossi";
                Password = "iwef hwieufh o3wf oqwiffj qowidj oq2idj oqi";
            }
            else //Se siamo a RUNTIME
            {
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
            }


            
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
