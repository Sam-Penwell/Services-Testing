using Employees.Shared;
using Employees.Shared.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace EmployeesApi.Employees;

public static class Api
{
    public static IEndpointRouteBuilder MapEmployees(this IEndpointRouteBuilder rb)
    {
        var group = rb.MapGroup("/employees");

        group.MapGet("/{id}", GetAnEmployee);

        return group;
    }

    private static async Task<Results<Ok<EmployeeResponseModel>, NotFound>> GetAnEmployee(string id, EmployeeRepository repository, CancellationToken token)
    {
       var result = await repository.GetEmployeeByIdAsnc(id, token);

        return result.Match<Results<Ok<EmployeeResponseModel>, NotFound>>(
            employee => TypedResults.Ok(employee.MapToResponse()),
            _ => TypedResults.NotFound()
        );
    }

    
}



