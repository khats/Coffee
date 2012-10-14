using System;
using System.Collections.Generic;
using Coffee.HelpDesk.DataAccess;
using Coffee.HelpDesk.Domain;
using Coffee.Shared;
using Coffee.Shared.Logging;

namespace Coffee.HelpDesk.Service
{
    public class HelpDeskService : IHelpDeskService
    {
        private readonly ILoggingService _loggingService;

        private readonly IHelpDeskRepository _helpDeskRepository;

        public HelpDeskService(ILoggingService loggingService, IHelpDeskRepository helpDeskRepository)
        {
            _loggingService = loggingService;
            _helpDeskRepository = helpDeskRepository;
        }

        public ResponseResult<IEnumerable<Department>> EnumerateDepartments()
        {
            try
            {
                return new ResponseResult<IEnumerable<Department>>(_helpDeskRepository.EnumerateDepartments());
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении отделов";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<Department>>(errorMessage);
            }
        }

        public ResponseResult<Guid> CreateDepartment(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ResponseResult<Guid>("Не задано название отдела");
                }

                if (name.Length > 256)
                {
                    return new ResponseResult<Guid>("Название отдела больше 256 символов");
                }

                return new ResponseResult<Guid>(_helpDeskRepository.CreateDepartment(name));
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при создании отдела";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }

        public ResponseResult<IEnumerable<HelpDeskTemplate>> EnumerateTemplate(Guid departmentId)
        {
            try
            {
                return new ResponseResult<IEnumerable<HelpDeskTemplate>>(_helpDeskRepository.EnumerateTemplate(departmentId));
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении шаблонов";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<HelpDeskTemplate>>(errorMessage);
            }
        }

        public ResponseResult<Guid> CreateTemplate(Guid departmentId, string template)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template))
                {
                    return new ResponseResult<Guid>("Текст шаблона не заполнен");
                }

                if (template.Length > 256)
                {
                    return new ResponseResult<Guid>("Шаблон больше 256 символов");
                }

                var templateId = _helpDeskRepository.CreateTemplate(departmentId, template);
                if (templateId == null)
                {
                    return new ResponseResult<Guid>("Не удалось найти отдел");
                }

                return new ResponseResult<Guid>((Guid)templateId);
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при создании шаблона для отдела";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }

        public ResponseResult UpdateTemplate(Guid templateId, string template)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template))
                {
                    return new ResponseResult("Текст шаблона не заполнен");
                }

                if (template.Length > 256)
                {
                    return new ResponseResult("Шаблон больше 256 символов");
                }

                if (_helpDeskRepository.UpdateTemplate(templateId, template))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }
                
                return new ResponseResult("Шаблон не найден");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при обновлении шаблона";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }

        public ResponseResult DeleteTemplate(Guid templateId)
        {
            try
            {
                if (_helpDeskRepository.DeleteTemplate(templateId))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }
                
                return new ResponseResult("Шаблон не найден");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при удалении шаблона";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }

        public ResponseResult<TicketDescription> EnumerateTickets(Guid userId, int page, int count)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<Guid> CreateTicket(Guid userId, string subject, string message)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<Ticket> GetTicket(Guid userId, Guid ticketId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<Guid> AddMessageInTicket(Guid userId, Guid ticketId, string message)
        {
            throw new NotImplementedException();
        }

        public ResponseResult RemoveMessage(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult RemoveTicket(Guid ticketId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult<int> CountUnreadTickets(Guid userId)
        {
            throw new NotImplementedException();
        }

        public ResponseResult UpdateDepartment(Guid departmentId, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return new ResponseResult("Не задано название отдела");
                }

                if (name.Length > 256)
                {
                    return new ResponseResult("Название отдела больше 256 символов");
                }

                if (_helpDeskRepository.UpdateDepartment(departmentId, name))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }
                
                return new ResponseResult
                           {
                               ErrorMessage = "Отдел с таким названием уже существует"
                           };
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении новости";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult(errorMessage);
            }
        }

        public ResponseResult DeleteDepartment(Guid departmentId)
        {
            try
            {
                if (_helpDeskRepository.DeleteDepartment(departmentId))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }
                
                return new ResponseResult("Не удалось удалить отдел.");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при удалении отдела";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }
    }
}
