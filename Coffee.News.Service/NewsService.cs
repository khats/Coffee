using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Coffee.News.DataAccess;
using Coffee.News.Domain;
using Coffee.Shared;
using Coffee.Shared.Logging;

namespace Coffee.News.Service
{
    [LoggingIdAttribute("NewsService")]
    public class NewsService : INewsService
    {
        private readonly ILoggingService _loggingService;

        private readonly INewsRepository _newsRepository;

        public NewsService(ILoggingService loggingService, INewsRepository newsRepository)
        {
            _loggingService = loggingService;
            _newsRepository = newsRepository;
        }

        public ResponseResult<IEnumerable<ShortNews>> EnumerateNews(int page, int count)
        {
            try
            {
                if (page < 0 || count < 0)
                {
                    return new ResponseResult<IEnumerable<ShortNews>>("Неверные данные для пагинации");
                }

                var startNumberRow = page*count;
                return
                    new ResponseResult<IEnumerable<ShortNews>>(_newsRepository.EnumerateNews(startNumberRow,
                                                                                             startNumberRow + count));
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении списка новостей";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<IEnumerable<ShortNews>>(errorMessage);
            }
        }

        public ResponseResult<FullNews> GetNews(Guid newsId)
        {
            try
            {
                var result = _newsRepository.GetNews(newsId);
                return result != null
                           ? new ResponseResult<FullNews>(result)
                           : new ResponseResult<FullNews>("Новость не найдена");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при получении новости";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<FullNews>(errorMessage);
            }
        }

        public ResponseResult<Guid> CreateNews(string content, string subject, string description)
        {
            try
            {
                var validResult = ValidateNews(content, subject, description);

                if (validResult != null)
                {
                    return validResult;
                }

                return new ResponseResult<Guid>(_newsRepository.CreateNews(content, subject, description));
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при создании новости";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult<Guid>(errorMessage);
            }
        }

        public ResponseResult UpdateNews(Guid newsId, string content, string subject, string description)
        {
            try
            {
                var validResult = ValidateNews(content, subject, description);

                if (validResult != null)
                {
                    return validResult;
                }

                if (_newsRepository.UpdateNews(newsId, content, subject, description))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }

                return new ResponseResult("Новость не найдена");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при обновлении новости";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult(errorMessage);
            }
        }

        public ResponseResult DeleteNews(Guid newsId)
        {
            try
            {
                if (_newsRepository.DeleteNews(newsId))
                {
                    return new ResponseResult
                               {
                                   IsSuccess = true
                               };
                }

                return new ResponseResult("Новость не найдена");
            }
            catch (Exception e)
            {
                const string errorMessage = "Ошибка при удалении новости";
                _loggingService.Log(this, errorMessage, LogType.Error, e);
                return new ResponseResult(errorMessage);
            }
        }

        private ResponseResult<Guid> ValidateNews(string content, string description, string subject)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return new ResponseResult<Guid>("Тело новости не заполнено");
            }

            if (string.IsNullOrWhiteSpace(subject))
            {
                return new ResponseResult<Guid>("Заголовок новости не заполнен");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return new ResponseResult<Guid>("Описание новости не заполнено");
            }

            if (subject.Length > 256)
            {
                return new ResponseResult<Guid>("Заголовок новости больше 256 символов");
            }

            if (description.Length > 1024)
            {
                return new ResponseResult<Guid>("Описание новости больше 1024 символов");
            }

            return null;
        }
    }
}