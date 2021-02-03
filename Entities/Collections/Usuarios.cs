using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Interfases;

namespace Entities.Collections
{
    [Table("usuarios")]
    public class Usuarios : IUsuarios
    {
        public Usuarios(){ }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Required]
        public int idPersona { get; set; }

        [StringLength(45)]
        [Required]
        public string usuario { get; set; }
        [StringLength(45)]
        [Required]
        public string clave { get; set; }

        public virtual Personas persona { get; set; }
        [JsonIgnore]
        public virtual List<Carros> carros { get; set; }

    }
}