using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EpsilonNet.Models
{
   public class Customer
    {
        public int Customerid { get; set; }
        [DisplayName("Customer Name")]
        [Required(ErrorMessage ="Customer name is required")]
        [StringLength(250,MinimumLength =5, ErrorMessage ="Customer name must be between 5 and 250 characters")]
        public string Name { get; set; }

        [DisplayName("Customer Surname")]
        [Required(ErrorMessage = "Customer surname is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Customer surname must be between 5 and 250 characters")]
        public string SurName { get; set; }

        [DisplayName("Customer Tin")]
        [Required(ErrorMessage = "Customer Tin is required")]
        [StringLength(9, MinimumLength =9, ErrorMessage = "Customer Tin must be 9 characters")]
        [RegularExpression(@"\d+", ErrorMessage = "Invalid Tin format.")]
        public string Tin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
       
    }
}
