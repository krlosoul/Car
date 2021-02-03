using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Interfases;

namespace Entities.Collections
{
    [Table("productos")]
    public class Productos : IProductos
    {
        public Productos(){}

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string referencia { get; set; }

        [StringLength(45)]
        [Required]
        public string descripcion { get; set; }
        [Required]
        public decimal valor { get; set; }

        [JsonIgnore]
        public virtual List<Carros> carros { get; set; }
    }


}
