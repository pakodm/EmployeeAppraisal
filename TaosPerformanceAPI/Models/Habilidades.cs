using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaosPerformanceAPI.Models
{
    [Table("capabilities")]
    public class Habilidades
    {
        public Habilidades()
        {
            Competencias = new HashSet<Competencias>();
        }

        [Column("id"), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("templateId"), Required]
        public int IdPlantilla { get; set; }

        [Column("title"), Required, StringLength(50)]
        public string Titulo { get; set; }

        [Column("shortCode"), Required, StringLength(6)]
        public string Identificador { get; set; }

        [Column("description"), StringLength(250)]
        public string Descripcion { get; set; }

        // Minimum acceptable score
        [Column("expectedScore")]
        public double PerfilEsperado { get; set; }

        [InverseProperty("Habilidades"), ForeignKey("IdPlantilla")]
        public virtual Plantillas Plantillas { get; set; }

        [InverseProperty("Habilidades")]
        public virtual ICollection<Competencias> Competencias { get; set; }
    }
}
