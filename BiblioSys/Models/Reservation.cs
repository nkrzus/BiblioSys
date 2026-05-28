using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliosys.Model
{
    public enum ReservationStatus
    {
        Aktywna,
        Zakonczona
    }

    public class Reservation
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime ReservationDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public double AdditionalFee { get; set; }

        public ReservationStatus Status { get; set; }
    }
}