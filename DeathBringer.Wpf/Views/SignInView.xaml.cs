using DeathBringer.Wpf.Messaging;
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
            Messenger.Default.Register<CloseSignInViewMessage>(
                this, OnCloseSignInViewMessageReceived);
        }

        private void OnCloseSignInViewMessageReceived(CloseSignInViewMessage message)
        {
            //Chiudo me stesso
            this.Close();
        }
    }
}
