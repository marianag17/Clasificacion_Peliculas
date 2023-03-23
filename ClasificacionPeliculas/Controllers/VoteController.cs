using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClasificacionPeliculas.Models;

namespace ClasificacionPeliculas.Controllers
{
    public class VoteController : Controller
    {
        private readonly MoviesContext _context;

        public VoteController(MoviesContext context)
        {
            _context = context;
        }

        // GET: Votes
        public IActionResult Index()
        {
            MoviesContext _moviesContext = new MoviesContext();
            IEnumerable<ClasificacionPeliculas.Models.Vote> votes = (IEnumerable<Vote>)(from c in _moviesContext.Votes
                                                                                //join mc in _moviesContext.Moviescategories on c.Id equals mc.Category_Id
                                                                                //where c.Id == 0
                                                                            select new ClasificacionPeliculasModel.Category
                                                                            {
                                                                                Id = c.Id,
                                                                                Name = c.Pi.name
                                                                            }).OrderBy(s => s.Name).ToList();
            return View(votes);
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Votes == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Movies)
                .Include(v => v.Pi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            ViewData["Movies_Id"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["Pi_Id"] = new SelectList(_context.Set<personal_information>(), "Id", "Id");
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pi_Id,Movies_Id,RowCreationTime")] Vote vote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Movies_Id"] = new SelectList(_context.Movies, "Id", "Id", vote.Movies_Id);
            ViewData["Pi_Id"] = new SelectList(_context.Set<personal_information>(), "Id", "Id", vote.Pi_Id);
            return View(vote);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Votes == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }
            ViewData["Movies_Id"] = new SelectList(_context.Movies, "Id", "Id", vote.Movies_Id);
            ViewData["Pi_Id"] = new SelectList(_context.Set<personal_information>(), "Id", "Id", vote.Pi_Id);
            return View(vote);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pi_Id,Movies_Id,RowCreationTime")] Vote vote)
        {
            if (id != vote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoteExists(vote.Id))
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
            ViewData["Movies_Id"] = new SelectList(_context.Movies, "Id", "Id", vote.Movies_Id);
            ViewData["Pi_Id"] = new SelectList(_context.Set<personal_information>(), "Id", "Id", vote.Pi_Id);
            return View(vote);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Votes == null)
            {
                return NotFound();
            }

            var vote = await _context.Votes
                .Include(v => v.Movies)
                .Include(v => v.Pi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vote == null)
            {
                return NotFound();
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Votes == null)
            {
                return Problem("Entity set 'MoviesContext.Votes'  is null.");
            }
            var vote = await _context.Votes.FindAsync(id);
            if (vote != null)
            {
                _context.Votes.Remove(vote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoteExists(int id)
        {
          return (_context.Votes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
