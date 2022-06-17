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
    /// Logika interakcji dla klasy DeathDate.xaml
    /// </summary>
    public partial class DeathDate : Page
    {
        public DeathDate()
        {
            InitializeComponent();
            Read();
        }

        public List<PatientsDeathDate> DatabaseDeathDate { get; private set; }
        public void Create()
        {
            try
            {
                Int32.Parse(PatientIdTextBox.Text);
            }
            catch
            {
                MessageBox.Show("PatientID must be a valid integer value.");
                return;
            }

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var patientID = int.Parse(PatientIdTextBox.Text);
                var deathDate = DeathDateTextBox.SelectedDate;
                var causeOfDeath = CauseOfDeathTextBox.Text; 
                

                if (deathDate != null && causeOfDeath != "")
                {
                    context.PatientsDeathDates.Add(new PatientsDeathDate() { PatientId = patientID, DeathDate = (DateTime)deathDate, CauseOfDeath = causeOfDeath  }); ;
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
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                DatabaseDeathDate = context.PatientsDeathDates.ToList();
                ItemList.ItemsSource = DatabaseDeathDate;
            }
        }

        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {

                try
                {
                    Int32.Parse(PatientIdTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("PatientID must be a valid integer value.");
                    return;
                }

                PatientsDeathDate selectedPatient = ItemList.SelectedItem as PatientsDeathDate;

                var patientID = int.Parse(PatientIdTextBox.Text);
                var deathDate = DeathDateTextBox.SelectedDate;
                var causeOfDeath = CauseOfDeathTextBox.Text;

                if (deathDate != null && causeOfDeath != "")
                {
                    PatientsDeathDate? patient = context.PatientsDeathDates.Find(selectedPatient.PatientId);

                    patient.PatientId = patientID;
                    patient.DeathDate = (DateTime)deathDate;
                    patient.CauseOfDeath = causeOfDeath;
                    
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

        public void Delete()
        {

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                PatientsDeathDate selectedPatient = (PatientsDeathDate)ItemList.SelectedItem;

                if (selectedPatient != null)
                {
                    PatientsDeathDate? patient = context.PatientsDeathDates.Find(selectedPatient.PatientId);


                    context.Remove(patient);
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

        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

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

                try
                {
                    DatabaseDeathDate = context.PatientsDeathDates.ToList();
                    ItemList.ItemsSource = DatabaseDeathDate.Where(x => x.PatientId.Equals(PatientID));

                }
                catch
                {
                    MessageBox.Show("Error.");
                    return;
                }


        }
        

    }
}
