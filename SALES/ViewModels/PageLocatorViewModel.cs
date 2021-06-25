using SALES.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SALES.ViewModels
{
    class PageLocatorViewModel : BaseViewModel
    {
        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                Set(ref _currentPage, value);
            }
        }

        public PageLocatorViewModel(NavigationService navigation)
        {
            navigation.OnPageChanged += (page) => CurrentPage = page;
            //navigation.Navigate(new LoadingPage());
        }
    }
}
