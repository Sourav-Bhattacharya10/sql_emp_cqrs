using FluentValidation;
using sql_emp_cqrs.Domain;

namespace sql_emp_cqrs.Application.Employees
{
    public class CreateEmployeeValidator : AbstractValidator<Employee>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.EmployeeEmail).NotEmpty();
            RuleFor(x => x.EmployeeName).NotEmpty();
            RuleFor(x => x.EmployeePassword).NotEmpty();
            RuleFor(x => x.EmployeeSalary).NotEmpty();
        }
    }
}