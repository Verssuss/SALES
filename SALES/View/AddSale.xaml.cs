using SALES.Models;
using SALES.Repositories;
using SALES.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SALES.View
{
    /// <summary>
    /// Логика взаимодействия для AddSale.xaml
    /// </summary>
    public partial class AddSale : Window
    {
        
        public AddSale(Sale sale)
        {
            InitializeComponent();
            Init();
            Sale = sale;
            DataContext = this;
        }
        public int Sum { get; set; }
        public List<Employee> Employees { get; private set; } = new List<Employee>();
        public List<Product> Products { get; private set; } = new List<Product>();

        public Sale Sale { get; private set; }

        async void Init()
        {
            BaseViewModel vm = new BaseViewModel();
            var emp = await vm.EmployeeDataStore.GetItemsAsync();
            var prod = await vm.ProductDataStore.GetItemsAsync();
            foreach (var item in emp)
            {
                Employees.Add(item);
            }
            foreach (var item in prod)
            {
                Products.Add(item);
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sumBox.Text.Length > 9) return;
            if (new object[] { empBox.SelectedItem, prodBox.SelectedItem, sumBox.Text }.Any(x => x is null || string.IsNullOrEmpty(x.ToString()))) return;
            Sale = new Sale { Employee = empBox.SelectedItem as Employee, Product = prodBox.SelectedItem as Product, Sum = Convert.ToInt32(sumBox.Text), SaleDate = DateTime.Now };

            this.DialogResult = true;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) || e.Key == Key.Escape)
                e.Handled = false;
            else
            e.Handled = true;
        }

        private void prodBox_KeyUp(object sender, KeyEventArgs e)
        {
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(prodBox.ItemsSource);

            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(prodBox.Text)) return true;
                else
                {
                    if (((Product)o).Name.Contains(prodBox.Text)) return true;
                    else return false;
                }
            });

            itemsViewOriginal.Refresh();

            // if datasource is a DataView, then apply RowFilter as below and replace above logic with below one
            /* 
             DataView view = (DataView) Cmb.ItemsSource; 
             view.RowFilter = ("Name like '*" + Cmb.Text + "*'"); 
            */
        }
    }
}
