using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UIUXLayer.Models;

namespace UIUXLayer.Controllers
{
    public class EmployeeController : Controller
    {

        public IActionResult dashBoard()
        {
            return View();
        }


        public async Task<IActionResult> viewEmployee()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            List<Employee>? employee = new List<Employee>();

            HttpResponseMessage res = await client.GetAsync("api/Employee");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<List<Employee>>(result);
            }
            return View(employee);

        }
        public async Task<IActionResult> Details(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            Employee? employee = new Employee();

            HttpResponseMessage res = await client.GetAsync($"api/Employee/get/{username}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(result);
            }
            return View(employee);

        }
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Employee emp)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            var postTask = client.PostAsJsonAsync("api/Employee/create", emp);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewEmployee");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string username)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7265");
            await client.DeleteAsync($"api/employee/delete/{username}");
            return RedirectToAction("ViewEmployee");
            //
        }
    }
}
