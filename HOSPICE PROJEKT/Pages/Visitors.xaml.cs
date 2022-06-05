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
        public Visitors()
        {
            InitializeComponent();
            Read();
        }
        public List<VisitorsDatum> DatabaseVisitors { get; private set; }

        public void Create()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                int patientId = Convert.ToInt32(PatientIDTextBox.Text);
                var degofkinship = DegofkinshipTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;

                if (name != null && surname != null && patientId != null && degofkinship != null && phonenumber != null)
                {
                    context.VisitorsData.Add(new VisitorsDatum() { Name = name, Surname = surname, DegOfKinship = degofkinship, PatientId = patientId, PhoneNumber = phonenumber });
                    context.SaveChanges();
                    Read();
                }

            }
        }

        public void Read()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                DatabaseVisitors = context.VisitorsData.ToList();
                ItemList.ItemsSource = DatabaseVisitors;
            }
        }

        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {

                VisitorsDatum selectedVisitor = ItemList.SelectedItem as VisitorsDatum;

                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                int patientId = Convert.ToInt32(PatientIDTextBox.Text);
                var degofkinship = DegofkinshipTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;

                if (name != null && surname != null && patientId != null && degofkinship != null && phonenumber != null)
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

            }

        }

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

        private void NavButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
