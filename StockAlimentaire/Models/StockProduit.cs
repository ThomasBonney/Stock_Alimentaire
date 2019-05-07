using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAlimentaire.Models
{
    public class StockProduit
    {
        [Key]
        public int stockProduit_id { get; set; }
        [ForeignKey("Produit")]
        public int produit_id { get; set; }
        [ForeignKey("Stock")]
        public int stock_id { get; set; }
        [ForeignKey("Utilisateur")]
        public int utilisateur_id { get; set; }

        public int stockProduit_qteStock { get; set; }
        public bool stockProduit_course { get; set; }

    }
}
