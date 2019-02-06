using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockAlimentaire.Models;

namespace StockAlimentaire.Models
{
    public class StockAlimentaireContext : DbContext
    {
        public StockAlimentaireContext (DbContextOptions<StockAlimentaireContext> options)
            : base(options)
        {
        }
        public DbSet<StockAlimentaire.Models.Utilisateur> Utilisateur { get; set; }
        public DbSet<StockAlimentaire.Models.StockProduit> StockProduit { get; set; }
        public DbSet<StockAlimentaire.Models.Stock> Stock { get; set; }
        public DbSet<StockAlimentaire.Models.Produit> Produit { get; set; }
    }
}
