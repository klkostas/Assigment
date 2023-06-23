using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EpsilonNet.Models
{
    public class Item
    {
        public int Itemid { get; set; }

        [DisplayName("Item description")]
        [Required(ErrorMessage = "Item description is required")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Item description must be between 3 and 250 characters")]
        public string Description { get; set; }

        [DisplayName("Item SupplierId")]
        [Required(ErrorMessage = "SupplierId is required")]
        public int SupplierId { get; set; }
        public float DefaultPrice { get; set; }
        public float AlarmPrice { get; set; }
    }
}
