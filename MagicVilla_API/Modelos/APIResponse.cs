using System.Net;

namespace MagicVilla_API.Modelos
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsExitoso { get; set; }
        public List<string> ErrorMensasges { get; set; }
        public object Resultado { get; set; }

    }
}
