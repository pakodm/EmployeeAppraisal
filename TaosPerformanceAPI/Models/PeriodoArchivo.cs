using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("period_archive")]
    public class PeriodoArchivo
    {
        /*
         * This model seems to be against normalization rules but
         * seeks to preserve the specific skills and parameters
         * used during an evaluation period
        */
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("periodId"), Required]
        public int IdPeriodo { get; set; }

        [Column("capability_title"), StringLength(50)]
        public string Habilidad { get; set; }

        [Column("expectedCapabilityScore")]
        public double PerfilEsperadoHabilidad { get; set; }

        [Column("skill_title"), StringLength(100)]
        public string Competencia { get; set; }

        [Column("expectedSkillScore")]
        public double PerfilEsperadoCompetencia { get; set; }

        [Column("paramKey")]
        public int IdInterno { get; set; }

        [Column("paramDescription"), StringLength(250)]
        public string Descripcion { get; set; }

        [Column("paramOptions", TypeName = "text")]
        public string Opciones { get; set; }
    }
}
