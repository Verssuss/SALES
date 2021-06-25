using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SALES.Services
{
    public class NavigationService
    {
        public event Action<Page> OnPageChanged;

        public void Navigate(Page page)
        {
            OnPageChanged?.Invoke(page);
        }

    }
}
