using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockAlimentaire.Models
{
    public class Utilisateur
    {
        [Key]
        public int      utilisateur_id          { get; set; }
        public string   utilisateur_nom         { get; set; }
        public string   utilisateur_prenom      { get; set; }
        public string   utilisateur_pseudo      { get; set; }
        public string   utilisateur_motDePasse  { get; set; }
    }
}
