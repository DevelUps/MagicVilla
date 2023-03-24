﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.villaDto
{
    public class VillaDto
    {
        public int Id { get; set; }
       
        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }
        
        [Required]
        public double Tarifa { get; set; }

        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set; }

        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

        
    }
}
