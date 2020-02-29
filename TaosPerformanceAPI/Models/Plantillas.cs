using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("templates")]
    public class Plantillas
    {
        public Plantillas()
        {
            Habilidades = new HashSet<Habilidades>();
        }

        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("base_template"), Required]
        public bool EsBase { get; set; }

        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("description"), Required, StringLength(200)]
        public string Descripcion { get; set; }

        [Column("enabled"), Required]
        public bool Habilitada { get; set; }

        [InverseProperty("Plantilas")]
        public virtual ICollection<Habilidades> Habilidades { get; set; }
    }
}
