using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using TaosPerformanceAPI.Models;

namespace TaosPerformanceAPI.DAL
{
    public class MySQLContext : DbContext
    {
        public virtual DbSet<Catalogos> Catalogos { get; set; }
        public virtual DbSet<Competencias> Competencias { get; set; }
        public virtual DbSet<DatosCatalogo> DatosCatalogo { get; set; }
        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<EmpleadosMetas> EmpleadosMetas { get; set; }
        public virtual DbSet<EmpleadosMetasRetro> EmpleadosMetasRetro { get; set; }
        public virtual DbSet<Empresas> Empresas { get; set; }
        public virtual DbSet<Evaluaciones> Evaluaciones { get; set; }
        public virtual DbSet<EvaluacionRespuestas> EvaluacionRespuestas { get; set; }
        public virtual DbSet<Habilidades> Habilidades { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Periodos> Periodos { get; set; }
        public virtual DbSet<PeriodoArchivo> PeriodoArchivo { get; set; }
        public virtual DbSet<PeriodoCompetencias> PeriodoCompetencias { get; set; }
        public virtual DbSet<PeriodoEvaluacion> PeriodoEvaluacion { get; set; }
        public virtual DbSet<PeriodoHabilidades> PeriodoHabilidades { get; set; }
        public virtual DbSet<PeriodoParametros> PeriodoParametros { get; set; }       
        public virtual DbSet<Plantillas> Plantillas { get; set; }
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

            IConfiguration confg = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
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
            modelBuilder.Entity<Competencias>(entity => {
                entity.HasKey(e => new { e.Id, e.IdHabilidad }).HasName("PK_skills");
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
            modelBuilder.Entity<Evaluaciones>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_evaluation");
            });
            modelBuilder.Entity<EvaluacionRespuestas>(entity => {
                entity.HasKey(e => new { e.IdEvaluacion, e.IdInterno }).HasName("PK_evaluation_answers");
            });
            modelBuilder.Entity<Habilidades>();
            modelBuilder.Entity<Parametros>(entity => {
                entity.HasKey(e => new { e.Id, e.IdCompetencia }).HasName("PK_parameters");
            });
            modelBuilder.Entity<Periodos>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_period_details");
            });
            modelBuilder.Entity<PeriodoArchivo>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_period_archive");
            });
            modelBuilder.Entity<PeriodoCompetencias>(entity => {
                entity.HasKey(e => new { e.Id, e.IdHabilidad }).HasName("PK_period_skills");
            });
            modelBuilder.Entity<PeriodoEvaluacion>(entity => {
                entity.HasKey(e => new { e.Id, e.IdPeriodo, e.IdPlantilla }).HasName("PK_evaluation_periods");
            });
            modelBuilder.Entity<PeriodoHabilidades>();
            modelBuilder.Entity<PeriodoParametros>(entity => {
                entity.HasKey(e => new { e.Id, e.IdCompetencia }).HasName("PK_period_parameters");
            });
            modelBuilder.Entity<Plantillas>(entity => {
                entity.Property(a => a.EsBase).HasConversion<int>();
                entity.Property(a => a.Habilitada).HasConversion<int>();
                entity.Property(a => a.IdEmpresa).HasDefaultValue(0);
            });
            modelBuilder.Entity<Relaciones>();
            modelBuilder.Entity<TipoMeta>(entity => {
                entity.HasKey(e => new { e.Id, e.IdEmpresa }).HasName("PK_goal_type");
            });
            modelBuilder.Entity<Usuarios>().Property(a => a.Activo).HasConversion<int>();
        }
    }
}
