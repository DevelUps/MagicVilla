using MagicVilla_API.Modelos.VillaDTO;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> VillaList = new List<VillaDTO>
        {
            new VillaDTO{Id=1, Nombre="Vista a la Piscina"},
            new VillaDTO{Id=2, Nombre="Vista a la Playa"}
        };
    }
}
