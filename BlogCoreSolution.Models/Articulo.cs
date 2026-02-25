using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogCoreSolution.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del articulo es obligatorio.")]
        [Display(Name = "Nombre del Articulo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del articulo es obligatorio.")]
        [Display(Name = "Nombre del Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "FechaCreacion de Creación")]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "UrlImagen de Url Imagen")]
        public string UrlImagen { get; set; }

        [Required(ErrorMessage = "La categoría es obligatorio.")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        public Articulo()
        {
            FechaCreacion = DateTime.Now;
        }

    }
}
