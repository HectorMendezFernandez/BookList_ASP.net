using BoolListRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//CLASE API QUE ME BRINDARA UNOS SERVICIOS
namespace BoolListRazor.Controllers
{
    //colocamos la ruta para acceder al API y definimos que es un controlador API
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // retorna un json con todos los registros de la base
             return Json(new { data =await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task <IActionResult> Delete(int id)
        {
            var bookFromDb =await _db.Book.FirstOrDefaultAsync(u => u.Id == id);
            if(bookFromDb == null)
            {
                //si el elemento no se logra eliminar, retorna el siguiente objeto json donde declara el error y el mensaje
                return Json(new { success = false, message = "Error while deleting" });
            }
            //se remueve el libro de la base de datos
            _db.Book.Remove(bookFromDb);
            //se realizan los cambios en la base de datos(se elimina oficialmente)
            await _db.SaveChangesAsync();
            //si el elemento se logra eliminar ,se retorna un objeto json donde declara el error y el mennsaje
            return Json(new { success = true, message = "Delete succesfull" });
        }
    }
}
