using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.villaDto
{
    public class VillaDto
    {
        public int Id { get; set; }
        //parametros obligatorios nombre
        [Required]
        [MaxLength(35)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
    }
}
