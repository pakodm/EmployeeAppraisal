using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("skills")]
    public class Competencias
    {
        public Competencias()
        {
            Parametros = new HashSet<Parametros>();
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

        [InverseProperty("Competencias")]
        public virtual ICollection<Parametros> Parametros { get; set; }

        [InverseProperty("Competencias"), ForeignKey("IdHabilidad")]
        public virtual Habilidades Habilidades { get; set; }
    }
}
