using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("evaluations")]
    public class Evaluaciones
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("periodId"), Required]
        public int IdPeriodo { get; set; }

        [Column("employeeId"), Required, StringLength(10)]
        public string IdEmpleado { get; set; }

        [Column("leaderId"), Required, StringLength(10)]
        public string IdLider { get; set; }

        [Column("evaluationDate"), Required]
        public DateTime FechaEvaluacion { get; set; }

        [Column("leaderComments", TypeName = "text")]
        public string ComentariosLider { get; set; }

        [Column("employeeComments", TypeName = "text")]
        public string ComentariosEmpleado { get; set; }

        [Column("goals_score")]
        public double ResultadoMetas { get; set; }

        [Column("skills_score")]
        public double ResultadoHabilidades { get; set; }

        [Column("final_score")]
        public double ResultadoGeneral { get; set; }
    }
}
