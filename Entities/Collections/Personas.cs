using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Interfases;

namespace Entities.Collections
{
    [Table("personas")]
    public class Personas : IPersonas
    {
        public Personas(){ }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [StringLength(45)]
        [Required]
        public string nombres { get; set; }

        [StringLength(45)]
        [Required]
        public string apellidos { get; set; }

        [JsonIgnore]
        public virtual Usuarios usuario { get; set; }

    }
}



