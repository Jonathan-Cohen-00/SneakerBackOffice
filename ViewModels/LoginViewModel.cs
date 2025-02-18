using SneakerBackOffice.Commands;
using SneakerBackOffice.Services;
using SneakerBackOffice.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;  // ✅ Ajouté pour `PasswordBox`
using System.Windows.Input;

namespace SneakerBackOffice.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        public string Email { get; set; }
        public string ErrorMessage { get; set; }
        public ICommand LoginCommand { get; }

        public LoginViewModel() { 
        
        }
        public LoginViewModel(AuthService authService) // ✅ Seul AuthService est injecté
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private void ExecuteLogin(object parameter)
        {
            if (parameter is PasswordBox passwordBox && _authService.ValidateUser(Email, passwordBox.Password))
            {
                // ✅ Vérifie que `DashboardWindow` est bien importé
                var dashboardWindow = new DashboardWindow();
                dashboardWindow.Show();

                // ✅ Ferme la fenêtre actuelle (LoginWindow)
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                ErrorMessage = "Invalid email or password";
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
