using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp
{
    internal class SingleReservation : GymReservation
    {
        public string ContactName { get; private set; }
        public int ReservationTime { get; private set; }
        public bool HasSeasonTicket { get; private set; }

        public SingleReservation(string contactName, int reservationTime)
        {
            ContactName = contactName;
            ReservationTime = reservationTime;
            HasSeasonTicket = false;
        }

        public void BuySeasonTicket()
        {
            HasSeasonTicket = true;
        }

        public int CalculatePriceToPay()
        {
            if (HasSeasonTicket)
            {
                return 2250;
            }
            else
            {
                return 4500;
            }
        }
    }
}
