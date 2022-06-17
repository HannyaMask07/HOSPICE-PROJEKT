using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Controls.Primitives;
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
        /// <summary>
        /// When clicked it changes page to Visitors
        /// In if with login textbox and password information can be added
        /// catching when passowrd or login is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToString(PasswordTextBox);
            }
            catch
            {
                MessageBox.Show("Password or login incorect");
                return;
            }
            string password = Convert.ToString(PasswordTextBox);
            if (LoginTextBox.Text != "" && password != "" )
            {
                this.NavigationService.Navigate(new Uri("/Pages/Visitors.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Password or login incorect");
                return;
            }


        }


    }
}
