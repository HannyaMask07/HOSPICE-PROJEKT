﻿using System;
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
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var patientID = int.Parse(PatientIdTextBox.Text);
                var deathDate = DeathDateTextBox.SelectedDate;
                var causeOfDeath = CauseOfDeathTextBox.Text; 
                

                if (deathDate != null && causeOfDeath != null)
                {
                    context.PatientsDeathDates.Add(new PatientsDeathDate() { PatientId = patientID, DeathDate = (DateTime)deathDate, CauseOfDeath = causeOfDeath  }); ;
                    context.SaveChanges();
                    Read();
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

                PatientsDeathDate selectedPatient = ItemList.SelectedItem as PatientsDeathDate;

                var patientID = int.Parse(PatientIdTextBox.Text);
                var deathDate = DeathDateTextBox.SelectedDate;
                var causeOfDeath = CauseOfDeathTextBox.Text;

                if (deathDate != null && causeOfDeath != null)
                {
                    PatientsDeathDate? patient = context.PatientsDeathDates.Find(selectedPatient.PatientId);

                    patient.PatientId = patientID;
                    patient.DeathDate = (DateTime)deathDate;
                    patient.CauseOfDeath = causeOfDeath;
                    
                    context.SaveChanges();
                    Read();

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
    }
}
