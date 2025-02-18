using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using SneakerBackOffice.Models;
using SneakerBackOffice.Services;

namespace SneakerBackOffice.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ICommand RefreshCommand { get; }

        public DashboardViewModel()
        {
            Products = new ObservableCollection<Sneaker>();
            RefreshCommand = new RelayCommand(_ => LoadSneakers());
            LoadSneakers();
        }

        private void LoadSneakers()
        {
            Products.Clear();
            DataTable dataTable = DatabaseService.GetSneakers();

            foreach (DataRow row in dataTable.Rows)
            {
                Products.Add(new Sneaker
                {
                    Id = (int)row["id"],
                    Name = row["name"].ToString(),
                    Brand = row["brand"].ToString(),
                    Price = (double)row["price"]
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
