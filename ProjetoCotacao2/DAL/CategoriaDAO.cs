using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.DAL
{
    public class CategoriaDAO
    {
        private static Context ctx = Singleton.Instance.Context;

        public static List<Categoria> RetornarCategoria()
        {
            return ctx.Categorias.ToList();
        }
        public static Categoria BuscarCategoriaPorId(int? id)
        {
            return ctx.Categorias.Find(id);
        }
    }
}