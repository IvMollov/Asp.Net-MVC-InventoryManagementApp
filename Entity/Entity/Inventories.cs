using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


namespace Entity.Entity
{
    [Table("InventoryManagement")]
    public class Inventories
    {
        [Key]
        public int InventoryID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public decimal SinglePrice { get; set; }

        public byte[] Picture { get; set; }
        public string Description { get; set; }
    }
}
