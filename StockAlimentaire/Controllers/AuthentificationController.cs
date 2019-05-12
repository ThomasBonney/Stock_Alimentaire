using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockAlimentaire.Models;

namespace StockAlimentaire.Controllers
{
    public class AuthentificationController : Controller
    {
        //StockAlimentaireContext Db_utilisateur = new StockAlimentaireContext();
        private readonly StockAlimentaireContext _context;

        public AuthentificationController(StockAlimentaireContext context)
        {
            _context = context;
        }


        public IActionResult Authentification()
        {
            return View();
        }



        [HttpPost]
        public ActionResult AuthentificationUtilisateur(String inputPseudo, String inputPassword)
        {
            var query = (
                            from    user in _context.Utilisateur
                            where   user.utilisateur_pseudo == inputPseudo
                            select  user);
            Utilisateur user1 = query.FirstOrDefault<Utilisateur>();
            if (user1 != null && user1.utilisateur_motDePasse == inputPassword)
            {
                //HttpContext.Session.GetString("UtilisateurNom");
                HttpContext.Session.SetInt32("UtilisateurId", user1.utilisateur_id);
                HttpContext.Session.SetString("Pseudo", user1.utilisateur_pseudo);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = string.Format("Votre pseudo ou votre mot de passe est incorrect");
                return View("Authentification");
            }
        }

        // GET: Authentification/DeconnexionUtilisateur
        public ActionResult DeconnexionUtilisateur()
        {
            HttpContext.Session.SetString("Pseudo", "");
            return View("Authentification");
        }



        private string Decrypt(string result1)
        {
            throw new NotImplementedException();
        }
    }
}