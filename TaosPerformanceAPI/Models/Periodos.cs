using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("evaluation_period")]
    public class Periodos
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("description"), Required, StringLength(150)]
        public string Descripcion { get; set; }

        [Column("startDate")]
        public DateTime FechaInicio { get; set; }

        [Column("endDate")]
        public DateTime FechaFin { get; set; }

        [InverseProperty("Periodos"), ForeignKey("IdEmpresa")]
        public virtual Empresas Empresas { get; set; }
    }
}
