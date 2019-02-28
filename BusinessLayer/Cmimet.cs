using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
 public   class Cmimet
    {
        public Int64 id { get; set; }
        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Zgjidhni Zonen !")]
        public string emriZona { get; set; }

        [Display(Name = "Lloji i sherbimit ")]
        [Required(ErrorMessage = "Zgjidhni llojin e sherbmit !")]
        public string emriLloji { get; set; }

        public Int64 idZona { get; set; }
        public Int64 idLloji { get; set; }
        [Display(Name = "Valuta")]
        [Required(ErrorMessage = "Zgjidhni Valuten !")]
        public string valuta { get; set; }

        [Display(Name = "Cmimi")]
        [Required(ErrorMessage = "Vendosni cmimin !")]
        public decimal cmimi { get; set; }
    }
}
