using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymApp
{
    internal class Gym
    {
        private List<GymReservation> reservations;

        public Gym()
        {
            reservations = new List<GymReservation>();
        }

        public bool AddReservation(GymReservation newReservation)
        {
            if (newReservation is TeamReservation)
            {
                foreach (GymReservation oldReservation in reservations)
                {
                    if (oldReservation is TeamReservation)
                    {
                        if (oldReservation.ReservationTime ==newReservation.ReservationTime)
                        {
                            return false;
                        }
                    }
                }
            }
            reservations.Add(newReservation);
            return true;
        }

        public List<GymReservation> GetReservations()
        {
            return reservations;
        }
    }
}
