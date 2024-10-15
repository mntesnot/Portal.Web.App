using System.ComponentModel.DataAnnotations;

namespace Portal.Web.App.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        [Display(Name = "User Name")]
        [DataType(DataType.Text)]
        [Required]
        public string Username { get; set; }
        [Display(Name ="User Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
