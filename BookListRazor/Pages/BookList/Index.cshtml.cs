using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoolListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BoolListRazor.Pages.BookList
{
    //La vista de este modelo ya tiene la extraccion de este modelo en su vista
    public class IndexModel : PageModel
    {
        //Se instancia un objeto ApplicationDataBase para poder hacer CRUD en la base de datos
        private readonly ApplicationDbContext _db;

        //creamos una colleccion de tipo IEnumerated para libros
        public IEnumerable<Book> Books { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        //este metodo recibira todos los libros de la base de datos (sera asincronico para que se puedan
        //..realizar multiples tareas mientras se complete la accion
        public async Task OnGet()
        {
            //se asignan todos los libros de la base de datos a la coleccion books
            //Lo que se hace a continuacion es ir a la base de datos y recuperar todos los libros -
            //_almacenandolos en el IEnumarable Books.
            //El asinc permite ejecutar multiples tareas
            Books = await _db.Book.ToListAsync(); 
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            //busca el libro que tenga el id recibido por parametro que en la vista se envia por medio del
            //_  asp-route-id='id'
            var book = await _db.Book.FindAsync(id);
            if(book == null)
            {
                //si no encuentra ningun libro con ese id, despliega mensaje de not found
                return NotFound();
            }
            //si lo encuentra remueve ese libro de la tabla
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
