using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("parameters")]
    public class Parametros
    {
        [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("skillId"), Required]
        public int IdCompetencia { get; set; }

        [Column("itemKey"), Required]
        public int IdInterno { get; set; }

        [Column("itemPosition")]
        public int Posicion { get; set; }

        [Column("parameterType"), Required, StringLength(20)]
        public int TipoPregunta { get; set; }

        [Column("description"), StringLength(250)]
        public string Descripcion { get; set; }

        [Column("enabled"), Required]
        public bool Habilitada { get; set; }

        [Column("options", TypeName = "text")]
        public string Opciones { get; set; }

        [InverseProperty("Parametros"), ForeignKey("IdCompetencia")]
        public virtual Competencias Competencias { get; set; }
    }
}
