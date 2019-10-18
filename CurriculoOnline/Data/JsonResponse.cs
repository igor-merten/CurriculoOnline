using System.Collections.Generic;

namespace CurriculoOnline.Data
{
    public class JsonResponse
    {
        public bool Sucesso { get; set; } = false;
        public string Texto { get; set; } = "";

        public JsonResponse(){}

        public JsonResponse(bool sucesso, string texto)
        {
            Sucesso = sucesso;
            Texto = texto;
        }
    }
}
