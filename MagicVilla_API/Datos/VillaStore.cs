using MagicVilla_API.Modelos.villaDto;


namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {// simulacion base de datos
            new VillaDto{Id=1, Nombre="Vista a la Piscina", Ocupantes =4, MetrosCuadrados=50},
            new VillaDto{Id=2, Nombre="Vista a la Playa", Ocupantes =8, MetrosCuadrados=45}
        };
    }
}
