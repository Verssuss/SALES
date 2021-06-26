using SALES.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SALES.ViewModels
{
    class ViewModel : BaseViewModel
    {
        public ViewModel()
        {
            Init();
        }
        async void Init()
        {
            Employees = new ObservableCollection<Employee>(await EmployeeDataStore.GetItemsAsync());
            Products = new ObservableCollection<Product>(await ProductDataStore.GetItemsAsync());
            Sales = new ObservableCollection<Sale>(await SaleDataStore.GetItemsAsync());
            OnPropertyChanged(nameof(Employees));
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(Sales));
        }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Sale> Sales { get; set; }
    }
}
