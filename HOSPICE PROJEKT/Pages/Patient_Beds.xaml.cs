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

        public PatientBeds()
        {
            InitializeComponent();
            Read();
        }

        public void Create()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var bedId = int.Parse(BedIdTextBox.Text);
                var patientId = int.Parse(PatientIdTextBox.Text);
                var roomNr = int.Parse(RoomNrTextBox.Text);

                context.HospiceRooms.Add(new HospiceRoom() { BedId = (short)bedId, PatientId = patientId, RoomNr = (short)roomNr }); ;
                context.SaveChanges();
                Read();


            }
        }

        public void Read()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                HospiceRoomsList = context.HospiceRooms.ToList();
                ItemList.ItemsSource = HospiceRoomsList;
            }
        }

        public void Update()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {

                HospiceRoom selectedBed = ItemList.SelectedItem as HospiceRoom;

                var bedId = int.Parse(BedIdTextBox.Text);
                var patientId = int.Parse(PatientIdTextBox.Text);
                var roomNr = int.Parse(RoomNrTextBox.Text);

                    HospiceRoom? bed = context.HospiceRooms.Find(selectedBed.BedId);

                    bed.BedId = (short)bedId;
                    bed.PatientId = patientId;
                    bed.RoomNr = (short)roomNr;

                    context.SaveChanges();
                    Read();

                

            }

        }

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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var PatientID = Convert.ToInt32(PatientIDText.Text);

            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                HospiceRoomsList = context.HospiceRooms.ToList();
                ItemList.ItemsSource = HospiceRoomsList.Where(x => x.PatientId.Equals(PatientID));

            }
        }
    }
}
