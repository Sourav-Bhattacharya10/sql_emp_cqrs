using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class Create
    {
        public class Command : IRequest
        {
            public Employee Employee { get; set; }
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
                _context.Employee.Add(request.Employee);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}