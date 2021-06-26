using LiveCharts;
using LiveCharts.Wpf;
using SALES.Models;
using System;
using System.Linq;
using System.Windows;

namespace SALES.View
{
    /// <summary>
    /// Логика взаимодействия для Chart.xaml
    /// </summary>
    public partial class Chart : Window
    {
        public Employee Employee { get; set; }
        public Chart(Employee emp)
        {
            InitializeComponent();
            Employee = emp;

            SeriesCollection = new SeriesCollection();
            foreach (var item in Employee.Sales.Where(x=> x.SaleDate.Date == DateTime.Now.Date).GroupBy(x=> x.Product))
            {
                SeriesCollection.Add(new ColumnSeries 
                {
                    Title = item.Key.Name,
                    Values = new ChartValues<int>() { item.Count() }
                });
            }

            Labels = new[] { Employee.Name };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
