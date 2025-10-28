using AutoMapper;
using ITIApi.BL.Interfaces;
using ITIApi.DAL.Models.ITI;
using ITIApi.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public StudentController(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] string name = "", [FromQuery] int pageSize = 3, [FromQuery] int pageCount = 1)
        {
            var stds = Enumerable.Empty<Student>();
            if (!string.IsNullOrEmpty(name))
            {
                stds = unit.Repository<Student>().GetAll().Include(S => S.Dept).Include(S => S.InverseStSuperNavigation)
               .Where(S => S.StFname!.Contains(name))
               .Skip((pageCount - 1) * pageSize).Take(pageSize)
               .ToList();
                return Ok(mapper.Map<List<StudentDeptSupervisorDto>>(stds));
            }
            stds = unit.Repository<Student>().GetAll().Include(S => S.Dept).Include(S => S.InverseStSuperNavigation)
                .Skip((pageCount - 1) * pageSize).Take(pageSize)
                .ToList();
            return Ok(mapper.Map<List<StudentDeptSupervisorDto>>(stds));

        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            var student = unit.Repository<Student>().GetAll().Include(S => S.InverseStSuperNavigation)
                .Include(S => S.Dept).FirstOrDefault(S => S.StId == id);
            if (student == null) return NotFound();
            //var std = context.Students.Include(S => S.Dept).Include(S => S.InverseStSuperNavigation).ToList();
            return Ok(mapper.Map<StudentDeptSupervisorDto>(student));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var student = unit.Repository<Student>().GetById(id);
            if (student == null) return NotFound();
            unit.Repository<Student>().Delete(student);
            unit.Complete();
            return NoContent();
        }
        [HttpPut]
        public IActionResult Edit(EditStudDto idItDto, int id)
        {
            var student = unit.Repository<Student>().GetById(id);
            if (student == null) return NotFound();
            //student = mapper.Map<Student>(idItDto);
            //student.StId = id;
            student.StLname = idItDto.StLname;
            student.StFname = idItDto.StFname;
            student.StAddress = idItDto.StAddress;
            student.StAge = idItDto.StAge;
            student.DeptId = idItDto.DeptId;
            student.StSuper = idItDto.StSuper;

            unit.Repository<Student>().Update(student);
            unit.Complete();
            return NoContent();
        }
    }
}
