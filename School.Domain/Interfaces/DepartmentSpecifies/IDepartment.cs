namespace School.Domain.Interfaces.DepartmentSpecifies
{
    public interface IDepartment
    {
        Task<bool> IsDepartmentNameUniqueAsync(string departmentName);
    }
}
