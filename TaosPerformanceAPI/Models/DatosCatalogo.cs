using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("catalogs_data")]
    public class DatosCatalogo
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("idCatalog")]
        [Required]
        public int IdCatalogo { get; set; }

        [Column("companyId")]
        [Required]
        public int IdEmpresa { get; set; }

        [Column("data")]
        [StringLength(210)]
        public string Descripcion { get; set; }

        [InverseProperty("Datos"), ForeignKey("IdCatalogo, IdEmpresa")]
        public virtual Catalogos Catalogos { get; set; }
    }
}
