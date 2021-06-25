using SALES.Models;
using SALES.Patterns;
using SALES.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SALES.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IDataStore<Employee> EmployeeDataStore => IoC.Resolve<EmployeeDataStore>();
        public IDataStore<Product> ProductDataStore => IoC.Resolve<ProductDataStore>();
        public IDataStore<Sale> SaleDataStore => IoC.Resolve<SaleDateStore>();

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
