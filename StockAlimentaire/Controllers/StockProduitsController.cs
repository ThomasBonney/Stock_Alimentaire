using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockAlimentaire.Models;

namespace StockAlimentaire.Controllers
{
    public class StockProduitsController : Controller
    {
        private readonly StockAlimentaireContext _context;

        public StockProduitsController(StockAlimentaireContext context)
        {
            _context = context;
        }

        // GET: StockProduits
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockProduit.ToListAsync());
        }

        // GET: StockProduits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProduit = await _context.StockProduit
                .FirstOrDefaultAsync(m => m.stockProduit_id == id);
            if (stockProduit == null)
            {
                return NotFound();
            }

            return View(stockProduit);
        }

        // GET: StockProduits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockProduits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stockProduit_id,produit_id,stock_id,stockProduit_qteStock")] StockProduit stockProduit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockProduit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockProduit);
        }

        // GET: StockProduits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProduit = await _context.StockProduit.FindAsync(id);
            if (stockProduit == null)
            {
                return NotFound();
            }
            return View(stockProduit);
        }

        // POST: StockProduits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stockProduit_id,produit_id,stock_id,stockProduit_qteStock")] StockProduit stockProduit)
        {
            if (id != stockProduit.stockProduit_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockProduit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockProduitExists(stockProduit.stockProduit_id))
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
            return View(stockProduit);
        }

        // GET: StockProduits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockProduit = await _context.StockProduit
                .FirstOrDefaultAsync(m => m.stockProduit_id == id);
            if (stockProduit == null)
            {
                return NotFound();
            }

            return View(stockProduit);
        }

        // POST: StockProduits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockProduit = await _context.StockProduit.FindAsync(id);
            _context.StockProduit.Remove(stockProduit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockProduitExists(int id)
        {
            return _context.StockProduit.Any(e => e.stockProduit_id == id);
        }
    }
}
