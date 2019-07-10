using ProjetoCotacao2.Models;
using ProjetoCotacao2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.DAL
{
    public class AlimentoCotacaoDAO
    {
        private static Context ctx = Singleton.Instance.Context;


        public static void CadastrarAlimentoCotacao(AlimentoCotacao ac)
        {
            string carrinhoId = Sessao.RetornarCarrinhoId();
            AlimentoCotacao a = ctx.AlimentosCotacao.Include("Alimento").FirstOrDefault(x => x.alimento.idAlimento == ac.alimento.idAlimento && x.CarrinhoId.Equals(carrinhoId));
            if (a == null)
            {
                ctx.AlimentosCotacao.Add(ac);
            }
            else
            {
                a.Quantidade++;
                ctx.Entry(a).State = System.Data.Entity.EntityState.Modified;
            }
            ctx.SaveChanges();
        }
        public static AlimentoCotacao BuscarItemPorId(int id)
        {
            return ctx.AlimentosCotacao.Find(id);
        }
        public static List<AlimentoCotacao> RetornarItensVenda()
        {
            string carrinhoId = Sessao.RetornarCarrinhoId();
            return ctx.AlimentosCotacao.Include("Alimento").
                Where(x => x.CarrinhoId.Equals(carrinhoId)).ToList();
        }

        public static void AdicionarQuantidade(int id)
        {
            AlimentoCotacao ac = BuscarItemPorId(id);
            ac.Quantidade++;
            ctx.Entry(ac).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }

        public static void DiminuirQuantidade(int id)
        {
            AlimentoCotacao ac = BuscarItemPorId(id);
            if (ac.Quantidade > 1)
            {
                ac.Quantidade--;
                ctx.Entry(ac).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public static void RemoverItem(int id)
        {
            AlimentoCotacao ac = BuscarItemPorId(id);
            ctx.AlimentosCotacao.Remove(ac);
            ctx.SaveChanges();
        }
        public static int TotalItensCarrinho()
        {
            try
            {
                string carrinhoId = Sessao.RetornarCarrinhoId();
                return ctx.AlimentosCotacao.Where(x => x.CarrinhoId.Equals(carrinhoId)).Sum(x => x.Quantidade);
            }
            catch
            {
                return 0;
            }
        }
    }
}