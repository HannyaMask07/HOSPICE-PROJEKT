﻿using System;
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