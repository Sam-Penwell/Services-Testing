using Employees.Shared;

namespace EmployeesApi.Employees;

public static class EntityMappers
{
    public static EmployeeResponseModel MapToResponse(this EmployeeEntity entity)
    {
        return new EmployeeResponseModel()
        {
            Id = entity.Id,
            Name = new NameInformation()
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName
            },
            Contact = new ContactInformation(entity.PhoneNumber, entity.EMailAddress)
        };
    }
}