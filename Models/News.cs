using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Portal.Web.App.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }
        [Display(Name ="News Title")]
        [Required]
        public string Title { get; set; }
        [Display(Name ="Content")]
        public string Body { get; set; }
        [Display(Name = "Image")]
        public string? Image { get; set; } = "Defoult.png";
        [Display(Name = "Posted Date")]
        public DateTime PostDate { get; set; }= DateTime.Now;
        [Display(Name = "Posted By")]
        public string PostedBy { get; set; }
    }
}
