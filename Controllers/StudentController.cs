using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZworks.Controllers
{
    // GET: api/<StudentController>
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // HTTP GET method to retrieve all students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students = { "John Doe", "Jane Smith", "Alice Johnson" };

            return Ok(students);
        }
    }
}
