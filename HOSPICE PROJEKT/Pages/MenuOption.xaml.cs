using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Logika interakcji dla klasy MenuOption.xaml
    /// </summary>
    public partial class MenuOption : Page
    {
        public MenuOption()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                this.NavigationService.Navigate(new Uri("/Pages/Visitors_only.xaml", UriKind.Relative));
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Pages/LogginNurse.xaml", UriKind.Relative));
        }
    }
}
