using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockAlimentaire.Models
{
    public class Produit
    {
        [Key]
        public int idProd { get; set; }
        [ForeignKey("Utilisateur")]
        public int utilisateur_id { get; set; }
        [ForeignKey("Stock")]
        public int stock_id { get; set; }
        public string nomProd { get; set; }
        public int quantite { get; set; }
    }
}
