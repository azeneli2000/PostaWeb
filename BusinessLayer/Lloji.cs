using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public  class Lloji
    {
        public Int64 id { get; set; }
        [Display(Name = "Lloji i sherbimit")]
        [Required(ErrorMessage = "Vendosni Llojin e sherbimit !")]
        public string emri { get; set; }
    }
}
