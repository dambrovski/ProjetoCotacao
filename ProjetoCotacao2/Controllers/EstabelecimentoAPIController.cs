using ProjetoCotacao2.DAL;
using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjetoCotacao2.Controllers
{
    [RoutePrefix("api/Estabelecimento")]
    public class EstabelecimentoAPIController : ApiController
    {
        //GET: api/Estabelecimento/Estabelecimentos
        [Route("Estabelecimentos")]
        public List<Estabelecimento> GetEstabelecimentos()
        {
            return EstabelecimentoDAO.RetornarEstabelecimentos();
        }


        //GET: api/Estabelecimento/EstabelecimentoDinamico/2
        [HttpGet]
        [Route("EstabelecimentoDinamico/{id}")]
        public IHttpActionResult EstabelecimentoDinamico(int id)
        {
            Estabelecimento e = EstabelecimentoDAO.BuscarEstabelecimentoPorId(id);
            if (e != null)
            {
                dynamic dinamico = new
                {
                    NomeEstabelecimento = e.nome,
                    Telefone = e.telefone,
                    Cnpj = e.cnpj,
                    Cidade = e.cidade,
                    Endereco = e.endereco,
                    DataEnvio = DateTime.Now
                };
                return Ok(dinamico);
            }
            return NotFound();
        }

    }
}
