using Microsoft.AspNetCore.Mvc;
namespace ProjectClock.MVC.Services.Components
{
    public class ProjectCreator : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }

}
