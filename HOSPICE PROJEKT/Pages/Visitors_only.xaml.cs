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
    /// Logika interakcji dla klasy Visitors_only.xaml
    /// </summary>
    public partial class Visitors_only : Page
    {
        public Visitors_only()
        {
            InitializeComponent();

        }

        public List<VisitorsDatum> DatabaseVisitors { get; private set; }

        public void Create()
        {
            try
            {
                int patientIds = Int32.Parse(PatientIDTextBox.Text);
            }
            catch
            {
                MessageBox.Show("PatientID must be a valid integer value.");
                return;
            }

            try
            {
                int phonen = Int32.Parse(PhoneNrTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Phone number can't have characters.");
                return;
            }

            if (PhoneNrTextBox.Text.Length != 9)
            {
                MessageBox.Show("Phone number has to be 9 digits");
                return;
            }

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                int patientId = Convert.ToInt32(PatientIDTextBox.Text);
                var degofkinship = DegofkinshipTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;

                if (name != "" && surname != "" && degofkinship != "" && phonenumber != "")
                {
                    context.VisitorsData.Add(new VisitorsDatum() { Name = name, Surname = surname, DegOfKinship = degofkinship, PatientId = patientId, PhoneNumber = phonenumber });
                    context.SaveChanges();
                    Read();
                }
                else
                {
                    MessageBox.Show("Every information is needed.");
                    return;
                }

            }
        }

        public void Read()
        {
            var phonenumber = PhoneNrTextBox.Text;

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                DatabaseVisitors = context.VisitorsData.ToList();
                ItemList.ItemsSource = DatabaseVisitors.Where(x => x.PhoneNumber.Contains(phonenumber));
            }
        }


        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Clear();
        }

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
    }

}
