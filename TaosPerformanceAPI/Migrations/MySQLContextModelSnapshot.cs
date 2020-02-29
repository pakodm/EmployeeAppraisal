﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaosPerformanceAPI.DAL;

namespace TaosPerformanceAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    partial class MySQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113");

            modelBuilder.Entity("TaosPerformanceAPI.Models.Catalogos", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("id");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("NombreCatalogo")
                        .IsRequired()
                        .HasColumnName("catalogName")
                        .HasMaxLength(150);

                    b.HasKey("Id", "IdEmpresa")
                        .HasName("PK_catalogs");

                    b.ToTable("catalogs");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Competencias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdHabilidad")
                        .HasColumnName("capabilityId");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<double>("PerfilEsperado")
                        .HasColumnName("expectedScore");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(100);

                    b.HasKey("Id", "IdHabilidad")
                        .HasName("PK_skills");

                    b.HasIndex("IdHabilidad");

                    b.ToTable("skills");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.DatosCatalogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdCatalogo")
                        .HasColumnName("idCatalog");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("Descripcion")
                        .HasColumnName("data")
                        .HasMaxLength(210);

                    b.HasKey("Id", "IdCatalogo", "IdEmpresa")
                        .HasName("PK_catalogs_data");

                    b.HasIndex("IdCatalogo", "IdEmpresa");

                    b.ToTable("catalogs_data");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Empleados", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasMaxLength(10);

                    b.Property<int>("EmpresaUsuario")
                        .HasColumnName("companyId");

                    b.Property<int>("Activo")
                        .HasColumnName("enabled");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnName("motherLastName")
                        .HasMaxLength(80);

                    b.Property<string>("ApellidoPaterno")
                        .IsRequired()
                        .HasColumnName("fatherLastName")
                        .HasMaxLength(80);

                    b.Property<string>("CURP")
                        .HasColumnName("curp")
                        .HasMaxLength(20);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasMaxLength(150);

                    b.Property<DateTime>("FechaAntiguedad")
                        .HasColumnName("employedSinceDate");

                    b.Property<DateTime?>("FechaFinContrato")
                        .HasColumnName("contractEndDate");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnName("employmentStartDate");

                    b.Property<DateTime?>("FechaInicioContrato")
                        .HasColumnName("contractStartDate");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnName("birthDate");

                    b.Property<int>("IdContrato")
                        .HasColumnName("contractType");

                    b.Property<int>("IdDepartamento")
                        .HasColumnName("departmentId");

                    b.Property<string>("IdLider")
                        .HasColumnName("leaderId")
                        .HasMaxLength(10);

                    b.Property<int>("IdPuesto")
                        .HasColumnName("jobTypeId");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnName("employeeName")
                        .HasMaxLength(60);

                    b.Property<string>("NumeroNomina")
                        .HasColumnName("payrollId")
                        .HasMaxLength(50);

                    b.Property<string>("NumeroSeguroSocial")
                        .HasColumnName("socialSecurityNumber")
                        .HasMaxLength(15);

                    b.Property<string>("RFC")
                        .HasColumnName("taxId")
                        .HasMaxLength(15);

                    b.HasKey("Id", "EmpresaUsuario")
                        .HasName("PK_employees");

                    b.HasIndex("EmpresaUsuario");

                    b.ToTable("employees");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.EmpleadosMetas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdPeriodo")
                        .HasColumnName("periodId");

                    b.Property<string>("IdEmpleado")
                        .HasColumnName("employeeId")
                        .HasMaxLength(10);

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaLimite")
                        .HasColumnName("dueDate");

                    b.Property<int>("IdTipoMeta")
                        .HasColumnName("goalType");

                    b.Property<string>("Objetivo")
                        .HasColumnName("goal")
                        .HasMaxLength(200);

                    b.Property<double>("PorcentajeValor")
                        .HasColumnName("weight");

                    b.Property<double>("ProgresoCompletado")
                        .HasColumnName("progressMade");

                    b.HasKey("Id", "IdPeriodo", "IdEmpleado")
                        .HasName("PK_employees");

                    b.HasIndex("IdEmpleado");

                    b.ToTable("employee_goals");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.EmpleadosMetasRetro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("EscritoPor")
                        .IsRequired()
                        .HasColumnName("wroteBy")
                        .HasMaxLength(10);

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnName("recordDate");

                    b.Property<int>("IdMeta")
                        .HasColumnName("goalId");

                    b.Property<string>("Retroalimentacion")
                        .HasColumnName("feedback")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("employee_goals_feedback");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Empresas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("DomicilioFiscal")
                        .HasColumnName("legalAddress")
                        .HasMaxLength(250);

                    b.Property<string>("NombreEmpresa")
                        .IsRequired()
                        .HasColumnName("companyName")
                        .HasMaxLength(210);

                    b.Property<int>("NumeroEvaluaciones")
                        .HasColumnName("purchasedLicenses");

                    b.Property<int>("PrefijoInicio")
                        .HasColumnName("startPrefix");

                    b.Property<int>("PrefijoTermina")
                        .HasColumnName("endPrefix");

                    b.Property<string>("PrefijoUsuario")
                        .HasColumnName("userPrefix")
                        .HasMaxLength(3);

                    b.Property<string>("RFC")
                        .HasColumnName("taxId")
                        .HasMaxLength(15);

                    b.Property<string>("RegistroSTPS")
                        .HasColumnName("laborCode")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("company");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Evaluaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("ComentariosEmpleado")
                        .HasColumnName("employeeComments")
                        .HasColumnType("text");

                    b.Property<string>("ComentariosLider")
                        .HasColumnName("leaderComments")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaEvaluacion")
                        .HasColumnName("evaluationDate");

                    b.Property<string>("IdEmpleado")
                        .IsRequired()
                        .HasColumnName("employeeId")
                        .HasMaxLength(10);

                    b.Property<string>("IdLider")
                        .IsRequired()
                        .HasColumnName("leaderId")
                        .HasMaxLength(10);

                    b.Property<int>("IdPeriodo")
                        .HasColumnName("periodId");

                    b.Property<double>("ResultadoGeneral")
                        .HasColumnName("final_score");

                    b.Property<double>("ResultadoHabilidades")
                        .HasColumnName("skills_score");

                    b.Property<double>("ResultadoMetas")
                        .HasColumnName("goals_score");

                    b.HasKey("Id", "IdEmpresa")
                        .HasName("PK_evaluation");

                    b.ToTable("evaluations");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.EvaluacionRespuestas", b =>
                {
                    b.Property<int>("IdEvaluacion")
                        .HasColumnName("evaluationId");

                    b.Property<int>("IdInterno")
                        .HasColumnName("itemKey");

                    b.Property<string>("Comentarios")
                        .HasColumnName("feedback")
                        .HasColumnType("text");

                    b.Property<int>("IdCompetencia")
                        .HasColumnName("skillId");

                    b.Property<int>("IdHabilidad")
                        .HasColumnName("capabilityId");

                    b.Property<int>("IdPlantilla")
                        .HasColumnName("templateId");

                    b.Property<string>("ValorRespuesta")
                        .HasColumnName("answerValue")
                        .HasMaxLength(50);

                    b.HasKey("IdEvaluacion", "IdInterno")
                        .HasName("PK_evaluation_answers");

                    b.ToTable("evaluation_answers");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Habilidades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<int>("IdPlantilla")
                        .HasColumnName("templateId");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnName("shortCode")
                        .HasMaxLength(6);

                    b.Property<double>("PerfilEsperado")
                        .HasColumnName("expectedScore");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IdPlantilla");

                    b.ToTable("capabilities");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Parametros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdCompetencia")
                        .HasColumnName("skillId");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<bool>("Habilitada")
                        .HasColumnName("enabled");

                    b.Property<int>("IdInterno")
                        .HasColumnName("itemKey");

                    b.Property<string>("Opciones")
                        .HasColumnName("options")
                        .HasColumnType("text");

                    b.Property<int>("Posicion")
                        .HasColumnName("itemPosition");

                    b.Property<int>("TipoPregunta")
                        .HasColumnName("parameterType")
                        .HasMaxLength(20);

                    b.HasKey("Id", "IdCompetencia")
                        .HasName("PK_parameters");

                    b.HasIndex("IdCompetencia");

                    b.ToTable("parameters");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoArchivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("Competencia")
                        .HasColumnName("skill_title")
                        .HasMaxLength(100);

                    b.Property<string>("Descripcion")
                        .HasColumnName("paramDescription")
                        .HasMaxLength(250);

                    b.Property<string>("Habilidad")
                        .HasColumnName("capability_title")
                        .HasMaxLength(50);

                    b.Property<int>("IdInterno")
                        .HasColumnName("paramKey");

                    b.Property<int>("IdPeriodo")
                        .HasColumnName("periodId");

                    b.Property<string>("Opciones")
                        .HasColumnName("paramOptions")
                        .HasColumnType("text");

                    b.Property<double>("PerfilEsperadoCompetencia")
                        .HasColumnName("expectedSkillScore");

                    b.Property<double>("PerfilEsperadoHabilidad")
                        .HasColumnName("expectedCapabilityScore");

                    b.HasKey("Id", "IdEmpresa")
                        .HasName("PK_period_archive");

                    b.ToTable("period_archive");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoCompetencias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdHabilidad")
                        .HasColumnName("capabilityId");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<double>("PerfilEsperado")
                        .HasColumnName("expectedScore");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(100);

                    b.HasKey("Id", "IdHabilidad")
                        .HasName("PK_period_skills");

                    b.HasIndex("IdHabilidad");

                    b.ToTable("period_skills");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoEvaluacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdPeriodo")
                        .HasColumnName("periodId");

                    b.Property<int>("IdPlantilla")
                        .HasColumnName("templateId");

                    b.Property<string>("NombrePlantilla")
                        .HasColumnName("templateName")
                        .HasMaxLength(200);

                    b.HasKey("Id", "IdPeriodo", "IdPlantilla")
                        .HasName("PK_evaluation_periods");

                    b.ToTable("evaluation_period");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoHabilidades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<int>("IdPeriodo")
                        .HasColumnName("periodId");

                    b.Property<string>("Identificador")
                        .IsRequired()
                        .HasColumnName("shortCode")
                        .HasMaxLength(6);

                    b.Property<double>("PerfilEsperado")
                        .HasColumnName("expectedScore");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("period_capabilities");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoParametros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdCompetencia")
                        .HasColumnName("skillId");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(250);

                    b.Property<bool>("Habilitada")
                        .HasColumnName("enabled");

                    b.Property<int>("IdInterno")
                        .HasColumnName("itemKey");

                    b.Property<string>("Opciones")
                        .HasColumnName("options")
                        .HasColumnType("text");

                    b.Property<int>("Posicion")
                        .HasColumnName("itemPosition");

                    b.Property<int>("TipoPregunta")
                        .HasColumnName("parameterType")
                        .HasMaxLength(20);

                    b.HasKey("Id", "IdCompetencia")
                        .HasName("PK_period_parameters");

                    b.HasIndex("IdCompetencia");

                    b.ToTable("period_parameters");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Periodos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasMaxLength(150);

                    b.Property<DateTime>("FechaFin")
                        .HasColumnName("endDate");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnName("startDate");

                    b.HasKey("Id", "IdEmpresa")
                        .HasName("PK_period_details");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("period_details");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Plantillas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasMaxLength(200);

                    b.Property<int>("EsBase")
                        .HasColumnName("base_template");

                    b.Property<int>("Habilitada")
                        .HasColumnName("enabled");

                    b.Property<int>("IdEmpresa")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("companyId")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("templates");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Relaciones", b =>
                {
                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<string>("IdLider")
                        .HasColumnName("leaderId")
                        .HasMaxLength(10);

                    b.Property<string>("IdEmpleado")
                        .HasColumnName("employeeId")
                        .HasMaxLength(10);

                    b.HasKey("IdEmpresa", "IdLider", "IdEmpleado");

                    b.ToTable("employeeLinks");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.TipoMeta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("IdEmpresa")
                        .HasColumnName("companyId");

                    b.Property<bool>("Activo")
                        .HasColumnName("enabled");

                    b.Property<string>("Descripcion")
                        .HasColumnName("description")
                        .HasMaxLength(150);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(80);

                    b.HasKey("Id", "IdEmpresa")
                        .HasName("PK_goal_type");

                    b.ToTable("goal_type");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Usuarios", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasMaxLength(10);

                    b.Property<int>("Activo")
                        .HasColumnName("loginAllowed");

                    b.Property<string>("ClaveUsuario")
                        .IsRequired()
                        .HasColumnName("userKey")
                        .HasMaxLength(200);

                    b.Property<int>("EmpresaUsuario")
                        .HasColumnName("companyId");

                    b.Property<string>("NombreCompleto")
                        .HasColumnName("displayName")
                        .HasMaxLength(200);

                    b.Property<int>("RolUsuario")
                        .HasColumnName("userType");

                    b.Property<DateTime?>("UltimoAcceso")
                        .HasColumnName("lastLogin");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaUsuario");

                    b.ToTable("users");
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Competencias", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Habilidades", "Habilidades")
                        .WithMany("Competencias")
                        .HasForeignKey("IdHabilidad")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.DatosCatalogo", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Catalogos", "Catalogos")
                        .WithMany("Datos")
                        .HasForeignKey("IdCatalogo", "IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Empleados", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Empresas", "Empresas")
                        .WithMany("Empleados")
                        .HasForeignKey("EmpresaUsuario")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.EmpleadosMetas", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Empleados", "Empleados")
                        .WithMany("EmpleadosMetas")
                        .HasForeignKey("IdEmpleado")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Habilidades", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Plantillas", "Plantillas")
                        .WithMany("Habilidades")
                        .HasForeignKey("IdPlantilla")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Parametros", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Competencias", "Competencias")
                        .WithMany("Parametros")
                        .HasForeignKey("IdCompetencia")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoCompetencias", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.PeriodoHabilidades", "PeriodoHabilidades")
                        .WithMany("PeriodoCompetencias")
                        .HasForeignKey("IdHabilidad")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.PeriodoParametros", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.PeriodoCompetencias", "PeriodoCompetencias")
                        .WithMany("PeriodoParametros")
                        .HasForeignKey("IdCompetencia")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Periodos", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Empresas", "Empresas")
                        .WithMany("Periodos")
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TaosPerformanceAPI.Models.Usuarios", b =>
                {
                    b.HasOne("TaosPerformanceAPI.Models.Empresas", "Empresas")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaUsuario")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
