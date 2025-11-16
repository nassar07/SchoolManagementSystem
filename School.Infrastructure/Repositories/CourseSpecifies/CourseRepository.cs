using Microsoft.EntityFrameworkCore;
using School.Domain.Interfaces.CourseSpecifies;
using School.Infrastructure.Data;

namespace School.Infrastructure.Repositories.CourseSpecifies
{
    public class CourseRepository(AppDbContext context) : ICourse
    {
        public async Task<bool> IsCourseCodeUniqueAsync(string courseCode)
        {
            var result = await context.Courses.AnyAsync(c => c.Code == courseCode);
            return !result;
        }
    }
}
