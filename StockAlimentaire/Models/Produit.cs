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
        public string nomProd { get; set; }
    }
}
