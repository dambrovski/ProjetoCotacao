using ProjetoCotacao2.DAL;
using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoCotacao2.Controllers
{
    public class EstabelecimentoController : Controller
    {
        private Context db = new Context();
        public ActionResult Index()
        {
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            int id = u.IDUsuarioCliente;
            return View(EstabelecimentoDAO.BuscarEstabelecimentoPorCliente(id));
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEstabelecimento,nome,telefone,cnpj,cidade,endereco,tipoEstabelecimento")] Estabelecimento estabelecimento, int? id)
        {

            if (ModelState.IsValid)
            {

                String Email_Cliente = User.Identity.Name;
                UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
                id = u.IDUsuarioCliente;

                EstabelecimentoDAO.CadastrarEstabelecimento(estabelecimento, id);
                return RedirectToAction("Index/" + id + "", "Estabelecimento");

            }

            return View(estabelecimento);
        }

        public ActionResult ListAll()
        {
            return View(db.Estabelecimentos.ToList());
        }

        public ActionResult Edit(int? id)
        {
            return View(EstabelecimentoDAO.BuscarEstabelecimentoPorId(id));
        }
        [HttpPost]
        public ActionResult Edit(Estabelecimento estabelecimento, int? id)
        {

            Estabelecimento e = EstabelecimentoDAO.BuscarEstabelecimentoPorId(estabelecimento.idEstabelecimento);
            e.nome = estabelecimento.nome;
            e.cidade = estabelecimento.cidade;
            e.cnpj = estabelecimento.cnpj;
            e.endereco = estabelecimento.endereco;
            e.telefone = estabelecimento.telefone;

            EstabelecimentoDAO.AlterarEstabelecimento(e);
            return RedirectToAction("Index", "Estabelecimento");
        }
        public ActionResult Details(int? id)
        {
            return View(EstabelecimentoDAO.BuscarEstabelecimentoPorId(id));
        }
        public ActionResult Remover(int? id)
        {
            EstabelecimentoDAO.RemoverEstabelecimento(EstabelecimentoDAO.BuscarEstabelecimentoPorId(id));
            return RedirectToAction("Index", "Estabelecimento");
        }
    }
}