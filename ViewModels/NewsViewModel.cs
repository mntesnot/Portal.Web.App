using Portal.Web.App.Models;

namespace Portal.Web.App.ViewModels
{
    public class NewsViewModel
    {
        public News News { get; set; }
        public IEnumerable<News> NewsList { get; set; }
    }
}
