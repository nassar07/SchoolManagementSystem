using Microsoft.EntityFrameworkCore;
using School.Domain.Interfaces.DepartmentSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.DepartmentSpecifies
{
    public class DepartmentRepository(AppDbContext context) : IDepartment
    {
        public async Task<bool> IsDepartmentNameUniqueAsync(string departmentName)
        {
            var result = await context.Departments.AnyAsync(d => d.Name == departmentName);
            return !result;
        }
    }
}
