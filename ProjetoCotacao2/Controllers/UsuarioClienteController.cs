using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using ProjetoCotacao2.DAL;
using ProjetoCotacao2.Models;
using System.Web.Security;
using ProjetoCotacao2.Utils;
using System.Text;
using Newtonsoft.Json;

namespace ProjetoCotacao2.Controllers
{
    public class UsuarioClienteController : Controller
    {
        private Context db = new Context();

        [HttpPost]
        public ActionResult ConsultarCep(UsuarioCliente usuario)
        {
            string url = "https://viacep.com.br/ws/" + usuario.Cep + "/json/";
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            usuario = JsonConvert.DeserializeObject<UsuarioCliente>(json);
            TempData["Usuario"] = usuario;

            string url2 = "http://apiadvisor.climatempo.com.br/api/v1/locale/city?name=" + usuario.Localidade + "&state=" + usuario.UF + "&token=866474fc3c51b2f4229db9d8f11648de";
            WebClient client2 = new WebClient();
            string json2 = client2.DownloadString(url2);
            byte[] bytes2 = Encoding.Default.GetBytes(json2);

            json2 = Encoding.UTF8.GetString(bytes2);
            var _Data = JsonConvert.DeserializeObject<List<UsuarioCliente>>(json2);

            foreach (UsuarioCliente id in _Data)
            {
                usuario.id = id.id;
            }

            TempData["Usuario"] = usuario;
            return RedirectToAction("Create", "UsuarioCliente");
        }

        public ActionResult Index()
        {
            string Email_Cliente = User.Identity.Name;
            UsuarioCliente usuario = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            int id = usuario.IDUsuarioCliente;
            ViewBag.Estabelecimentos = EstabelecimentoDAO.BuscarEstabelecimentoPorCliente(id);

            if (usuario == null)
            {
                return RedirectToAction("Logout", "UsuarioCliente");
            }

            return View(usuario);
        }

        public ActionResult Lista()
        {
            return View(db.UsuarioClientes.ToList());
        }

        public ActionResult Create()
        {
            UsuarioCliente usuario = (UsuarioCliente)TempData["Usuario"];
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Cep,Logradouro,Localidade,UF,IDUsuarioCliente,Email_Cliente,Login,SenhaCliente,ConfirmacaoSenha,CPF_Cliente,Nome_Cliente,Sobrenome,Telefone_Cliente")] UsuarioCliente usuarioCliente)
        {


            if (UsuarioClienteDAO.BuscarClienteporEmail(usuarioCliente) == null)
            {
                if (ModelState.IsValid)
                {

                    UsuarioClienteDAO.CadastrarUsuario(usuarioCliente);
                    return RedirectToAction("Login", "UsuarioCliente");
                }

            }
            else
            {
                ViewBag.Email = "Email já existente";
            }
            return View(usuarioCliente);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioCliente usuario)
        {
            usuario = UsuarioClienteDAO.BuscarUsuarioPorEmailSenha(usuario);
            if (usuario != null)
            {
                FormsAuthentication.SetAuthCookie(usuario.Email_Cliente, true);
                return RedirectToAction("Index", "UsuarioCliente");

            }
            ModelState.AddModelError("", "Login ou senha incorretos!");
            return View(usuario);
        }

        public ActionResult Edit()
        {
            string Email_Cliente = User.Identity.Name;
            UsuarioCliente usuario = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit(UsuarioCliente usuarioCliente, int? id)
        {
            if (ModelState.IsValid)
            {
                UsuarioClienteDAO.EditarCliente(usuarioCliente);
                return RedirectToAction("Logout", "UsuarioCliente");

            }
            return View(usuarioCliente);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            UsuarioClienteDAO.Limpar();
            return RedirectToAction("Login", "UsuarioCliente");
        }

        public ActionResult SendEmail()
        {
            return View();
        }


        public ActionResult Remover(int? id)
        {
            UsuarioClienteDAO.RemoverUsuario(UsuarioClienteDAO.BuscarClienteporID(id));
            return RedirectToAction("Logout", "UsuarioCliente");
        }
    }
}