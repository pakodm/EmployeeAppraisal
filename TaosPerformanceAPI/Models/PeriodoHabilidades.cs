using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("period_capabilities")]
    public class PeriodoHabilidades
    {
        public PeriodoHabilidades()
        {
            PeriodoCompetencias = new HashSet<PeriodoCompetencias>();
        }

        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("periodId"), Required]
        public int IdPeriodo { get; set; }

        [Column("title"), Required, StringLength(50)]
        public string Titulo { get; set; }

        [Column("shortCode"), Required, StringLength(6)]
        public string Identificador { get; set; }

        [Column("description"), StringLength(250)]
        public string Descripcion { get; set; }

        // Minimum acceptable score
        [Column("expectedScore")]
        public double PerfilEsperado { get; set; }

        [InverseProperty("PeriodoHabilidades")]
        public virtual ICollection<PeriodoCompetencias> PeriodoCompetencias { get; set; }
    }
}
