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
    public class Create
    {
        public class Command : IRequest<Result<EmployeeDTO>>
        {
            public Employee Employee { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Employee).SetValidator(new CreateEmployeeValidator());
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
                _context.Employee.Add(request.Employee);

                var res = await _context.SaveChangesAsync(cancellationToken) > 0;

                if(!res) return Result<EmployeeDTO>.Failure("Failed to create new employee");

                var emp = await _context.Employee.FirstOrDefaultAsync(x => x.EmployeeEmail == request.Employee.EmployeeEmail, cancellationToken);

                var empDTO = _mapper.Map<Employee, EmployeeDTO>(emp);

                empDTO.EmployeeOperationStatus = "created";

                return Result<EmployeeDTO>.Success(empDTO);
            }
        }
    }
}