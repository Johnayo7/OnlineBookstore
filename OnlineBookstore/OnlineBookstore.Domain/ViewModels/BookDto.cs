using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.ViewModels
{
    public class BookDto
    {
        [Required(ErrorMessage =("Title is required"))]
        public string Title { get; set; }

        [Required(ErrorMessage =("Genre is required"))]
        public string Genre { get; set; }

        [Required(ErrorMessage =("ISBN is required"))]
        public string ISBN { get; set; }

        [Required(ErrorMessage =("Author is required"))]
        public string Author { get; set; }

        [Required(ErrorMessage =("Publication year is required"))]
        public int PublicationYear { get; set; }
    }
}
