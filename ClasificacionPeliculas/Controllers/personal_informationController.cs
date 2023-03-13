using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClasificacionPeliculas.Models;
using ClasificacionPeliculasModel;
using System.Drawing;

namespace ClasificacionPeliculas.Controllers
{
    public class personal_informationController : Controller
    {
        private readonly MoviesContext _context;

        public personal_informationController()
        {
            _context = new MoviesContext();
        }

        // GET: personal_information
        public async Task<IActionResult> Index()
        {



            return _context.personal_information != null ?
                          View(await _context.personal_information.ToListAsync()) :
                          Problem("Entity set 'MoviesContext.personal_information'  is null.");
        }

        // GET: personal_information/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.personal_information == null)
            {
                return NotFound();
            }

            var personal_information = await _context.personal_information
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personal_information == null)
            {
                return NotFound();
            }
            var city = await _context.city.FindAsync(personal_information.geonameidCity);
            ViewBag.cityName = city.name;
            return View(personal_information);
        }

        // GET: personal_information/Create
        public IActionResult Create()
        {
            List<region> regions = (from rg in _context.region
                                    select new region
                                    {
                                        geonameid = rg.geonameid,
                                        name = rg.name
                                    }).ToList();

            List<SelectListItem> items = regions.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.name,
                    Value = d.geonameid.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;
            return View();
        }

        [HttpGet]
        public JsonResult citys(int regi)
        {
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
            lst = (from ct in _context.city
                                  where ct.geonameidRegion == regi
                                  select new ElementJsonIntKey
                                  {
                                        Value = ct.geonameid,
                                        Text = ct.name
                                    }).ToList();
            return Json(lst);
        }

        public class ElementJsonIntKey
        {
            public int Value { get; set; }
            public string Text { get; set; }
        }
    // POST: personal_information/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,geonameidCity,name,date_of_birth,email,phone_number,address")] personal_information personal_information)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(personal_information);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personal_information);
        }

        // GET: personal_information/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            

            if (id == null || _context.personal_information == null)
            {
                return NotFound();
            }

            var personal_information = await _context.personal_information.FindAsync(id);
            if (personal_information == null)
            {
                return NotFound();
            }

            var city = await _context.city.FindAsync(personal_information.geonameidCity);
            if (city == null) { return NotFound(); }

            List<region> regions = (from rg in _context.region
                                    select new region
                                    {
                                        geonameid = rg.geonameid,
                                        name = rg.name
                                    }).ToList();

            List<SelectListItem> items = regions.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.name,
                    Value = d.geonameid.ToString(),
                    Selected = (d.geonameid == city.geonameidRegion) ? true : false
                };
            });

            ViewBag.items = items;


            return View(personal_information);
        }

        // POST: personal_information/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,geonameidCity,name,date_of_birth,email,phone_number,address")] personal_information personal_information)
        {
            if (id != personal_information.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personal_information);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!personal_informationExists(personal_information.Id))
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
            return View(personal_information);
        }

        // GET: personal_information/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.personal_information == null)
            {
                return NotFound();
            }

            var personal_information = await _context.personal_information
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personal_information == null)
            {
                return NotFound();
            }

            return View(personal_information);
        }

        // POST: personal_information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.personal_information == null)
            {
                return Problem("Entity set 'MoviesContext.personal_information'  is null.");
            }
            var personal_information = await _context.personal_information.FindAsync(id);
            if (personal_information != null)
            {
                _context.personal_information.Remove(personal_information);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool personal_informationExists(int id)
        {
          return (_context.personal_information?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        
    }
}
