using System;
using System.Collections.Generic;
using Coffee.HelpDesk.Domain;
using Coffee.Shared;

namespace Coffee.HelpDesk.Service
{
    public interface IHelpDeskService
    {
        ResponseResult<IEnumerable<Department>> EnumerateDepartments();
        
        ResponseResult DeleteDepartment(Guid departmentId);
        
        ResponseResult UpdateDepartment(Guid departmentId, string name);
        
        ResponseResult<Guid> CreateDepartment(string name);

        ResponseResult<IEnumerable<HelpDeskTemplate>> EnumerateTemplate(Guid departmentId); 
            
        ResponseResult<Guid> CreateTemplate(Guid departmentId, string template);

        ResponseResult UpdateTemplate(Guid templateId, string template);

        ResponseResult DeleteTemplate(Guid templateId);

        ResponseResult<TicketDescription> EnumerateTickets(Guid userId, int page, int count); 
            
        ResponseResult<Guid> CreateTicket(Guid userId, string subject, string message);

        ResponseResult<Ticket> GetTicket(Guid userId, Guid ticketId); 
            
        ResponseResult<Guid> AddMessageInTicket(Guid userId, Guid ticketId, string message);

        ResponseResult RemoveMessage(Guid messageId);

        ResponseResult RemoveTicket(Guid ticketId);

        ResponseResult<int> CountUnreadTickets(Guid userId);
    }
}