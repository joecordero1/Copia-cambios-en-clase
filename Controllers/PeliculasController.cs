using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final_Cordero_Raura.Data;
using Final_Cordero_Raura.Models;
using Microsoft.AspNetCore.Authorization;

namespace Final_Cordero_Raura.Controllers
{
    [Authorize]
    public class PeliculasController : Controller
    {
        private readonly Final_Cordero_RauraContext _context;

        public PeliculasController(Final_Cordero_RauraContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(string buscar, string filtro)
        {
            var peliculas = from pelicula in _context.Pelicula select pelicula;
            if (!String.IsNullOrEmpty(buscar))
            {
                peliculas = peliculas.Where(s => s.Nombre!.Contains(buscar) || s.Genero!.Contains(buscar));
            }

            ViewData["FiltroNombre"] = String.IsNullOrEmpty(filtro) ? "NombreDescendente" : "";
            ViewData["FiltroAnio"] = filtro == "" ? "FechaDescendente" : "";
            switch (filtro)
            {
                case "NombreDescendente":
                    peliculas = peliculas.OrderByDescending(resenas => resenas.Nombre);
                    break;
                case "FechaDescendente":
                    peliculas = peliculas.OrderByDescending(resenas => resenas.anio);
                    break;
                default:
                    peliculas = peliculas.OrderBy(resenas => resenas.Nombre);
                    break;

            }

            return View(await peliculas.ToListAsync());
        }


        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                .FirstOrDefaultAsync(m => m.IdPelicula == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            //Aqui modifico y meto la siguiente linea
            ViewBag.Resenas = _context.Resena.Select(p => new SelectListItem { Value = p.IdPelicula.ToString(), Text = p.Titulo }).ToList();
            ///////////////
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPelicula,Nombre,Descripcion,Genero,anio,Poster")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                //Estas dos lineas de codigo arreglaron el problema con el html
                string htmlContent = Request.Form["Descripcion"];
                pelicula.Descripcion = htmlContent;
                ////////////////////////////
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //y esta tambien
            //ViewData["IdPelicula"] = new SelectList(_context.Pelicula, "IdPelicula", "Descripcion", pelicula.IdPelicula);

            //////////////////
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            //Aqui modifico y meto la siguiente linea
            ViewBag.Resenas = _context.Resena.Select(p => new SelectListItem { Value = p.IdPelicula.ToString(), Text = p.Titulo }).ToList();
            ///////////////
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPelicula,Nombre,Descripcion,Genero,anio,Poster")] Pelicula pelicula)
        {
            if (id != pelicula.IdPelicula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //las siguientes dos lineas hacen que sirva guardar el contenido del html
                    var texto = Request.Form["Descripcion"].ToString();
                    pelicula.Descripcion = texto;
                    //////////////////////
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.IdPelicula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            //NO SE QUE HICE AQUI, MODIFICAR POR SI ACASO
            //esta liena tambien 
            ViewData["Descripcion"] = new SelectList(_context.Pelicula, "Genero", "Descripcion", pelicula.Descripcion);
            //////////////
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pelicula == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Pelicula
                //Aqui modifique segun openIA//modificado como tres veces, puede que este mal
                .Include(r => r.Resenas)
                ////////////////////////////////
                .FirstOrDefaultAsync(m => m.IdPelicula == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pelicula == null)
            {
                return Problem("Entity set 'Final_Cordero_RauraContext.Pelicula'  is null.");
            }
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula != null)
            {
                _context.Pelicula.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Pelicula?.Any(e => e.IdPelicula == id)).GetValueOrDefault();
        }
    }
}
