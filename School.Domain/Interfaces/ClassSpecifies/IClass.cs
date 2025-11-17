namespace School.Domain.Interfaces.DepartmentSpecifies
{
    public interface IClass
    {
        Task<bool> IsClassNameUniqueAsync(string ClassName);
        Task<bool> DeactiveClass(Guid id);
    }
}
