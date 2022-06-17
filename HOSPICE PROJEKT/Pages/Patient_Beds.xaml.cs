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
    /// Logika interakcji dla klasy PatientAdress.xaml
    /// </summary>
    public partial class PatientBeds : Page
    {
        public List<HospiceRoom> HospiceRoomsList { get; private set; }
        /// <summary>
        /// Using read class to show all the record in datagrid
        /// </summary>
        public PatientBeds()
        {
            InitializeComponent();
            Read();
        }


        /// <summary>
        /// Class for create button, created information about patients rooms is based on what user put inside textboxes
        /// </summary>
        public void Create()
        {
            try
            {
                int patientIds = Int32.Parse(BedIdTextBox.Text);
            }
            catch
            {
                MessageBox.Show("BedId must be a valid integer value.");
                return;
            }
            try
            {
                int patientIds = Int32.Parse(PatientIdTextBox.Text);
            }
            catch
            {
                MessageBox.Show("PatientID must be a valid integer value.");
                return;
            }
            try
            {
                int patientIds = Int32.Parse(RoomNrTextBox.Text);
            }
            catch
            {
                MessageBox.Show("RoomNr must be a valid integer value.");
                return;
            }


            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var bedId = int.Parse(BedIdTextBox.Text);
                var patientId = int.Parse(PatientIdTextBox.Text);
                var roomNr = int.Parse(RoomNrTextBox.Text);

                try
                {
                    context.HospiceRooms.Add(new HospiceRoom() { BedId = (short)bedId, PatientId = patientId, RoomNr = (short)roomNr }); ;
                    context.SaveChanges();
                    Read();
                }
                catch
                {
                    MessageBox.Show("Can't create (Probably patient is arleady signed to another bed)");
                    return;
                }


            }
        }

        /// <summary>
        /// Class for read button, shows data form database in datagrid
        /// </summary>
        public void Read()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                HospiceRoomsList = context.HospiceRooms.ToList();
                ItemList.ItemsSource = HospiceRoomsList;
            }
        }
        /// <summary>
        /// Class for updat button, updated information about patients rooms is based on what user put inside textboxes
        /// </summary>
        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                try
                {
                    int patientIds = Int32.Parse(BedIdTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("BedId must be a valid integer value.");
                    return;
                }
                try
                {
                    int patientIds = Int32.Parse(PatientIdTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("PatientID must be a valid integer value.");
                    return;
                }
                try
                {
                    int patientIds = Int32.Parse(RoomNrTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("RoomNr must be a valid integer value.");
                    return;
                }
                HospiceRoom selectedBed = ItemList.SelectedItem as HospiceRoom;

                var bedId = int.Parse(BedIdTextBox.Text);
                var patientId = int.Parse(PatientIdTextBox.Text);
                var roomNr = int.Parse(RoomNrTextBox.Text);

                    HospiceRoom? bed = context.HospiceRooms.Find(selectedBed.BedId);
                try
                {
                    bed.BedId = (short)bedId;
                    bed.PatientId = patientId;
                    bed.RoomNr = (short)roomNr;

                    context.SaveChanges();
                    Read();
                }
                catch
                {
                    MessageBox.Show("Can't create (Probably patient is arleady signed to another bed)");
                    return;
                }


            }

        }
        /// <summary>
        /// Class for delete button
        /// </summary>
        public void Delete()
        {

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                HospiceRoom selectedBed = (HospiceRoom)ItemList.SelectedItem;

                if (selectedBed != null)
                {
                    HospiceRoom? bed = context.HospiceRooms.Find(selectedBed.BedId);


                    context.Remove(bed);
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
        /// <summary>
        /// Sort list based on whats inside PatientIDTextbox
        /// </summary>
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
                try { 
                HospiceRoomsList = context.HospiceRooms.ToList();
                ItemList.ItemsSource = HospiceRoomsList.Where(x => x.PatientId.Equals(PatientID));
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
