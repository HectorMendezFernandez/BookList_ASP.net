using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoolListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoolListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        //atributo para usar metodos de ingreso , update y delete en la base de datos
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //este metodo ira a la base de datos y con el id que reciba por parametro busca el libro y lo retorna en el objeto book
        //_el objeto book es bindProperty entonces tomara esa propiedad como parte de la vista
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }

        //metodo post para actualizar (update) el objeto
        public async Task<IActionResult> OnPost()
        {
            //validamos los datos
            if (ModelState.IsValid)
            {
                //traemos el libro de la base de datos y lo almacenamos en una variable para actualizar sus datos
                var BookFromDb = await _db.Book.FindAsync(Book.Id);
                //actualizamos los datos (esto es posible porque con el bindProperty 
                BookFromDb.Name = Book.Name;
                BookFromDb.Autor = Book.Autor;
                BookFromDb.ISBN = Book.ISBN;
                //se actualiza la base de datos
                await _db.SaveChangesAsync();
                //una vez los cambios se hallan realizado a la base de datos, redirigo a la pagina inicial Index
                return RedirectToPage("Index");
            }
            else
            {
                //retorna nuevamente la vista pagina
                return Page();
            }
        }
    }
}
