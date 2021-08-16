namespace sql_emp_cqrs.Domain
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeImage { get; set; }
        public int EmployeeSalary { get; set; }
        public string EmployeeOperationStatus {get; set;}
    }
}