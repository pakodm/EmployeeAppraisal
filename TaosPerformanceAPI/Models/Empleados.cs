using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("employees")]
    public class Empleados
    {
        [Column("id"), Key, StringLength(10), Required]
        public string Id { get; set; }

        [Column("companyId"), Required]
        public int EmpresaUsuario { get; set; }

        [Column("employeeName"), StringLength(60), Required]
        public string Nombre { get; set; }

        [Column("fatherLastName"), StringLength(80), Required]
        public string ApellidoPaterno { get; set; }

        [Column("motherLastName"), StringLength(80)]
        public string ApellidoMaterno { get; set; }

        [Column("birthDate")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("socialSecurityNumber"), StringLength(15)]
        public string NumeroSeguroSocial { get; set; }

        [Column("taxId"), StringLength(15)]
        public string RFC { get; set; }

        [Column("curp"), StringLength(20)]
        public string CURP { get; set; }

        [Column("payrollId"), StringLength(50)]
        public string NumeroNomina { get; set; }

        [Column("leaderId"), StringLength(10)]
        public string IdLider { get; set; }

        [Column("departmentId")]
        public int IdDepartamento { get; set; }

        [Column("jobTypeId")]
        public int IdPuesto { get; set; }

        [Column("employmentStartDate")]
        public DateTime FechaIngreso { get; set; }

        [Column("employedSinceDate")]
        public DateTime FechaAntiguedad { get; set; }

        [Column("contractType")]
        public int IdContrato { get; set; }

        [Column("contractStartDate")]
        public DateTime? FechaInicioContrato { get; set; }

        [Column("contractEndDate")]
        public DateTime? FechaFinContrato { get; set; }

        [Column("email"), StringLength(150)]
        public string Email { get; set; }

        [Column("enabled"), Required]
        public bool Activo { get; set; }

        [InverseProperty("Empleados"), ForeignKey("EmpresaUsuario")]
        public virtual Empresas Empresas { get; set; }

        public string GetNombreCompleto()
        {
            var sbNombreCompleto = new StringBuilder();
            sbNombreCompleto.AppendFormat("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
            return sbNombreCompleto.ToString();
        }
    }
}
