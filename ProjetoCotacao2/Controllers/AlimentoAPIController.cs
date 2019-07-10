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
    //[RoutePrefix("api/Produto")]
    [RoutePrefix("api/Alimento")]
    public class AlimentoAPIController : ApiController
    {
        //GET: api/Alimento/Alimentos
        [Route("Alimentos")]
        public List<Alimento> GetAlimentos()
        {
            return AlimentoDAO.RetornarAlimentos();
        }



        //GET: api/Alimento/AlimentoDinamico/2
        [HttpGet]
        [Route("AlimentoDinamico/{id}")]
        public IHttpActionResult AlimentoDinamico(int id)
        {
            Alimento a = AlimentoDAO.BuscarAlimentoPorId(id);
            if (a != null)
            {
                dynamic dinamico = new
                {
                    NomeProduto = a.nome,
                    QuantidadeProduto = a.quantidade,
                    PrecoProduto = a.preco,
                    DataEnvio = DateTime.Now
                };
                return Ok(dinamico);
            }
            return NotFound();
        }
    }
}
