using Employees.Shared;

namespace EmployeesApi.Employees;

public record class EmployeeResponseModel
{
    public string Id { get; set; } = string.Empty;
    public NameInformation Name { get; set; } = null!;
    public ContactInformation Contact { get; set; } = null!;
}

public record ContactInformation(string Phone, string Email);