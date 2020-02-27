using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("catalogs")]
    public class Catalogos
    {
        public Catalogos()
        {
            Datos = new HashSet<DatosCatalogo>();
        }

        [Column("id")]
        [Required]
        public int Id { get; set; }

        [Column("companyId")]
        [Required]
        public int IdEmpresa { get; set; }

        [Column("catalogName")]
        [Required]
        [StringLength(150)]
        public string NombreCatalogo { get; set; }

        [InverseProperty("Catalogos")]
        public virtual ICollection<DatosCatalogo> Datos { get; set; }

    }
}
