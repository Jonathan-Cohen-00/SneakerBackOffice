using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SneakerBackOffice.Data;
using SneakerBackOffice.Services;
using SneakerBackOffice.ViewModels;
using SneakerBackOffice.Views;

namespace SneakerBackOffice
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Enregistrement du DbContext avec DI
                    services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql("server=localhost;database=sneaker_db;user=root;password=",
                            new MySqlServerVersion(new Version(8, 0, 23))));

                    // Services et ViewModels
                    services.AddSingleton<AuthService>();
                    services.AddSingleton<DatabaseService>();
                    services.AddTransient<LoginViewModel>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = _host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // 🔥 TEST DE CONNEXION À LA BDD
                bool isDbConnected = dbContext.TestDatabaseConnection();
                if (!isDbConnected)
                {
                    MessageBox.Show("⚠️ Erreur : Impossible de se connecter à la base de données !",
                                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Shutdown(); // Ferme l'application si la BDD ne fonctionne pas
                    return;
                }

                // ✅ Lancer LoginWindow si tout va bien
                var loginVM = scope.ServiceProvider.GetRequiredService<LoginViewModel>();
                var loginWindow = new LoginWindow
                {
                    DataContext = loginVM
                };
                loginWindow.Show();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
