using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;

namespace WebApplication2.Models 
{
    public class ContactModel
    {
        

        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$", ErrorMessage = "Must be valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Comments { get; set; }

    }

    

}