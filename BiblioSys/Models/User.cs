using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliosys.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public double AccountBalance { get; set; } = 0;

        public List<Reservation> ?Reservations { get; set; }

        public bool IsAdmin { get; set; } = false;


    }
}
