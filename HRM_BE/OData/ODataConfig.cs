using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using ProjectPRN232_HRM.DTOs;

namespace ProjectPRN232_HRM.OData
{
    public static class ODataConfig
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<EmployeeDTO>("Employees");
            builder.EntitySet<DepartmentDTO>("Departments");
            builder.EntitySet<PositionDTO>("Positions");

            return builder.GetEdmModel();
        }
    }
}
