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
            ViewBag.pseudo = (string)HttpContext.Session.GetString("Pseudo");
            DashboardModel dashboard = new DashboardModel();
            dashboard.userId = (int)HttpContext.Session.GetInt32("UtilisateurId");
            dashboard.nbStock = NbStock(dashboard.userId);
            dashboard.nbProdRupt = NbProdRupt(dashboard.userId);

            return View(dashboard);
        }

        public int NbStock(int Id)
        {
            int nbStock = 0;
            var lStock = _context.Stock.Where(x=>x.utilisateur_id == Id).ToList();
            if (lStock != null)
            {
                nbStock = lStock.Count();
            }
            return nbStock;
        }

        public int NbProdRupt(int Id)
        {
            int nbProdRupt = 0;
            var lProduit = _context.StockProduit.Where(x => x.utilisateur_id == Id && x.stockProduit_qteStock == 0).ToList();
            if (lProduit != null)
            {
                nbProdRupt = lProduit.Count();
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