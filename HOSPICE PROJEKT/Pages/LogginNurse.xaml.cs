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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HOSPICE_PROJEKT.Pages
{
    /// <summary>
    /// Logika interakcji dla klasy LogginNurse.xaml
    /// </summary>
    public partial class LogginNurse : Page
    {
        public LogginNurse()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/Visitors.xaml", UriKind.Relative));
        }
    }
}
