using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("evaluation_answers")]
    public class EvaluacionRespuestas
    {
        [Column("evaluationId"), Required]
        public int IdEvaluacion { get; set; }

        [Column("templateId"), Required]
        public int IdPlantilla { get; set; }

        [Column("itemKey"), Required]
        public int IdInterno { get; set; }

        [Column("capabilityId")]
        public int IdHabilidad { get; set; }

        [Column("skillId")]
        public int IdCompetencia { get; set; }

        [Column("answerValue"), StringLength(50)]
        public string ValorRespuesta { get; set; }

        [Column("feedback", TypeName = "text")]
        public string Comentarios { get; set; }
    }
}
