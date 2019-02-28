using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class Zona
    {
        public Int64 id { get; set; }
        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Vendosni Zonen !")]
        public string emri { get; set; }
    }
}
