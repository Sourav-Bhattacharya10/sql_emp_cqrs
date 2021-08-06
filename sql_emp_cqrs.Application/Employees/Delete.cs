using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int EmployeeId {get; set;}
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var emp = await _context.Employee.FindAsync(request.EmployeeId);

                _context.Employee.Remove(emp);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}