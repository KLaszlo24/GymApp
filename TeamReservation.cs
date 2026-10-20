using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp
{
    internal class TeamReservation : GymReservation
    {
        public string ContactName { get; private set; } 
        public int ReservationTime { get; private set; }
        public List<string> TeamMembers { get; private set; }

        public TeamReservation(string contactName, List<string> teamMembers, int reservationTime)
        {
            ContactName = contactName;
            TeamMembers = teamMembers;
            ReservationTime = reservationTime;
        }

        public int CalculatePriceToPay()
        {
            int peopleCount = TeamMembers.Count + 1;
            int egyforejuto = 9000 / peopleCount;
            return egyforejuto;
        }
    }
}
