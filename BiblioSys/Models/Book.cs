using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiblioSys.Enums;


namespace Bibliosys.Model
{
    public class Book
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Kategoria jest wymagana")]
        public Category Category { get; set; }

        [Required(ErrorMessage = "Autor jest wymagany")]
        public int AuthorId { get; set; }

        public Author? Author { get; set; }

        [Required(ErrorMessage = "Opis jest wymagany")]
        [StringLength(2000)]
        public string? Description { get; set; }

        public bool IsFree { get; set; }

        public List<Reservation>? Reservations { get; set; }

    }
}
