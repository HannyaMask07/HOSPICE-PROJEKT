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

namespace HOSPICE_PROJEKT
{
    /// <summary>
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    /// 
    

    public partial class Page1 : Page
    {
        public List<PatientsPersonalDatum> PatientsPersonalData { get; private set; }

        public Page1()
        {
            InitializeComponent();
            Read();

        }

       

        public void Create()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                try
                {
                    Int32.Parse(PhoneNrTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("PhoneNr can't contain letters.");
                    return;
                }

                if (PhoneNrTextBox.Text.Length != 0)
                {
                    if (PhoneNrTextBox.Text.Length != 9)
                    {
                        MessageBox.Show("PhoneNr mus have 9 digits.");
                        return;
                    }

                }

                if (PeselTextBox.Text.Length != 0)
                {
                    if (PeselTextBox.Text.Length != 11)
                    {
                        MessageBox.Show("Pesel must have 11 digits.");
                        return;
                    }
                }


                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var pesel = PeselTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;

                if (name != "" && surname != "")
                {
                    context.PatientsPersonalData.Add(new PatientsPersonalDatum() { Name = name, Surname = surname, PhoneNumber = phonenumber, Pesel = pesel});
                    context.SaveChanges();
                    Read();
                }
                else
                {
                    MessageBox.Show("Please check if all needed informations are provided");
                    return;
                }
            }
        }

        public void Read()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                PatientsPersonalData = context.PatientsPersonalData.ToList();
                ItemList.ItemsSource = PatientsPersonalData;
            }
        }

        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {

                PatientsPersonalDatum selectedPatient = ItemList.SelectedItem as PatientsPersonalDatum;

                try
                {
                    Int32.Parse(PhoneNrTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("PhoneNr can't contain letters.");
                    return;
                }

                if (PhoneNrTextBox.Text.Length != 0)
                {
                    if (PhoneNrTextBox.Text.Length != 9)
                    {
                        MessageBox.Show("PhoneNr mus have 9 digits.");
                        return;
                    }

                }

                if (PeselTextBox.Text.Length != 0)
                {
                    if (PeselTextBox.Text.Length != 11)
                    {
                        MessageBox.Show("Pesel must have 11 digits.");
                        return;
                    }
                }


                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var pesel = PeselTextBox.Text;
                var phonenumber = PhoneNrTextBox.Text;

                if (name != "" && surname != "")
                {
                    PatientsPersonalDatum? patient = context.PatientsPersonalData.Find(selectedPatient.PatientId);

                    patient.Name = name;
                    patient.Surname = surname;
                    patient.PhoneNumber = phonenumber;
                    patient.Pesel = pesel;
                    context.SaveChanges();
                    Read();

                }
                else
                {
                    MessageBox.Show("Please check if all needed informations are provided");
                    return;
                }

            }

        }

        public void Delete()
        {

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                PatientsPersonalDatum selectedPatient = (PatientsPersonalDatum)ItemList.SelectedItem;

                if (selectedPatient != null)
                {
                    PatientsPersonalDatum? patient = context.PatientsPersonalData.Find(selectedPatient.PatientId);


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
                Convert.ToInt32(PatientIDText.Text);
            }
            catch
            {
                MessageBox.Show("PatientID must be a valid integer value.");
                return;
            }
            var PatientID = Convert.ToInt32(PatientIDText.Text);

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                PatientsPersonalData = context.PatientsPersonalData.ToList();
                ItemList.ItemsSource = PatientsPersonalData.Where(x => x.PatientId.Equals(PatientID));

            }
        }
    }
}
   
