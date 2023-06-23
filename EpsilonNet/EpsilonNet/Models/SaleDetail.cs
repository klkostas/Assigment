using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EpsilonNet.Models
{
    public class SaleDetail
    {
        [DisplayName("SaleDetail Id")]
        [Required(ErrorMessage = "SaleDetail Id is required")]
        public int SaleDetailId { get; set; }

        [DisplayName("Sale Id")]
        [Required(ErrorMessage = "Sale Id is required")]
        public int SaleId { get; set; }

        [DisplayName("Item Id")]
        [Required(ErrorMessage = "Item Id is required")]
        public int ItemId { get; set; }

        [DisplayName("Price")]
        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}
