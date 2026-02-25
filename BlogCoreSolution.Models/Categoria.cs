using System.ComponentModel.DataAnnotations;

namespace BlogCoreSolution.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [MaxLength(60)]
        [Display(Name = "Nombre de Categoria")]
        public String Nombre { get; set; }

        [Display(Name = "Orden de Visualización")]
        [Range(1, 10, ErrorMessage = "El valor debe de estar entre 1 y 100")]
        public int Orden { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Categoria()
        {
            FechaCreacion = DateTime.Now;
        }


    }
}
