using System.ComponentModel.DataAnnotations;
using System.Web;

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

        

        [Required(ErrorMessage = "Required")]
        public HttpPostedFileBase file { get; set; }

        public string filename { get; set; }

    }

    

}