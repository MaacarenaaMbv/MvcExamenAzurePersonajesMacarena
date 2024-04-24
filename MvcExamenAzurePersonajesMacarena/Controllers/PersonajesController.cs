using Microsoft.AspNetCore.Mvc;
using MvcExamenAzurePersonajesMacarena.Models;
using MvcExamenAzurePersonajesMacarena.Services;

namespace MvcExamenAzurePersonajesMacarena.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public async Task<IActionResult> Delete(int idpersonaje)
        {
            await this.service.DeletePersonajeAsync(idpersonaje);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen, personaje.Serie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int idpersonaje)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(idpersonaje);
            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Personaje pers)
        {
            await this.service.UpdatePersonajeAsync(pers.IdPersonaje, pers.Nombre, pers.Imagen, pers.Serie);
            return RedirectToAction("Index");
        }

    }
}
