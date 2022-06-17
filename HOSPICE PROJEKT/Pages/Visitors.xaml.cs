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
    /// Logika interakcji dla klasy Visitors.xaml
    /// </summary>
    public partial class Visitors : Page
    {
        /// <summary>
        /// Read data from databse to show in datagrid
        /// </summary>
        public Visitors()
        {
            InitializeComponent();
            Read();
        }
        public List<VisitorsDatum> DatabaseVisitors { get; private set; }

            
        /// <summary>
        /// Class for create button, created visitor is based on what user put inside textboxes
        /// </summary>
        public void Create()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
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

                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var patientID = int.Parse(PatientIDTextBox.Text);
                var degofkinship = DegofkinshipTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;


                if (name != "" && surname != "" && degofkinship != "" && phonenumber != "")
                {
                    context.VisitorsData.Add(new VisitorsDatum() { Name = name, Surname = surname, DegOfKinship = degofkinship, PatientId = patientID, PhoneNumber = phonenumber });
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
        /// <summary>
        /// Class for Read button, Reads every user from database and shows in datagrid
        /// </summary>
        public void Read()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                DatabaseVisitors = context.VisitorsData.ToList();
                ItemList.ItemsSource = DatabaseVisitors;
            }
        }
        /// <summary>
        /// Class for Update button, Update users infromation based on what user put inside texboxes
        /// </summary>
        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {

                VisitorsDatum selectedVisitor = ItemList.SelectedItem as VisitorsDatum;

                try
                {
                    int patientIds = Int32.Parse(PatientIDTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("PatientID must be a valid integer value.");
                    return;
                }

                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                int patientId = Convert.ToInt32(PatientIDTextBox.Text);
                var degofkinship = DegofkinshipTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;


                if (name != "" && surname != "" && degofkinship != "" && phonenumber != "")
                {
                    VisitorsDatum? visitor = context.VisitorsData.Find(selectedVisitor.VisitId);

                    visitor.Name = name;
                    visitor.Surname = surname;
                    visitor.PatientId = patientId;
                    visitor.DegOfKinship = degofkinship;
                    visitor.PhoneNumber = phonenumber;
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
        /// <summary>
        /// Class for deletebutton, delets user from database based on datagrid click or puted information
        /// </summary>
        /// 
        public void Delete()
        {

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                VisitorsDatum selectedVisitor = (VisitorsDatum)ItemList.SelectedItem;

                if (selectedVisitor != null)
                {
                    VisitorsDatum? visitor = context.VisitorsData.Find(selectedVisitor.VisitId);


                    context.Remove(visitor);
                    context.SaveChanges();
                    Read();

                }


            }

        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            Read();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Clear();
        }

        /// <summary>
        /// I made navButton class to better navigate trough pages
        /// it can be easly modifide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }
        /// <summary>
        /// Sort list based on whats inside PatientIDTextbox
        /// </summary>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var PatientIDTEST = Convert.ToInt32(PatientIDText.Text);
            }
            catch
            {
                MessageBox.Show("PatientID must be a valid integer value.");
                return;
            }
            var PatientID = Convert.ToInt32(PatientIDText.Text);

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                try
                {
                    DatabaseVisitors = context.VisitorsData.ToList();
                    ItemList.ItemsSource = DatabaseVisitors.Where(x => x.PatientId.Equals(PatientID));
                }
                catch
                {
                    MessageBox.Show("error");
                    return;
                }

            }
        }
    }
}
