using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace APILayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return _employeeServices.Get();
        }
        [HttpGet]
        [Route("get/{username}")]
        public ActionResult<Employee> Get(string username)
        {
            return _employeeServices.Get(username);
        }
        [HttpDelete]
        [Route("delete/{username}")]

        public ActionResult Delete(string username)
        {
            _employeeServices.delete(username);
            return Ok();
        }
        [HttpPost]
        [Route("create")]
        public IActionResult Create([FromBody] Employee employeeDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest("not a valid request");
            _employeeServices.create(employeeDetail);
            return Ok();
        }


    }
}
