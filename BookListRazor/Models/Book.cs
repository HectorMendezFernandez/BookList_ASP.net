using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoolListRazor.Models
{
    public class Book
    {
        //indicamos que es un primary key en este modelo (tabla)
        [Key]
        public int Id { get; set; }

        //indicamos que no puede ser null
        [Required] 
        public string Name { get; set; }

        public string Autor { get; set; }

        public string ISBN { get; set; }
    }
}
