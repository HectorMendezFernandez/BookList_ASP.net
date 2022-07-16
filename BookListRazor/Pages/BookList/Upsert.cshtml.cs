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
    //ESTE MODELO ES UNA COMBINACION DEL CREATE Y UPDATE
    public class UpsertModel : PageModel
    {
        //atributo para usar metodos de ingreso , update y delete en la base de datos
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Book Book { get; set; }
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }

        //este metodo verificacra que el id no sea nulo, si es nulo es porque se quiere crear un objeto, si no lo es
        //es porque se quiere actualizar un objeto
        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            //significa que se quiere crear un elemento
            //CREATE
            if(id == null)
            {
                //create
                return Page();
            }

            //UPDATE
            Book =await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        //metodo post UPSERT, se revisara que el estado sea valido, si no es valido retonramos de vuelta, y
        // si es valido
        public async Task<IActionResult> OnPost()
        {
            //validamos los datos
            if (ModelState.IsValid)
            {
                //si se esta creando un elemento (CREATE)
               if(Book.Id == 0)
                {
                    _db.Book.Add(Book);
                }
                else //Si se esta actualizando un elemento (UPDATE)
                {
                    _db.Book.Update(Book);
                }
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            //retorna nuevamente la vista pagina
            return RedirectToPage();

        }
    }
}

