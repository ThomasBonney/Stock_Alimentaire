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
    }
}
