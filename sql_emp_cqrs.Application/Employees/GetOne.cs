using System;
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
    public class GetOne
    {
        public class Query : IRequest<Result<EmployeeDTO>>
        {
            public int EmployeeId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EmployeeDTO>>
        {
            private readonly DataContext _context;

            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<EmployeeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var emp = await _context.Employee.FindAsync(request.EmployeeId);
                var empDTO = _mapper.Map<Employee, EmployeeDTO>(emp);
                return Result<EmployeeDTO>.Success(empDTO);
            }
        }
    }
}