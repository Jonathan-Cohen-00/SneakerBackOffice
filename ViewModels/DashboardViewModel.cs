using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Input;
using SneakerBackOffice.Models;
using SneakerBackOffice.Services;
using SneakerBackOffice.Commands;   
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
        }

   

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
