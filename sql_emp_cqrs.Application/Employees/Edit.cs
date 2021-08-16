using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Application.Core;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class Edit
    {
        public class Command : IRequest<Result<EmployeeDTO>>
        {
            public EmployeeDTO Employee { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Employee).SetValidator(new EditEmployeeValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<EmployeeDTO>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<EmployeeDTO>> Handle(Command request, CancellationToken cancellationToken)
            {
                var emp1 = await _context.Employee.AsNoTracking().FirstOrDefaultAsync(empl => empl.EmployeeId == request.Employee.EmployeeId);

                if(emp1 == null) return Result<EmployeeDTO>.Failure("No employee found");

                var emp2 = _mapper.Map<Employee>(request.Employee);
                emp2.EmployeePassword = emp1.EmployeePassword;
                _context.Update<Employee>(emp2);
                
                var res = await _context.SaveChangesAsync() > 0;

                if(!res) return Result<EmployeeDTO>.Failure("Failed to update the employee");

                var emp3 = await _context.Employee.FirstOrDefaultAsync(x => x.EmployeeId == request.Employee.EmployeeId, cancellationToken);

                var empDTO = _mapper.Map<Employee, EmployeeDTO>(emp3);

                empDTO.EmployeeOperationStatus = "updated";

                return Result<EmployeeDTO>.Success(empDTO);
            }
        }
    }
}