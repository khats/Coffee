using System;
using System.Collections.Generic;
using System.Web.Http;
using Coffee.HelpDesk.Domain;
using Coffee.HelpDesk.Service;
using Coffee.Shared;

namespace Coffee.Controllers
{
    public class HelpDeskController : ApiController, IHelpDeskService
    {
        private readonly IHelpDeskService _helpDeskService;

        public HelpDeskController(IHelpDeskService helpDeskService)
        {
            _helpDeskService = helpDeskService;
        }

        [HttpGet]
        public ResponseResult<IEnumerable<Department>> EnumerateDepartments()
        {
            return _helpDeskService.EnumerateDepartments();
        }

        [HttpPost]
        public ResponseResult<Guid> CreateDepartment(string name)
        {
            return _helpDeskService.CreateDepartment(name);
        }

        [HttpGet]
        public ResponseResult<IEnumerable<HelpDeskTemplate>> EnumerateTemplate(Guid departmentId)
        {
            return _helpDeskService.EnumerateTemplate(departmentId);
        }

        [HttpPost]
        public ResponseResult<Guid> CreateTemplate(Guid departmentId, string template)
        {
            return _helpDeskService.CreateTemplate(departmentId, template);
        }

        [HttpPost]
        public ResponseResult UpdateTemplate(Guid templateId, string template)
        {
            return _helpDeskService.UpdateTemplate(templateId, template);
        }

        [HttpPost]
        public ResponseResult DeleteTemplate(Guid templateId)
        {
            return _helpDeskService.DeleteTemplate(templateId);
        }

        public ResponseResult<TicketDescription> EnumerateTickets(Guid userId, int page, int count)
        {
            return _helpDeskService.EnumerateTickets(userId, page, count);
        }

        public ResponseResult<Guid> CreateTicket(Guid userId, string subject, string message)
        {
            return _helpDeskService.CreateTicket(userId, subject, message);
        }

        public ResponseResult<Ticket> GetTicket(Guid userId, Guid ticketId)
        {
            return _helpDeskService.GetTicket(userId, ticketId);
        }

        public ResponseResult<Guid> AddMessageInTicket(Guid userId, Guid ticketId, string message)
        {
            return _helpDeskService.AddMessageInTicket(userId, ticketId, message);
        }

        public ResponseResult RemoveMessage(Guid messageId)
        {
            return _helpDeskService.RemoveMessage(messageId);
        }

        public ResponseResult RemoveTicket(Guid ticketId)
        {
            return _helpDeskService.RemoveTicket(ticketId);
        }

        public ResponseResult<int> CountUnreadTickets(Guid userId)
        {
            return _helpDeskService.CountUnreadTickets(userId);
        }

        [HttpPost]
        public ResponseResult UpdateDepartment(Guid departmentId, string name)
        {
            return _helpDeskService.UpdateDepartment(departmentId, name);
        }

        [HttpPost]
        public ResponseResult DeleteDepartment(Guid departmentId)
        {
            return _helpDeskService.DeleteDepartment(departmentId);
        }
    }
}