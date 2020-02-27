using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("users")]
    public class Usuarios
    {
        [Column("id"), Key, Required, StringLength(10)]
        public string Id { get; set; }

        [Column("companyId"), Required]
        public int EmpresaUsuario { get; set; }

        [Column("userKey"), Required, StringLength(200)]
        public string ClaveUsuario { get; set; }

        [Column("userType")]
        [Required]
        public int RolUsuario { get; set; }

        [Column("loginAllowed")]
        [Required]
        public bool Activo { get; set; }

        [Column("lastLogin")]
        public DateTime? UltimoAcceso { get; set; }

        [Column("displayName")]
        [StringLength(200)]
        public string NombreCompleto { get; set; }

        [InverseProperty("Usuarios"), ForeignKey("EmpresaUsuario")]
        public virtual Empresas Empresas { get; set; }
    }
}
