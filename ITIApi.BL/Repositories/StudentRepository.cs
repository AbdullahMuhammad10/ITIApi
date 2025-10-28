using ITIApi.BL.Interfaces;
using ITIApi.DAL.Data.Context;
using ITIApi.DAL.Models.ITI;

namespace ITIApi.BL.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(ITIContext context) : base(context)
        {
        }
    }
}
