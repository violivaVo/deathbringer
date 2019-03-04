using DeathBringer.Wpf.Messaging;
using DeathBringer.Wpf.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace DeathBringer.Wpf.Views
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        public SignInView()
        {
            //Inizializzazione
            InitializeComponent();

            //Mi metto in ascolto sul Messenger
            Messenger.Default.Register<ShowMainViewMessage>(
                this, OnShowMainViewReceived);
        }

        private void OnShowMainViewReceived(ShowMainViewMessage message)
        {
            ////Creo il viewmodel della main
            //MainViewModel vm = new MainViewModel();
            //MainView view = new MainView();
            //view.DataContext = vm;
            //view.Show();

            //Chiudo me stesso
            Close();
        }
    }
}
