using OneOf;
using OneOf.Types;

namespace Employees.Shared.Services;

public class EmployeeRepository
{
    // Fake Data
    private IReadOnlyList<EmployeeEntity> _employees =
    [
        new EmployeeEntity()
        {
            Id = "1",
            FirstName = "Bob",
            LastName = "Smith",
            PhoneNumber = "555-1212",
            EMailAddress = "bob@aol.com"
        },
        new EmployeeEntity()
        {
            Id = "2",
            FirstName = "Sue",
            LastName = "Jones",
            PhoneNumber = "555-1213",
            EMailAddress = "sue@aol.com"
        }
    ];
    public async Task< OneOf<EmployeeEntity,None>> GetEmployeeByIdAsnc(string id, CancellationToken token)
    {
        var emp = _employees.FirstOrDefault(e => e.Id == id);
        if (emp is null)
        {
            return new None();
        }
        else
        {
            return emp;
        }
    }
}