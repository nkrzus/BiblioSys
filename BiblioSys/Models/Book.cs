using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioSys.Enums;


namespace Bibliosys.Model
{
    public class Book
    {
    
        public int Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public string Description { get; set; }

        public bool IsFree { get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}
