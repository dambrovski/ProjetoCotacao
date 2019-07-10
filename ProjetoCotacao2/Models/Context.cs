using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoCotacao2.Models
{
    public class Context : DbContext
     {
       public Context() : base("ProjetoCotacao") { }
       public DbSet<Alimento> Alimentos{ get; set; }
       public DbSet<Categoria> Categorias { get; set; }
       public DbSet<Estabelecimento> Estabelecimentos { get; set; }
       public DbSet<UsuarioCliente> UsuarioClientes { get; set; }
       public DbSet<PrevisaoTempo> PrevisaoTempos { get; set; }
       public DbSet<AlimentoCotacao> AlimentosCotacao { get; set; }
    }

}