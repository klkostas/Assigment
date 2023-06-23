using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EpsilonNet.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        [DisplayName("Purchase date")]
        [Required(ErrorMessage = "Purchase date is required")]
        public DateTime DatePurchase { get; set; }
        [DisplayName("SupplierId")]
        [Required(ErrorMessage = "SupplierId is required")]
        public int SupplierID { get; set; }
        public string Justification { get; set; }
    }
}
