using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPW217_PortfolioProject2021.Models
{
	public class Item
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [NotMapped] // Does not get stored in DB
        public IFormFile Photo { get; set; }

        [NotMapped]
        public IFormFile Model { get; set; }

        /// <summary>
        /// URL where the product photo is stored
        /// </summary>
        public string PhotoUrl { get; set; }

        public string ModelUrl { get; set; }
    }
}
