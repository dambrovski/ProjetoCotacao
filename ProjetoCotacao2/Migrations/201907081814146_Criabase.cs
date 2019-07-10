namespace ProjetoCotacao2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Criabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alimentos",
                c => new
                    {
                        idAlimento = c.Int(nullable: false, identity: true),
                        descricao = c.String(nullable: false),
                        nome = c.String(nullable: false),
                        preco = c.Double(nullable: false),
                        unidade = c.String(nullable: false),
                        quantidade = c.Int(nullable: false),
                        Categoria_idCategoria = c.Int(),
                        estabelecimento_idEstabelecimento = c.Int(),
                    })
                .PrimaryKey(t => t.idAlimento)
                .ForeignKey("dbo.Categorias", t => t.Categoria_idCategoria)
                .ForeignKey("dbo.Estabelecimentos", t => t.estabelecimento_idEstabelecimento)
                .Index(t => t.Categoria_idCategoria)
                .Index(t => t.estabelecimento_idEstabelecimento);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.Estabelecimentos",
                c => new
                    {
                        idEstabelecimento = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false),
                        telefone = c.String(nullable: false),
                        cnpj = c.String(nullable: false),
                        cidade = c.String(nullable: false),
                        endereco = c.String(nullable: false),
                        usuariocliente_IDUsuarioCliente = c.Int(),
                    })
                .PrimaryKey(t => t.idEstabelecimento)
                .ForeignKey("dbo.UsuarioClientes", t => t.usuariocliente_IDUsuarioCliente)
                .Index(t => t.usuariocliente_IDUsuarioCliente);
            
            CreateTable(
                "dbo.UsuarioClientes",
                c => new
                    {
                        IDUsuarioCliente = c.Int(nullable: false, identity: true),
                        Email_Cliente = c.String(),
                        Login = c.String(nullable: false),
                        SenhaCliente = c.String(nullable: false),
                        CPF_Cliente = c.String(nullable: false),
                        Nome_Cliente = c.String(nullable: false),
                        Telefone_Cliente = c.String(nullable: false),
                        id = c.String(),
                        Cep = c.String(),
                        Logradouro = c.String(),
                        Localidade = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.IDUsuarioCliente);
            
            CreateTable(
                "dbo.AlimentoCotacao",
                c => new
                    {
                        AlimentoCotacaoId = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Preco = c.Double(nullable: false),
                        CarrinhoId = c.String(),
                        alimento_idAlimento = c.Int(),
                    })
                .PrimaryKey(t => t.AlimentoCotacaoId)
                .ForeignKey("dbo.Alimentos", t => t.alimento_idAlimento)
                .Index(t => t.alimento_idAlimento);
            
            CreateTable(
                "dbo.PrevisaoTempos",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                        state = c.String(),
                        country = c.String(),
                        data_temperature = c.Int(nullable: false),
                        data_wind_direction = c.String(),
                        data_humidity = c.Single(nullable: false),
                        data_condition = c.String(),
                        data_pressure = c.Single(nullable: false),
                        data_icon = c.String(),
                        data_date = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlimentoCotacao", "alimento_idAlimento", "dbo.Alimentos");
            DropForeignKey("dbo.Estabelecimentos", "usuariocliente_IDUsuarioCliente", "dbo.UsuarioClientes");
            DropForeignKey("dbo.Alimentos", "estabelecimento_idEstabelecimento", "dbo.Estabelecimentos");
            DropForeignKey("dbo.Alimentos", "Categoria_idCategoria", "dbo.Categorias");
            DropIndex("dbo.AlimentoCotacao", new[] { "alimento_idAlimento" });
            DropIndex("dbo.Estabelecimentos", new[] { "usuariocliente_IDUsuarioCliente" });
            DropIndex("dbo.Alimentos", new[] { "estabelecimento_idEstabelecimento" });
            DropIndex("dbo.Alimentos", new[] { "Categoria_idCategoria" });
            DropTable("dbo.PrevisaoTempos");
            DropTable("dbo.AlimentoCotacao");
            DropTable("dbo.UsuarioClientes");
            DropTable("dbo.Estabelecimentos");
            DropTable("dbo.Categorias");
            DropTable("dbo.Alimentos");
        }
    }
}
