using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("employeeLinks")]
    public class Relaciones
    {
        [Column("companyId"), Required]
        public int IdEmpresa { get; set; }

        [Column("leaderId"), StringLength(10)]
        public string IdLider { get; set; }

        [Column("employeeId"), StringLength(10)]
        public string IdEmpleado { get; set; }
    }
}
