using Limak.az.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limak.az.ViewModels
{
    public class DeclarationViewModel
    {
        public int Id { get; set; }
        public string Shop { get; set; }
        public string ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
        public string TrackingCode { get; set; }
        public string Link { get; set; }
        public DateTime DeclarationDate { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal ProductWeight { get; set; }
        public int DeclarationStatusId { get; set; }
        public int WarehouseId { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        public Order Order { get; }
    }
}
