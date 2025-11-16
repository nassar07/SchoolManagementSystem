namespace School.Domain.Interfaces.CourseSpecifies
{
    public interface ICourse
    {
        Task<bool> IsCourseCodeUniqueAsync(string courseCode);
    }
}
