using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
  public   class Skonto
    {
        public Int64 id { get; set; }

        [Display(Name = "Zona")]
        [Required(ErrorMessage = "Zgjidhni Zonen !")]
        public string emriZona { get; set; }

        [Display(Name = "Lloji i sherbimit ")]
        [Required(ErrorMessage = "Zgjidhni llojin e sherbmit !")]
        public string emriLloji { get; set; }

        [Display(Name = "Klienti")]
        [Required(ErrorMessage = "Zgjidhni klientin !")]
        public string emriKlienti { get; set; }

        public Int64 idZona { get; set; }

        public Int64 idLloji { get; set; }

        public Int64 idKlienti { get; set; }

       
        [Display(Name = "Skonto")]
        [Required(ErrorMessage = "Vendosni skonton !")]
        public decimal skonto { get; set; }
    }
}
