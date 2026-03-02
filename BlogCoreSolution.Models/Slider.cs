using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCoreSolution.Models
{
    public class Slider
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del Slider es obligatorio")]
        [MaxLength(100)]
        [Display(Name = "Nombre de Slider")]
        public String Nombre { get; set; }

        [Required(ErrorMessage = "El estado del Slider es obligatorio")]
        [Display(Name = "Estado de Slider")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "UrlImagen de Url Imagen")]
        public string UrlImagen { get; set; }
    }


    }