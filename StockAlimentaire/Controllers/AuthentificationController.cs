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
                HttpContext.Session.SetString("UtilisateurNom", user1.utilisateur_nom);
                //HttpContext.Session.GetString("UtilisateurNom");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = string.Format("Votre pseudo ou votre mot de passe est incorrect");
                return View("Authentification");
            }
        }

        private string Decrypt(string result1)
        {
            throw new NotImplementedException();
        }
    }
}