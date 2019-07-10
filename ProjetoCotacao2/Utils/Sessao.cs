using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Utils
{
    public class Sessao
    {
        private static string carrinhoId = "CARRINHO_ID";
        private static string usuario = "USUARIO";

        public static void ZerarSessao()
        {
            HttpContext.Current.Session[carrinhoId] = null;
        }


        public static string RetornarCarrinhoId()
        {
            if (HttpContext.Current.Session[carrinhoId] == null)
            {
                Guid guid = Guid.NewGuid();
                HttpContext.Current.Session[carrinhoId] = guid.ToString();
            }
            return HttpContext.Current.Session[carrinhoId].ToString();
        }

        public static string Email_Cliente(string Email_Cliente)
        {
            HttpContext.Current.Session[usuario] = Email_Cliente;
            return HttpContext.Current.Session[usuario].ToString();
        }


        public static string RetornarUsuario()
        {
            if (HttpContext.Current.Session[usuario] == null)
            {
                return null;
            }
            return HttpContext.Current.Session[usuario].ToString();
        }
    }
}
