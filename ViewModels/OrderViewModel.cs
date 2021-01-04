using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limak.az.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderLink { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal CargoPrice { get; set; }
        public decimal PriceResult { get; set; }
        public bool InlandOrNot { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int CountryId { get; set; }
        public int WarehouseId { get; set; }
    }
}
