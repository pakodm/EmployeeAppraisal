using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("evaluation_period")]
    public class PeriodoEvaluacion
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("periodId"), Required]
        public int IdPeriodo { get; set; }

        [Column("templateId"), Required]
        public int IdPlantilla { get; set; }

        [Column("templateName"), StringLength(200)]
        public string NombrePlantilla { get; set; }
    }
}
