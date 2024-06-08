using OnlineBookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstore.Domain.ViewModels
{
    public class CartItemDto
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } 
    }
}
