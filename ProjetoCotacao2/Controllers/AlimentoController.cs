using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoCotacao2.Models;
using ProjetoCotacao2.DAL;
using ProjetoCotacao2.Utils;
using System.Web.Helpers;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace ProjetoCotacao2.Controllers
{
    public class AlimentoController : Controller
    {
        private Context db = new Context();


        public ActionResult Create()
        {
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "idCategoria", "nome");
            ViewBag.Estabelecimentos = new SelectList(EstabelecimentoDAO.BuscarEstabelecimentoPorCliente(u.IDUsuarioCliente), "idEstabelecimento", "nome");
            return View();
        }

        public ActionResult Index()
        {
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            return View(AlimentoDAO.BuscarAlimentoPorCliente(u.IDUsuarioCliente));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAlimento,descricao,nome,preco,unidade,quantidade")] Alimento alimento, int? Categorias, int? Estabelecimentos)
        {

            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "idCategoria", "nome");
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            ViewBag.Estabelecimentos = new SelectList(EstabelecimentoDAO.BuscarEstabelecimentoPorCliente(u.IDUsuarioCliente), "idEstabelecimento", "nome");

            if (ModelState.IsValid)
            {
                alimento.Categoria = CategoriaDAO.BuscarCategoriaPorId(Categorias);
                alimento.estabelecimento = EstabelecimentoDAO.BuscarEstabelecimentoPorId(Estabelecimentos);

                AlimentoDAO.CadastrarAlimento(alimento);
                return RedirectToAction("Index", "Alimento");
            }

            return View(alimento);
        }

        public ActionResult Details(int? id)
        {
            return View(AlimentoDAO.BuscarAlimentoPorId(id));
        }

        public ActionResult Alterar(int? id)

        {
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "idCategoria", "nome");
            ViewBag.Estabelecimentos = new SelectList(EstabelecimentoDAO.BuscarEstabelecimentoPorCliente(u.IDUsuarioCliente), "idEstabelecimento", "nome");
            return View(AlimentoDAO.BuscarAlimentoPorId(id));
        }

        [HttpPost]
        public ActionResult Alterar(Alimento alimento, int? Categorias, int? Estabelecimentos)

        {
            ViewBag.Categorias = new SelectList(CategoriaDAO.RetornarCategoria(), "idCategoria", "nome");

            Alimento a = AlimentoDAO.BuscarAlimentoPorId(alimento.idAlimento);
            a.nome = alimento.nome;
            a.preco = alimento.preco;
            a.quantidade = alimento.quantidade;
            a.Categoria = CategoriaDAO.BuscarCategoriaPorId(Categorias);
            a.estabelecimento = EstabelecimentoDAO.BuscarEstabelecimentoPorId(Estabelecimentos);

            AlimentoDAO.AlterarAlimento(a);
            return RedirectToAction("Index", "Alimento");
        }
        public ActionResult Remover(int id)
        {
            AlimentoDAO.RemoverAlimento(AlimentoDAO.BuscarAlimentoPorId(id));
            return RedirectToAction("Index", "Alimento");
        }

        public ActionResult BuscaNome()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscaNome(Alimento alimento)
        {
            List<Alimento> alimentos = AlimentoDAO.RetornarAlimentosPorNome2(alimento);
            ViewBag.Alimento = alimentos;
            return View();
        }

        public ActionResult AdicionarCotacao(int id)
        {
            Alimento alimento = AlimentoDAO.BuscarAlimentoPorId(id);
            AlimentoCotacao Ac = new AlimentoCotacao
            {
                alimento = alimento,
                Quantidade = 1,
                Preco = alimento.preco,
                CarrinhoId = Sessao.RetornarCarrinhoId()
            };

            AlimentoCotacaoDAO.CadastrarAlimentoCotacao(Ac);
            return RedirectToAction("BuscaNome", "Alimento");

        }

        public ActionResult CotacaoAlimento()
        {
            return View(AlimentoCotacaoDAO.RetornarItensVenda());
        }
        public ActionResult AdicionarQuantidade(int id)
        {
            AlimentoCotacaoDAO.AdicionarQuantidade(id);
            return RedirectToAction("CotacaoAlimento", "Alimento");

        }
        public ActionResult DiminuirQuantidade(int id)
        {
            AlimentoCotacaoDAO.DiminuirQuantidade(id);
            return RedirectToAction("CotacaoAlimento", "Alimento");

        }
        public ActionResult RemoverItem(int id)
        {
            AlimentoCotacaoDAO.RemoverItem(id);
            return RedirectToAction("CotacaoAlimento", "Alimento");
        }


        [HttpPost]
        public ActionResult EnviarEmail(UsuarioCliente cotacao)
        {
            String Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            String email = cotacao.Email_Cliente;
            String assunto = "Cotação de Preços - VENDEDOR: " + u.Nome_Cliente;
            String nome = u.Nome_Cliente;

            WebMail.SmtpUseDefaultCredentials = false;
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "airtonteste@gmail.com";
            WebMail.Password = "senha";
            WebMail.From = "airtonteste@gmail.com";


            List<AlimentoCotacao> a;
            a = AlimentoCotacaoDAO.RetornarItensVenda();
            string alimentos = string.Empty;
            for (int i = 0; i < a.Count; i++)
            {
                alimentos += string.Format("Nome do alimento: {0}, Descrição do alimento: {1}, Preço do alimento: {2}, Quantidade do alimento: {3} ", a[i].alimento.nome.ToUpper(), a[i].alimento.descricao.ToUpper(), a[i].alimento.preco.ToString("C2"), a[i].Quantidade, Environment.NewLine, Environment.NewLine);
            }

            if (string.IsNullOrEmpty(alimentos))
                alimentos = "Nenhum alimento encontrado";

            try
            {

                WebMail.Send(to: email,
                    from: "airtonteste@gmail.com",
                       subject: assunto,
                       body: alimentos +
                        "<p>Caso tenha interesse, favor entrar em contato neste número: " +u.Telefone_Cliente + "</p>"+
                       "<p> Agradecemos o contato!</p>" 
                       
                       );


                return RedirectToAction("Index", "UsuarioCliente");
            }
            catch
            {
                return RedirectToAction("Index", "UsuarioCliente");
            }
        }

        public ActionResult Email()
        {
            return View();
        }

    }
}