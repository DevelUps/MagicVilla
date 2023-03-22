using MagicVilla_API.Modelos.VillaDTO;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {// simulacion base de datos
            new VillaDTO{Id=1, Nombre="Vista a la Piscina"},
            new VillaDTO{Id=2, Nombre="Vista a la Playa"}
        };
    }
}
