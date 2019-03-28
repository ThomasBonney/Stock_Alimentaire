using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAlimentaire.Models;

namespace StockAlimentaire.Controllers
{
    public class HomeController : Controller
    {
        private readonly StockAlimentaireContext _context;

        public HomeController(StockAlimentaireContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DashboardModel dashboard = new DashboardModel
            {
                nbStock = NbStock()
            };
            return View(dashboard);
        }

        public int NbStock()
        {
            int nbStock = 0;
            var lStock = _context.Stock.Where(x=>x.utilisateur_id == (int)HttpContext.Session.GetInt32("UtilisateurId")).ToListAsync();
            if (lStock != null)
            {
                nbStock = _context.Stock.Count();
            }
            return nbStock;
        }

        public int NbProdRupt()
        {
            int nbProdRupt = 0;
            var lProduit = _context.Produit.Where(x => x.utilisateur_id == (int)HttpContext.Session.GetInt32("UtilisateurId") && x.quantite == 0).ToListAsync();
            if (lProduit != null)
            {
                nbProdRupt = _context.Produit.Count();
            }
            return nbProdRupt;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}