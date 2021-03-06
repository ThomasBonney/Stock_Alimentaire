﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockAlimentaire.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace StockAlimentaire.Controllers
{
    public class StocksController : Controller
    {
        private readonly StockAlimentaireContext _context;

        public StocksController(StockAlimentaireContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            int idUser = (int)HttpContext.Session.GetInt32("UtilisateurId");
            return View(await _context.Stock.Where(x=>x.utilisateur_id == idUser).ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Stock stock = await _context.Stock.FirstOrDefaultAsync(m => m.stock_id == id);
            stock.produits = new List<StockProduit>();
            if(_context.StockProduit.Where(x => x.stock_id == id).ToListAsync()!= null)
                stock.produits = await _context.StockProduit.Where(x => x.stock_id == id).Include(sp=>sp.Produit).ToListAsync();
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stock_id,utilisateur_id,stock_nom")] Stock stock)
        {
            stock.utilisateur_id = (int)HttpContext.Session.GetInt32("UtilisateurId");

            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stock_id,utilisateur_id,stock_nom")] Stock stock)
        {
            if (id != stock.stock_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.stock_id))
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
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stock
                .FirstOrDefaultAsync(m => m.stock_id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stock.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.Stock.Any(e => e.stock_id == id);
        }

        public void EditProd(int IdStockProd, int Qte, bool Course)
        {
            StockProduit unStockProd = _context.StockProduit.Where(x => x.stockProduit_id == IdStockProd).FirstOrDefault();
            unStockProd.stockProduit_course = Course;
            unStockProd.stockProduit_qteStock = Qte;
            _context.Update(unStockProd);
            _context.SaveChanges();

        }

        public async Task<IActionResult> DeleteStockProd(int id)
        {
            var stockProduit = await _context.StockProduit.FindAsync(id);
            _context.StockProduit.Remove(stockProduit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
