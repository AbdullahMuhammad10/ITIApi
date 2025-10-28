namespace ITIApi.Dtos.DepartmentDtos
{
    public class DepartmentWithCountDto
    {
        public int DeptId { get; set; }

        public string DeptName { get; set; }

        public string DeptDesc { get; set; }

        public string DeptLocation { get; set; }

        public int? DeptManager { get; set; }

        public DateOnly? ManagerHiredate { get; set; }
        public int StudentsCount { get; set; }
    }
}
