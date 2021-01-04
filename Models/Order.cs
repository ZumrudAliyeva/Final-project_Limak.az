using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limak.az.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string OrderLink { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public decimal CargoPrice { get; set; }
        public decimal PriceResult { get; set; }
        [Required]
        public bool InlandOrNot { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int CountryId { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public virtual CustomAppUser User { get; set; }
        public virtual Country Country { get; set; }
        public virtual OrderStatus Status { get; set; }
    }
}
