using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using sql_emp_cqrs.Application.Core;
using sql_emp_cqrs.Domain;
using sql_emp_cqrs.Persistence;

namespace sql_emp_cqrs.Application.Employees
{
    public class GetAll
    {
        public class Query : IRequest<Result<List<EmployeeDTO>>>{}

        public class Handler : IRequestHandler<Query, Result<List<EmployeeDTO>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<List<EmployeeDTO>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var empList = await _context.Employee.ToListAsync(cancellationToken);
                var empDTOList = _mapper.Map<List<Employee>, List<EmployeeDTO>>(empList);
                return Result<List<EmployeeDTO>>.Success(empDTOList);
            }
        }
    }
}