using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Inventory.DataAccess.Entities
{
    [Table("InventoryManagement")]
    public class InventoriesManagement
    {
        [Key]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Size { get; set; }
        
        [Required(ErrorMessage = "This field is required")]
        [UIHint("Currency")]
        public decimal SinglePrice { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public string Base64Image { get; set; }
    }
}
