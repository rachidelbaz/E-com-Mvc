using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Entities
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        [Display(Name ="First Name")]
        public string FisrtName { get; set; }
        [Required(ErrorMessage ="Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage ="Please enter a valid Email Adresse")]
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Adresse is required")]
        public string Adresse { get; set; }
        [Required(ErrorMessage ="Phone number is required")]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",ErrorMessage ="Please Enter a valid phone number")]
        public string Phone { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="PassWord is required")]
        public string PassWord { get; set; }
        [Compare("PassWord",ErrorMessage ="please confirm your PassWord")]
        [DataType(DataType.Password)]
        public string ConformePasseWord { get; set; }
    }
}
