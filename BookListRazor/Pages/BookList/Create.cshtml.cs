using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoolListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoolListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        //atributo para usar metodos de ingreso , update y delete en la base de datos
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }


        //Al crear la variable Libro, tendremos la posibilidad de agregar textboxes , labels, etc
        //El data annotation BindProerty, esto lo que hara es asumir que en el post, se obtendra este
        //_objeto Book 
        [BindProperty]
        public Book Book { get; set; }
        public void OnGet()
        {
        }

        //Este metodo sera de tipo Task<IActionResult> por el hecho de que nos rediirigira a una nueva pagina
        //Sera de tipo post, para poder crear un libro en la vista y guardarlo en la base de datos
        //El objeto que creara es un Libro entonces se puede insertar por parametro un libro o se puede colocar
        //_la annotation BindProperty en alguna propiedad que tengamos de tipo Libro(esto es lo que tenemos en este caso {linea 24,25})
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid) //valida que todo este bien ingresado
            {
                //si todo esta bien ingresado, ingresa el libro en la base de datos (en este caso en una cola que esta en espera del push)
                await _db.Book.AddAsync(Book);
                //despues de agregar el libro necesitamos hacer update a la base de datos
                await _db.SaveChangesAsync();
                //una vez los cambios se hallan subido, redirigo a la pagina inicial Index
                return RedirectToPage("Index");
            }
            else //de lo contrario retornara de nuevo la pagina 
            {
                return Page();
            }
        }
    }
}
