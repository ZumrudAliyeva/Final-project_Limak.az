using Limak.az.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Limak.az.ViewModels
{
    public class DeclarationViewModel
    {
        //public int Id { get; set; }
        [Required]
        public string Shop { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public string TrackingCode { get; set; }
        public string Link { get; set; }
        public DateTime DeclarationDate { get; set; }
        public string FileName { get; set; }
        [NotMapped]
        [Required]
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal ProductWeight { get; set; }
        public int DeclarationStatusId { get; set; }
        public int WarehouseId { get; set; }
        public string UserId { get; set; }
        public int CountryId { get; set; }
        //public Order Order { get; }
    }
}
