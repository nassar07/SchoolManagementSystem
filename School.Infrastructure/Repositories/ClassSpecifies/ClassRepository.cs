using Microsoft.EntityFrameworkCore;
using School.Domain.Interfaces.DepartmentSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.DepartmentSpecifies
{
    public class ClassRepository(AppDbContext context) : IClass
    {
        public async Task<bool> DeactiveClass(Guid id)
        {
            var classEntity = await context.Classes.FindAsync(id);
            if (classEntity == null)
            {
                return false;
            }
            classEntity.IsActive = false;
            context.Classes.Update(classEntity);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsClassNameUniqueAsync(string ClassName)
        {
            var result = await context.Classes.AnyAsync(d => d.Name == ClassName);
            return !result;
        }



        
    }
}
