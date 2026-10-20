using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GymApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Gym gym;
        public MainWindow()
        {
            InitializeComponent();

            gym = new Gym();
            UpdateForm();
        }

        private bool IsTeamReservation()
        {
            ComboBoxItem selectedItem = TypeComboBox.SelectedItem as ComboBoxItem; ;
            if (selectedItem != null && selectedItem.Content.ToString() == "csoportos")
            {
                return true;
            }

            return false;
        }
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateForm();
        }

        private void UpdateForm()
        {
            if (TeamMembersTextBox == null || SeasonTicketCheckBox == null)
            {
                return;
            }
            if (IsTeamReservation())
            {
                TeamMembersTextBox.IsEnabled = true;
                SeasonTicketCheckBox.IsEnabled = false;
            }
            else
            {
                TeamMembersTextBox.IsEnabled = false;
                SeasonTicketCheckBox.IsEnabled = true;
            }
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            string nev = ContactNameTextBox.Text;

            int reservationTime;

            if (!int.TryParse(TimeTextBox.Text, out reservationTime))//ezt feldobta
            {
                MessageBox.Show("A foglalás idejének számnak kell lennie!");
                return;
            }

            GymReservation reservation;

            if (IsTeamReservation())
            {
                List<string> resztvevok = new List<string>();

                string[] nevek =TeamMembersTextBox.Text.Split(',');

                foreach (string memberName in nevek)
                {
                    string cleanedName = memberName.Trim();

                    if (cleanedName != "")
                    {
                        resztvevok.Add(cleanedName);
                    }
                }

                reservation = new TeamReservation(nev,resztvevok,reservationTime);
            }
            else
            {
                SingleReservation single =new SingleReservation(nev, reservationTime);

                if (SeasonTicketCheckBox.IsChecked == true)
                {
                    single.BuySeasonTicket();
                }

                reservation = single;
            }

            bool success = gym.AddReservation(reservation);

            if (success == false)
            {
                StatusTextBlock.Text ="Erre az időpontra van már csoportos foglalás.";
                return;
            }

            StatusTextBlock.Text =
                "Foglalás sikeres. Fizetendő: " +reservation.CalculatePriceToPay() +" Ft";

            ShowReservations();
        }

        private void ShowReservations()
        {
            ReservationsListBox.Items.Clear();

            foreach (GymReservation reservation in gym.GetReservations())
            {
                string reservationType;

                if (reservation is TeamReservation)
                {
                    reservationType = "Csoportos";
                }
                else
                {
                    reservationType = "Egyéni";
                }

                string reservationText =
                    reservation.ReservationTime +":00 – " +
                    reservationType +" – " +
                    reservation.ContactName +" – " +
                    reservation.CalculatePriceToPay() +" Ft/fő";

                ReservationsListBox.Items.Add(reservationText);
            }
        }
    }
}
