using ITIApi.BL.Interfaces;
using ITIApi.DAL.Data.Context;
using ITIApi.DAL.Models.ITI;

namespace ITIApi.BL.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ITIContext context) : base(context)
        {
        }
    }
}
