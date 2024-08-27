using AuthandAuthrizations.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthandAuthrizations.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStuddent _srvc;
        public StudentController(IStuddent srvc)
        {
            _srvc = srvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]int pagenumber=1,int pageSize = 10,string filter = "")
        {
            try
            {
                var data = await _srvc.GetStudents(pagenumber, pageSize,filter);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getstudent(int id)
        {
            try
            {
                var data = await _srvc.GetStudentbyId(id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
