using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using sql_emp_cqrs.Application.Core;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class Delete
    {
        public class Command : IRequest<Result<EmployeeDTO>>
        {
            public int EmployeeId {get; set;}
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
                var emp = await _context.Employee.FindAsync(request.EmployeeId);

                if(emp == null) return Result<EmployeeDTO>.Failure("No employee found");

                var empDTO = _mapper.Map<Employee, EmployeeDTO>(emp);

                empDTO.EmployeeOperationStatus = "deleted";

                _context.Employee.Remove(emp);

                var res = await _context.SaveChangesAsync() > 0;

                if(!res) return Result<EmployeeDTO>.Failure("Failed to delete the employee");

                return Result<EmployeeDTO>.Success(empDTO);
            }
        }
    }
}