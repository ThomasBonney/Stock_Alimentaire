using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAlimentaire.Models
{
    public class Stock
    {
        [Key]
        public int  stock_id   { get; set; }
        [ForeignKey("Utilisateur")]
        public int  utilisateur_id  { get; set; }
        public string stock_nom { get; set; }

    }

    /*public string GetNameUser(int id)
    {
        using (var db = new StockAlimentaireContext())
        {
            Utilisateur user = db.Utilisateur.Where(x => x.utilisateur_pseudo == pseudoUser).FirstOrDefault();
            return user.utilisateur_pseudo;
        }

    }*/
}
