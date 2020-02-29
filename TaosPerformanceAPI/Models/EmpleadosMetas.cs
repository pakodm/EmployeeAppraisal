using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("employee_goals")]
    public class EmpleadosMetas
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("employeeId"), Required, StringLength(10)]
        public string IdEmpleado { get; set; }

        [Column("periodId"), Required]
        public int IdPeriodo { get; set; }

        [Column("goalType"), Required]
        public int IdTipoMeta { get; set; }

        [Column("goal"), StringLength(200)]
        public string Objetivo { get; set; }

        [Column("description", TypeName = "text")]
        public string Descripcion { get; set; }

        [Column("dueDate")]
        public DateTime FechaLimite { get; set; }

        [Column("weight")]
        public Double PorcentajeValor { get; set; }

        [Column("progressMade")]
        public Double ProgresoCompletado { get; set; }

        [InverseProperty("EmpleadosMetas"), ForeignKey("IdEmpleado")]
        public virtual Empleados Empleados { get; set; }
    }
}
