using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoCotacao2.Models;
using Newtonsoft.Json;
using ProjetoCotacao2.DAL;
using System.Text;

namespace ProjetoCotacao2.Controllers
{
    public class PrevisaoTempoController : Controller
    {
        // GET: PrevisaoTempo


        public ActionResult ConsultaClima(PrevisaoTempo previsao)
        {
            // PrevisaoTempo usuario = new PrevisaoTempo();

            try
            {


                string url = "http://apiadvisor.climatempo.com.br/api/v1/locale/city?name=" + previsao.name + "&" + previsao.state + "&token=866474fc3c51b2f4229db9d8f11648de";

                WebClient client = new WebClient();
                string json = client.DownloadString(url);
                byte[] bytes = Encoding.Default.GetBytes(json);
                json = Encoding.UTF8.GetString(bytes);
                var _Data = JsonConvert.DeserializeObject<List<PrevisaoTempo>>(json);

                foreach (PrevisaoTempo id in _Data)
                {
                    previsao.id = id.id;
                }
                String url2 = "http://apiadvisor.climatempo.com.br/api/v1/weather/locale/" + previsao.id + "/current?token=866474fc3c51b2f4229db9d8f11648de";
                WebClient client2 = new WebClient();
                string json2 = client2.DownloadString(url2);
                byte[] bytes2 = Encoding.Default.GetBytes(json2);
                json2 = Encoding.UTF8.GetString(bytes2);
                previsao = JsonConvert.DeserializeObject<PrevisaoTempo>(json2);


                TempData["Previsao"] = previsao;

                return RedirectToAction("Create", "PrevisaoTempo");

            }
            catch (Exception)
            {

                return RedirectToAction("Create", "PrevisaoTempo");
            }
        }



        public ActionResult Index()
        {
            PrevisaoTempo usuario = new PrevisaoTempo();
            string Email_Cliente = User.Identity.Name;
            UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);
            string url = "http://apiadvisor.climatempo.com.br/api/v1/weather/locale/" + u.id + "/current?token=866474fc3c51b2f4229db9d8f11648de";
            WebClient client = new WebClient();
            string json = client.DownloadString(url);
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            usuario = JsonConvert.DeserializeObject<PrevisaoTempo>(json);

            TempData["Usuario"] = usuario;
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Index(PrevisaoTempo usuario)
        {


            //try
            //{
            //    string Email_Cliente = User.Identity.Name;
            //    UsuarioCliente u = UsuarioClienteDAO.BuscarClienteporEmail(Email_Cliente);

            //    string url = "http://apiadvisor.climatempo.com.br/api/v1/weather/locale/" + u.id + "/current?token=866474fc3c51b2f4229db9d8f11648de";
            //    WebClient client = new WebClient();
            //    string json = client.DownloadString(url);
            //    byte[] bytes = Encoding.Default.GetBytes(json);
            //    json = Encoding.UTF8.GetString(bytes);
            //    //usuario = JsonConvert.DeserializeObject<UsuarioCliente>(json);
            //    usuario = JsonConvert.DeserializeObject<PrevisaoTempo>(json);

            //    TempData["Usuario"] = usuario;

            //    return RedirectToAction("Index", "PrevisaoTempo");


            //}
            //catch (Exception)
            //{

            return RedirectToAction("Index", "PrevisaoTempo");
            //    }

        }


        public ActionResult Create()
        {
            PrevisaoTempo previsao = (PrevisaoTempo)TempData["Previsao"];
            return View(previsao);
        }
    }
}