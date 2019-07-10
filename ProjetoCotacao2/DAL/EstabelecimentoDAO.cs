using ProjetoCotacao2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.DAL
{
    public class EstabelecimentoDAO
    {
        private static Context ctx = Singleton.Instance.Context;

        public static bool CadastrarEstabelecimento(Estabelecimento estabelecimento, int? id)
        {
            try
            {
                Estabelecimento estabelecimento2 = new Estabelecimento();
                estabelecimento2.nome = estabelecimento.nome;
                estabelecimento2.telefone = estabelecimento.telefone;
                estabelecimento2.endereco = estabelecimento.endereco;
                estabelecimento2.cnpj = estabelecimento.cnpj;
                estabelecimento2.cidade = estabelecimento.cidade;
                estabelecimento2.usuariocliente = UsuarioClienteDAO.BuscarClienteporID(id);
                ctx.Estabelecimentos.Add(estabelecimento2);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Estabelecimento> RetornarEstabelecimentos()
        {
            return ctx.Estabelecimentos.ToList();
        }

        public static void RemoverEstabelecimento(Estabelecimento e)
        {
            ctx.Estabelecimentos.Remove(e);
            ctx.SaveChanges();
        }
        public static Estabelecimento BuscarEstabelecimentoPorId(int? id)
        {
            return ctx.Estabelecimentos.Find(id);
        }

        public static void AlterarEstabelecimento(Estabelecimento e)
        {
            ctx.Entry(e).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static List<Estabelecimento> BuscarEstabelecimentoPorCliente(int? IDCliente)
        {
            return ctx.Estabelecimentos.Include("UsuarioCliente").Where(x => x.usuariocliente.IDUsuarioCliente == IDCliente).ToList();
        }


        public static List<Estabelecimento> BuscarEstabelecimentoPorClienteApi(int id)
        {
            return ctx.Estabelecimentos.Include("UsuarioCliente").Where(x => x.usuariocliente.IDUsuarioCliente == id).ToList();
        }


    }
}