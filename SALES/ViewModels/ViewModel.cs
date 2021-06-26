using SALES.Commands;
using SALES.Models;
using SALES.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SALES.ViewModels
{
    class ViewModel : BaseViewModel
    {
        private bool isBusy = false;
        public ViewModel()
        {
            Init();
        }
        async void Init()
        {
            isBusy = true;
            Employees = new ObservableCollection<Employee>(await EmployeeDataStore.GetItemsAsync());
            Products = new ObservableCollection<Product>(await ProductDataStore.GetItemsAsync());
            Sales = new ObservableCollection<Sale>(await SaleDataStore.GetItemsAsync());
            OnPropertyChanged(nameof(Employees));
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(Sales));
            isBusy = false;
        }
        public DelegateCommand AddEmployee
        {
            get => new DelegateCommand(async (_) =>
            {
                var addItem = new AddEmployee(new Employee());
                if (addItem.ShowDialog() == true)
                {
                    Employee emp = addItem.Employee;
                    await EmployeeDataStore.AddItemAsync(emp);
                    Employees.Add(emp);
                }
            });
        }
        public DelegateCommand RemoveEmployee
        {
            get => new DelegateCommand(async (_) =>
            {
                await EmployeeDataStore.DeleteItemAsync(SelectedEmployee.Id);
                Employees.Remove(SelectedEmployee);
            }, (_) => SelectedEmployee != null);
        }

        public DelegateCommand AddProduct
        {
            get => new DelegateCommand(async (_) =>
            {
                var addItem = new AddProduct(new Product());
                if (addItem.ShowDialog() == true)
                {
                    Product prod = addItem.Product;
                    await ProductDataStore.AddItemAsync(prod);
                    Products.Add(prod);
                }
            });
        }
        public DelegateCommand RemoveProduct
        {
            get => new DelegateCommand(async (_) =>
            {
                await ProductDataStore.DeleteItemAsync(SelectedProduct.Id);
                Products.Remove(SelectedProduct);
            }, (_) => SelectedProduct != null);
        }
        public DelegateCommand AddSale
        {
            get => new DelegateCommand(async (_) =>
            {
                    var addItem = new AddSale(new Sale());
                    if (addItem.ShowDialog() == true)
                    {
                        Sale sale = addItem.Sale;
                        await SaleDataStore.AddItemAsync(sale);
                        Sales.Add(sale);
                    }
            });
        }
        public DelegateCommand RemoveSale
        {
            get => new DelegateCommand(async (_) =>
            {
                await SaleDataStore.DeleteItemAsync(SelectedSale.Id);
                Sales.Remove(SelectedSale);
                SelectedSale = Sales.FirstOrDefault();
            }, (_) => SelectedSale != null && isBusy == false);
        }

        public DelegateCommand ShowGraph
        {
            get => new DelegateCommand(async (_) =>
            {
                var chart = new Chart(SelectedEmployee);
                chart.Show();
            }, (_) => SelectedEmployee != null && isBusy == false);
        }

        public Employee SelectedEmployee { get; set; }
        public Product SelectedProduct { get; set; }
        public Sale SelectedSale { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Sale> Sales { get; set; }
    }
}
