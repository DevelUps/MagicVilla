using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.VillaDTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }
    }
}
