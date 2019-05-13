using System;
using System.Collections.Generic;

namespace StockAlimentaire.Models
{
    public class AddProdStockModel
    {
        public Produit produit { get; set; }
        public List<Stock> lStock { get; set; }
        public StockProduit stoProd { get; set; }

    }
}
