using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sql_emp_cqrs.Application.Employees;
using sql_emp_cqrs.Domain;

namespace sql_emp_cqrs.API
{
    public class EmployeesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees(CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetAll.Query(), cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id, CancellationToken cancellationToken)
        {
            return await Mediator.Send(new GetOne.Query{EmployeeId = id}, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new Create.Command{Employee = employee}, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEmployee(int id, Employee employee, CancellationToken cancellationToken)
        {
            employee.EmployeeId = id;
            return Ok(await Mediator.Send(new Edit.Command{Employee = employee}, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new Delete.Command{EmployeeId = id}, cancellationToken));
        }
    }
}