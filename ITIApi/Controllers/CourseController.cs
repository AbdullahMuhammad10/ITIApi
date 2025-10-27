using ITIApi.DAL.Data.Context;
using ITIApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITIApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApiContext context;

        public CourseController(ApiContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var courses = context.Courses.ToList();
            if (courses != null) { return Ok(courses); }
            return NotFound();
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var course = context.Courses.Find(id);
            if (course != null) { return Ok(course); }
            return NotFound();
        }
        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var course = context.Courses.FirstOrDefault(C => C.Name == name);
            if (course != null) { return Ok(course); }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            var course = context.Courses.Find(id);
            if (course != null)
            {
                context.Courses.Remove(course);
                context.SaveChanges();
                var courses = context.Courses.ToList();
                return Ok(courses);
            }
            return NotFound();
        }
        [HttpPut]

        public IActionResult Put(int id, Course course)
        {
            if (id != course.Id) return BadRequest();
            else if (context.Courses.Find(id) == null) return NotFound();
            else
            {
                var crs = context.Courses.Find(id);
                crs.Duration = course.Duration;
                crs.Description = course.Description;
                crs.Name = course.Name;
                context.Courses.Update(crs);
                context.SaveChanges();
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Post(Course course)
        {
            if (course == null) { return BadRequest(); }
            context.Courses.Add(course);
            context.SaveChanges();
            return Created();
        }
    }
}
