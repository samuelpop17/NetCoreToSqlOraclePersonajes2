using Microsoft.AspNetCore.Mvc;
using NetCoreToSqlOraclePersonajes.Models;
using NetCoreToSqlOraclePersonajes.Repositories;

namespace NetCoreToSqlOraclePersonajes.Controllers
{
    public class PersonajesController : Controller
    {
        private IRepositoryPersonajes repo;

        public PersonajesController(IRepositoryPersonajes repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Personaje> list = this.repo.GetPersonajes();
            return View(list);
        }

        public IActionResult Create() { 
        
            return View();
        }

        [HttpPost]
        public IActionResult Create(Personaje personaje)
        {
            this.repo.InsertPersonajes(personaje.Id, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) { 
        
            Personaje personaje = this.repo.FindPersonaje(id);
            return View(personaje);
        
        }

        public IActionResult Update(int id) {
            Personaje per = this.repo.FindPersonaje(id);
            return View(per);
        }
        [HttpPost]
        public IActionResult Update(Personaje personaje)
        {
            this.repo.UpdatePersonaje(personaje.Id, personaje.Nombre, personaje.Imagen);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            this.repo.DeletePersonaje(id);
            return RedirectToAction("Index");
        }
    }
}
