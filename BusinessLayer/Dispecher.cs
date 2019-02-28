using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class Dispecher
    {

        public Int64 id { get; set; }

        [Display(Name = "Emri")]
        [Required(ErrorMessage = "Vendosni Emrin !")]
        public string emri { get; set; }

        [Display(Name = "Fjalekalimi")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vendosni Fjalekalimin !")]
        public string password { get; set; }

        [Display(Name = "Perserit")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Perserisni Fjalekalimin !")]
        [Compare("password", ErrorMessage = "Fjalekalimi nuk perkon me perseritjen !")]
        public string rePass { get; set; }


        [Display(Name = "Perdoruesi")]
        [Required(ErrorMessage = "Vendosni Perdoruesin !")]
        public string perdoruesi { get; set; }
     

     

    }
}
