using Microsoft.AspNetCore.Mvc;
using UIUXLayer.Models;

namespace UIUXLayer.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserLoginRequest loginDetails)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            var postTask = client.PostAsJsonAsync("api/User/login", loginDetails);
            postTask.Wait();
            var Result = postTask.Result;
            if (!Result.IsSuccessStatusCode)
            {
                return BadRequest("User wrong");
            }
            return RedirectToAction("dashBoard", "employee");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegisterRequest registerDetails)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            var postTask = client.PostAsJsonAsync("api/User/register", registerDetails);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return Ok();
            }
            return View();
        }


    }
}
