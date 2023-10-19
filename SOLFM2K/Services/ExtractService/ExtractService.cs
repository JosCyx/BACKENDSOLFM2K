namespace SOLFM2K.Services.ExtractService
{
    public class ExtractService : IExtractService
    {
        private readonly IConfiguration _configuration;

        public ExtractService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ExtractBean()
        {
            string extractedChars = "";
            var bean = _configuration["Bean"];

            // Verifica que la cadena tenga al menos 6 caracteres de longitud.
            if (bean.Length < 6)
            {
                return "La cadena es demasiado corta para extraer caracteres.";
            }

            // Divide la cadena en fragmentos de 6 caracteres y extrae el carácter en la posición 6.
            for (int i = 0; i < bean.Length; i += 6)
            {
                if (i + 6 <= bean.Length)
                {
                    extractedChars += bean[i + 5];
                }
            }

            return extractedChars;
        }

    }
}
