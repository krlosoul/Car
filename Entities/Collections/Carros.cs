using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Interfases;

namespace Entities.Collections
{
    [Table("carros")]
    public class Carros : ICarros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [ForeignKey("Productos")]
        [Required]
        public string referenciaProducto { get; set; }
        [ForeignKey("Usuarios")]
        [Required]
        public int idUsuario { get; set; }

        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public int estado { get; set; }

        [ForeignKey("referenciaProducto")]
        public virtual Productos producto { get; set; }
        [ForeignKey("idUsuario")]
        public virtual Usuarios Usuario { get; set; }

    }
}
