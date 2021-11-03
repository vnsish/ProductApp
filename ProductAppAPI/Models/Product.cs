using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAppAPI.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Estoque")]
        public int Stock { get; set; }
        [Display(Name = "Preço")]
        [Range(0, Double.MaxValue, ErrorMessage = "O preço deve ser positivo.")]
        public double Price { get; set; }
    }
}
