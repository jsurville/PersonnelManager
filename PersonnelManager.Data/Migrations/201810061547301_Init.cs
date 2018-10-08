namespace PersonnelManager.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cadres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SalaireMensuel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        Nom = c.String(nullable: false, maxLength: 50),
                        DateEmbauche = c.DateTime(nullable: false),
                        Statut = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ouvriers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TauxHoraire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Prenom = c.String(nullable: false, maxLength: 50),
                        Nom = c.String(nullable: false, maxLength: 50),
                        DateEmbauche = c.DateTime(nullable: false),
                        Statut = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Periodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PremierJour = c.DateTime(nullable: false),
                        DernierJour = c.DateTime(nullable: false),
                        EstFermee = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelevesJours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdReleveMensuel = c.Int(nullable: false),
                        Jour = c.DateTime(nullable: false),
                        NombreHeures = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RelevesMensuels", t => t.IdReleveMensuel, cascadeDelete: true)
                .Index(t => t.IdReleveMensuel);
            
            CreateTable(
                "dbo.RelevesMensuels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPeriode = c.Int(nullable: false),
                        IdOuvrier = c.Int(nullable: false),
                        NombreTotalHeures = c.Int(nullable: false),
                        TauxHoraire = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ouvriers", t => t.IdOuvrier, cascadeDelete: true)
                .ForeignKey("dbo.Periodes", t => t.IdPeriode, cascadeDelete: true)
                .Index(t => t.IdPeriode)
                .Index(t => t.IdOuvrier);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelevesMensuels", "IdPeriode", "dbo.Periodes");
            DropForeignKey("dbo.RelevesMensuels", "IdOuvrier", "dbo.Ouvriers");
            DropForeignKey("dbo.RelevesJours", "IdReleveMensuel", "dbo.RelevesMensuels");
            DropIndex("dbo.RelevesMensuels", new[] { "IdOuvrier" });
            DropIndex("dbo.RelevesMensuels", new[] { "IdPeriode" });
            DropIndex("dbo.RelevesJours", new[] { "IdReleveMensuel" });
            DropTable("dbo.RelevesMensuels");
            DropTable("dbo.RelevesJours");
            DropTable("dbo.Periodes");
            DropTable("dbo.Ouvriers");
            DropTable("dbo.Cadres");
        }
    }
}
