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


namespace HOSPICE_PROJEKT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();


            
        }

        public void Create()
        {
            using (HospiceDataBaseContext context = new HospiceDataBaseContext())
            {
                var name = NameTextBox.Text;
                var surname = SurnameTextBox.Text;
                var patientId = ;
                var degofkinship = ;
                var phonenumber = ;
                context.VisitorsData.Add(new VisitorsDatum() { Name = name, Surname = surname });
            }
        }

        public void Read()
        {

        }

        public void Update()
        {

        }

        public void Delete()
        {

        }


       
    }
}
