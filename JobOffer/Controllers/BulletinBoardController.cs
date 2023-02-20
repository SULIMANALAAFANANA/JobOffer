using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobOffer.Data;
using JobOffer.Models;

namespace JobOffer.Controllers
{
    public class BulletinBoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BulletinBoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BulletinBoard
        public async Task<IActionResult> Index()
        {
            return View(await _context.BulletinBoards.ToListAsync());
        }

        // GET: BulletinBoard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bulletinBoard = await _context.BulletinBoards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bulletinBoard == null)
            {
                return NotFound();
            }

            return View(bulletinBoard);
        }

        // GET: BulletinBoard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BulletinBoard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,JobCategoryId,JobTypeId,PostalCode,Title,Description,Wage")] BulletinBoard bulletinBoard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bulletinBoard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bulletinBoard);
        }

        // GET: BulletinBoard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bulletinBoard = await _context.BulletinBoards.FindAsync(id);
            if (bulletinBoard == null)
            {
                return NotFound();
            }
            return View(bulletinBoard);
        }

        // POST: BulletinBoard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuthorId,JobCategoryId,JobTypeId,PostalCode,Title,Description,Wage")] BulletinBoard bulletinBoard)
        {
            if (id != bulletinBoard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bulletinBoard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BulletinBoardExists(bulletinBoard.Id))
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
            return View(bulletinBoard);
        }

        // GET: BulletinBoard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bulletinBoard = await _context.BulletinBoards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bulletinBoard == null)
            {
                return NotFound();
            }

            return View(bulletinBoard);
        }

        // POST: BulletinBoard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bulletinBoard = await _context.BulletinBoards.FindAsync(id);
            _context.BulletinBoards.Remove(bulletinBoard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BulletinBoardExists(int id)
        {
            return _context.BulletinBoards.Any(e => e.Id == id);
        }
    }
}
