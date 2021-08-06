using AutoMapper;
using sql_emp_cqrs.Domain;

namespace sql_emp_cqrs.Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, Employee>();
        }
    }
}