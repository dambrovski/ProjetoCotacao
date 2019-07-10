namespace ProjetoCotacao2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class camponovo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UsuarioClientes", "Sobrenome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UsuarioClientes", "Sobrenome");
        }
    }
}
