﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Index(int idUser, bool rupt, bool course)
        {
            List<StockProduit> produits = new List<StockProduit>();
            produits = await _context.StockProduit.Where(x => x.utilisateur_id == idUser).Include(sp => sp.Produit).ToListAsync();
            if (rupt)
                produits = produits.Where(x => x.stockProduit_qteStock == 0).ToList();
            if (course)
                produits = produits.Where(x => x.stockProduit_course == true).ToList();
            return View(produits);
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
                stockProduit.utilisateur_id = (int)HttpContext.Session.GetInt32("UtilisateurId");
                stockProduit.stockProduit_course = false;
                _context.Add(stockProduit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),"Home");
            }
            return RedirectToAction(nameof(Index), "Home");
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


        public void EditProd(int IdStockProd, int Qte, bool Course)
        {
            StockProduit unStockProd = _context.StockProduit.Where(x => x.stockProduit_id == IdStockProd).FirstOrDefault();
            unStockProd.stockProduit_course = Course;
            unStockProd.stockProduit_qteStock = Qte;
            _context.Update(unStockProd);
            _context.SaveChanges();

        }
    }
}
