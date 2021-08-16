using FluentValidation;
using sql_emp_cqrs.Domain;

namespace sql_emp_cqrs.Application.Employees
{
    public class EditEmployeeValidator : AbstractValidator<EmployeeDTO>
    {
        public EditEmployeeValidator()
        {
            RuleFor(x => x.EmployeeEmail).NotEmpty();
            RuleFor(x => x.EmployeeName).NotEmpty();
            RuleFor(x => x.EmployeeSalary).NotEmpty();
        }
    }
}