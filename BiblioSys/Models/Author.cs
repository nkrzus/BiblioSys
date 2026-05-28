using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliosys.Model
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Imię autora jest wymagane")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko autora jest wymagane")]
        [StringLength(100)]
        public string LastName { get; set; }

        public List<Book>? Books { get; set; }

    }
}

