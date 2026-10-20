using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp
{
    internal interface GymReservation
    {
        string ContactName { get; }
        int ReservationTime { get; }
        int CalculatePriceToPay();
    }
}
