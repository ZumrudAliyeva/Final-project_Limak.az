using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_project_Limak.az.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string OrderLink { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public int CustomAppUserId { get; set; }
        public CustomAppUser Customer { get; set; }
    }
}
