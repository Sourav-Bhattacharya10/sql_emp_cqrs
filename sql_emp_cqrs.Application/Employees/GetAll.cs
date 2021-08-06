using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class GetAll
    {
        public class Query : IRequest<List<Employee>>{}

        public class Handler : IRequestHandler<Query, List<Employee>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Employee>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Employee.ToListAsync(cancellationToken);
            }
        }
    }
}