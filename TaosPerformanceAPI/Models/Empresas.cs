using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("company")]
    public class Empresas
    {
        public Empresas()
        {
            Empleados = new HashSet<Empleados>();
            Usuarios = new HashSet<Usuarios>();
            Periodos = new HashSet<Periodos>();
        }

        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }

        [Column("companyName"), StringLength(210), Required]
        public string NombreEmpresa { get; set; }

        [Column("legalAddress")]
        [StringLength(250)]
        public string DomicilioFiscal { get; set; }

        [Column("taxId")]
        [StringLength(15)]
        public string RFC { get; set; }

        [Column("laborCode")]
        [StringLength(30)]
        public string RegistroSTPS { get; set; }

        [Column("userPrefix")]
        [StringLength(3)]
        public string PrefijoUsuario { get; set; }

        [Column("startPrefix")]
        public int PrefijoInicio { get; set; }

        [Column("endPrefix")]
        public int PrefijoTermina { get; set; }

        [Column("purchasedLicenses")]
        [Required]
        public int NumeroEvaluaciones { get; set; }

        [InverseProperty("Empresas")]
        public virtual ICollection<Empleados> Empleados { get; set; }

        [InverseProperty("Empresas")]
        public virtual ICollection<Usuarios> Usuarios { get; set; }

        [InverseProperty("Empresas")]
        public virtual ICollection<Periodos> Periodos { get; set; }
    }
}
