using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("period_skills")]
    public class PeriodoCompetencias
    {
        public PeriodoCompetencias()
        {
            Parametros = new HashSet<PeriodoParametros>();
        }

        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("capabilityId"), Required]
        public int IdHabilidad { get; set; }

        [Column("title"), Required, StringLength(100)]
        public string Titulo { get; set; }

        [Column("description"), StringLength(250)]
        public string Descripcion { get; set; }

        // Minimum acceptable score
        [Column("expectedScore")]
        public double PerfilEsperado { get; set; }

        [InverseProperty("PeriodoCompetencias")]
        public virtual ICollection<PeriodoParametros> Parametros { get; set; }

        [InverseProperty("PeriodoCompetencias"), ForeignKey("IdHabilidad")]
        public virtual PeriodoHabilidades Habilidades { get; set; }
    }
}
