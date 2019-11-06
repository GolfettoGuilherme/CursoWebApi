using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.Persistencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lista = Alura.ListaLeitura.Modelos.ListaLeitura;


namespace Alura.ListaLeitura.Api.Controllers
{
    [Authorize]   
    [ApiController]
    [Route("api/[controller]")]
    public class ListasLeiturasController : ControllerBase
    {

        private readonly IRepository<Livro> _repo;

        public ListasLeiturasController(IRepository<Livro> repo)
        {
            this._repo = repo;
        }

        private Lista CriaLista(TipoListaLeitura tipo)
        {
            return new Lista
            {
                Tipo = tipo.ParaString(),
                Livros = _repo.All.Where(l => l.Lista == tipo).Select(l => l.ToApi2()).ToList()
            };
        }



        [HttpGet]
        public IActionResult TodasListas()
        {
            Lista paraLer = CriaLista(TipoListaLeitura.ParaLer);
            Lista lendo = CriaLista(TipoListaLeitura.Lendo);
            Lista lidos = CriaLista(TipoListaLeitura.Lidos);
            var colecao = new List<Lista>(){ paraLer, lendo, lidos};

            return Ok(colecao);
        }

        [HttpGet("{tipo}")]
        public IActionResult Recuperar(TipoListaLeitura tipo)
        {

            var lista = CriaLista(tipo);
            return Ok(lista);
        }
    }
}
