using Microsoft.AspNetCore.Mvc;
namespace ProjectClock.MVC.ViewComponents
{
    public class ProjectCreator : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }

}
