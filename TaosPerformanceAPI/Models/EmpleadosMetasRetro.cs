using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("employee_goals_feedback")]
    public class EmpleadosMetasRetro
    {
        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("goalId"), Required]
        public int IdMeta { get; set; }

        [Column("wroteBy"), Required, StringLength(10)]
        public string EscritoPor { get; set; }

        [Column("recordDate"), Required]
        public DateTime FechaRegistro { get; set; }

        [Column("feedback", TypeName = "text")]
        public string Retroalimentacion { get; set; }
    }
}
