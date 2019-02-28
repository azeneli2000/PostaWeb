using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public  class Magazina
    {
        public Int64 id { get; set; }
        [Display(Name = "Magazina")]
     
        public string emri { get; set; }
    }
}
