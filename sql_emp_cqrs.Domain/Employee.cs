namespace sql_emp_cqrs.Domain
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePassword { get; set; }
        public string EmployeeImage { get; set; }
        public int EmployeeSalary { get; set; }
    }
}