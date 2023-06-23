using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EpsilonNet.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        [DisplayName("Saledate")]
        [Required(ErrorMessage = "Sale date is required")]
        public DateTime SaleDate { get; set; }
        [DisplayName("CustomerId")]
        [Required(ErrorMessage = "CustomerId is required")]
        public int CustomerID { get; set; }
        public string Justification { get; set; }

    }
}
