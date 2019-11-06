using System;
using System.Text;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers; 

namespace Alura.ListaLeitura.Api.Formatters
{
    public class LivroCsvFormatter : TextOutputFormatter
    {

        public LivroCsvFormatter()
        {
            //PRESTA ATENCAO QUE TEM DOIS USING DESSA CLASSE, USAR O DA MICROSOFT, NÃO O SYSTEM
            var csvMediaType = MediaTypeHeaderValue.Parse("text/csv");
            var appMediaType = MediaTypeHeaderValue.Parse("application/csv");
            this.SupportedMediaTypes.Add(csvMediaType);
            this.SupportedMediaTypes.Add(appMediaType);
            this.SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return type == typeof(LivroUpload);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var livroEmCsv = "";

            if(context.Object is LivroUpload)
            {
                var livro = context.Object as LivroUpload;

                livroEmCsv = $"{livro.Titulo};{livro.Subtitulo};{livro.Autor};{livro.Lista}";
            }

            using (var escritor = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
            {
                return escritor.WriteAsync(livroEmCsv);
            }
        }
    }
}
