using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using TaosPerformanceAPI.Models;

namespace TaosPerformanceAPI.DAL
{
    public class MySQLContext : DbContext
    {
        public virtual DbSet<Catalogos> Catalogos { get; set; }
        public virtual DbSet<DatosCatalogo> DatosCatalogo { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<EmpleadosMetas> EmpleadosMetas { get; set; }
        public virtual DbSet<EmpleadosMetasRetro> EmpleadosMetasRetro { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Periodos> Periodos { get; set; }
        public virtual DbSet<Relaciones> Relaciones { get; set; }
        public virtual DbSet<TipoMeta> TipoMeta { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }
        public MySQLContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                return;
            }

            IConfiguration confg = new ConfigurationBuilder().AddJsonFile("appsetting.json").Build();
            String cnxString = confg.GetValue<String>("dbInUse");
            if (!String.IsNullOrEmpty(cnxString))
            {
                optionsBuilder.UseMySQL(confg.GetConnectionString(cnxString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            modelBuilder.Entity<Catalogos>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_catalogs");
            });
            modelBuilder.Entity<DatosCatalogo>(entity => {
                entity.HasKey(e => new { e.Id, e.IdCatalogo, e.IdEmpresa }).HasName("PK_catalogs_data");
            });
            modelBuilder.Entity<Empleados>(entity => {
                entity.HasKey(e => new { e.Id, e.EmpresaUsuario }).HasName("PK_employees");
                entity.Property(a => a.Activo).HasConversion<int>();
            });
            modelBuilder.Entity<EmpleadosMetas>(entity => {
                entity.HasKey(e => new { e.Id, e.IdPeriodo, e.IdEmpleado }).HasName("PK_employees");
                
            });
            modelBuilder.Entity<EmpleadosMetasRetro>();
            modelBuilder.Entity<Empresas>();
            modelBuilder.Entity<Periodos>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_evaluation_periods");
            });
            modelBuilder.Entity<Relaciones>();
            modelBuilder.Entity<TipoMeta>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_goal_type");
            });
            modelBuilder.Entity<Usuarios>().Property(a => a.Activo).HasConversion<int>();
        }
    }
}
