using System;
using System.Collections.Generic;
using Coffee.HelpDesk.Domain;

namespace Coffee.HelpDesk.DataAccess
{
    public interface IHelpDeskRepository
    {
        IEnumerable<Department> EnumerateDepartments();
        
        Guid CreateDepartment(string name);

        bool UpdateDepartment(Guid departmentId, string name);

        bool DeleteDepartment(Guid departmentId);
        
        Guid? CreateTemplate(Guid departmentId, string template);

        bool UpdateTemplate(Guid templateId, string template);
        
        bool DeleteTemplate(Guid templateId);

        IEnumerable<HelpDeskTemplate> EnumerateTemplate(Guid departmentId);
    }
}
