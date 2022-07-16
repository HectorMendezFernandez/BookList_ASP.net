using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoolListRazor.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //todos los modelos que se quieran insertar en la base de datos deben de estar colocados de la siguiente manera

        public DbSet<Book> Book { get; set; }
    }
}
