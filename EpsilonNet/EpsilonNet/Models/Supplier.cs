using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpsilonNet.Models
{
    public class Supplier
    {
        public int Supplierid { get; set; }
        [DisplayName("Supplier Name")]
        [Required(ErrorMessage = "Supplier name is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Supplier name must be between 5 and 250 characters")]
        public string Name { get; set; }

        [DisplayName("Supplier Surname")]
        [Required(ErrorMessage = "Supplier surname is required")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Supplier surname must be between 5 and 250 characters")]
        public string SurName { get; set; }

        [DisplayName("Supplier Tin")]
        [Required(ErrorMessage = "Supplier Tin is required")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Supplier Tin must be 9 characters")]
        //[RegularExpression(@"")]
        public string Tin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
    }
}
