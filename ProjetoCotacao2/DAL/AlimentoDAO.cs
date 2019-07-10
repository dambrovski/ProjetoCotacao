using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.DAL
{
    public class AlimentoDAO
    {

        private static Context ctx = Singleton.Instance.Context;

        public static Alimento BuscarALimentoPorNome(Alimento alimento)
        {
            try
            {
                Alimento a = ctx.Alimentos.FirstOrDefault(x => x.nome.Equals(alimento.nome));
                return a;
            }
            catch
            {
                return null;
            }
        }

        public static List<Alimento> RetornarAlimentosPorNome(Alimento alimento)
        {
            return ctx.Alimentos.Where(x => x.nome == alimento.nome).ToList();
        }

        public static List<Alimento> RetornarAlimentosPorNome2(Alimento alimento)
        {
            return ctx.Alimentos.Include("Estabelecimento").Where(x => x.nome.Contains(alimento.nome)).ToList();
        }

        public static Alimento BuscarAlimentoPorId(int? id)
        {
            return ctx.Alimentos.Find(id);
        }

        public static bool CadastrarAlimento(Alimento alimento)
        {
            try
            {
                ctx.Alimentos.Add(alimento);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Alimento> BuscarAlimentoPorCliente(int? IDCliente)
        {
            return ctx.Alimentos.Include("Estabelecimento").Where(x => x.estabelecimento.usuariocliente.IDUsuarioCliente == IDCliente).ToList();
        }

        public static void RemoverAlimento(Alimento a)
        {
            ctx.Alimentos.Remove(a);
            ctx.SaveChanges();
        }

        public static void AlterarAlimento(Alimento a)
        {
            ctx.Entry(a).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        //API
        public static List<Alimento> RetornarAlimentos()
        {
            return ctx.Alimentos.Include("Categoria").ToList();
        }

        public static List<Alimento> RetornarAlimentos2(int? id)
        {
            return ctx.Alimentos.Include("Categoria").Include("Estabelecimento").Where(x => x.idAlimento == id).ToList();
        }



        public static List<Alimento> RetornarAlimentosPorCategoria(int? id)
        {
            return ctx.Alimentos.Include("Categoria").Where(x => x.Categoria.idCategoria == id).ToList();
        }

    

    }
}