using AutoMapper;
using ITIApi.DAL.Models.ITI;
using ITIApi.Dtos.DepartmentDtos;
using ITIApi.Dtos.StudentDtos;

namespace ITIApi.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Student, StudentDeptSupervisorDto>().AfterMap((src, dis) =>
            {
                dis.StDeptName = src.Dept?.DeptName;
                dis.StSuperName = src.StSuperNavigation?.StFname ?? src.StSuperNavigation?.StLname;
                //dis.StSuperName = $"{src.StSuperNavigation?.StFname} {src.StSuperNavigation?.StLname}";
            }).ReverseMap();
            CreateMap<Student, EditStudDto>().ReverseMap();
            CreateMap<Department, DepartmentWithCountDto>().AfterMap((src, dis) =>
            {
                dis.StudentsCount = src.Students.Count();
            }).ReverseMap();
        }
    }
}
