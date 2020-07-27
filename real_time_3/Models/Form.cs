using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace real_time_3.Models
{
    public class Form
    {

        [Required]
        public string Name{ set; get; }

        [Required]
        public string Surname { set; get; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Text { get; set; }

    }
}
