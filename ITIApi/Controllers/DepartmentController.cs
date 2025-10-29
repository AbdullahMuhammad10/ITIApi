using AutoMapper;
using ITIApi.BL.Interfaces;
using ITIApi.DAL.Models.ITI;
using ITIApi.Dtos.DepartmentDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITIApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unit, IMapper mapper)
        {
            this.unit = unit;
            this.mapper = mapper;
        }

        [EndpointSummary("Get All Departments.")]
        [ProducesResponseType(typeof(DepartmentWithCountDto), 200)]
        [HttpGet]
        public IActionResult GetDepartments([FromQuery] string name = "", [FromQuery] int pageSize = 3, [FromQuery] int pageCount = 1)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var departmentsByName = unit.Repository<Department>().GetAll().Include(D => D.Students)
                .Where(D => D.DeptName!.Contains(name))
                .Skip((pageCount - 1) * pageSize).Take(pageSize)
                .ToList();
                return Ok(mapper.Map<List<DepartmentWithCountDto>>(departmentsByName));
            }

            var departments = unit.Repository<Department>().GetAll().Include(D => D.Students)
                .Skip((pageCount - 1) * pageSize).Take(pageSize).ToList();
            return Ok(mapper.Map<List<DepartmentWithCountDto>>(departments));
        }
    }
}
