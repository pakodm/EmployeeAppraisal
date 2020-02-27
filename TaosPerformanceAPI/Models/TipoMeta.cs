using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("goal_type")]
    public class TipoMeta
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("title"), Required, StringLength(80)]
        public string Titulo { get; set; }

        [Column("description"), StringLength(150)]
        public string Descripcion { get; set; }

        [Column("enabled"), Required]
        public bool Activo { get; set; }
    }
}
